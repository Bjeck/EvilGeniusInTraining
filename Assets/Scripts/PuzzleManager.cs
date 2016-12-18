using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PuzzleManager : Singleton<PuzzleManager> {
	protected PuzzleManager() { }

	public bool DebugGame = false;

	public House house;
	public List<Room> allRooms = new List<Room> ();
	public List<Puzzle> allPuzzles = new List<Puzzle>();
	public List<Puzzle> gamePuzzles = new List<Puzzle>();

	public List<Mentor> allMentors = new List<Mentor> ();
	public Mentor selectedMentor;

	public Evil_Genius teamEvil;
	public Agents teamAgent;
	public PuzzleInstantiator puzzleInstantiator;
	[SerializeField] RoomInstantiator roomInstantiator;
	[SerializeField] MentorInstantiator mentorInstantiator;
	public Doomsday_Device doomsday;

	public PuzzleCustoms puzzleCustomFunctions;

	public Puzzle curPuzzle;
	public bool isPuzzleRunning = false;
	public bool runsOnLives = false;

	[SerializeField] InputField solutionIPF;
	[SerializeField] Text timerUI;
	[SerializeField] Text outputText;
	[SerializeField] Text curPuzzleText;
	[SerializeField] Text livesCounter;

	[SerializeField] GameObject hintPanel; [SerializeField] Text hintPanelText;
	[SerializeField] GameObject introPanel;
	[SerializeField] GameObject doomsdayPanel;


	public GameObject customPuzzleObject;

	public float timer;
	[SerializeField] Image timerMeter;
	[SerializeField] GameObject timerParent;

	int puzzleIterator = 0;


	// Use this for initialization
	void Start () {
		allMentors = mentorInstantiator.CreateMentors ();
		allPuzzles = puzzleInstantiator.CreatePuzzles ();
		allRooms = roomInstantiator.CreateRooms ();

		if (DebugGame) {
			DebugGameStart ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isPuzzleRunning) {
			timer -= Time.deltaTime;
			timerUI.text = timer.ToString("F2");
			SetTimerMeter();
			if(timer < 0 && timerUI.color == Color.white){
				timerUI.color = Color.red;
				timerMeter.color = Color.red;
			}
			if(timer < (0-curPuzzle.timeToComplete)){
				//END PUZZLE
				ExecuteSolution();
			}
		}

		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}

	void SetTimerMeter(){
		Vector3 sc = timerMeter.transform.localScale;
		sc.x = -timer/curPuzzle.timeToComplete*3;
		timerMeter.transform.localScale = sc;
	}



	public void InitiateGame(){
		introPanel.SetActive (true);
	}


	public void BeginGame(){

		doomsday.FillInClues ();

		StartPuzzle (gamePuzzles [puzzleIterator]);

	}


	public void StartNextPuzzleInIteration(){
		puzzleIterator += 1;
		if (gamePuzzles.Count > puzzleIterator) {
			StartPuzzle (gamePuzzles [puzzleIterator]);
		} else {
			print ("NO MORE PUZZLES!");
			doomsdayPanel.SetActive(true);
			doomsday.WhatIsDoomsdayDevice();
		}
	}

	public void StartPuzzle(Puzzle p){
		if (gamePuzzles.Exists (x => x == p) && !p.hasRun) {
			curPuzzle = p;
			curPuzzleText.text = curPuzzle.instruction;
			outputText.text = "";
			solutionIPF.text = "";
			timer = p.timeToComplete;
			timerParent.SetActive(true);
			timerUI.color = Color.white;
			timerMeter.color = Color.white;
			if (p.lives > 0) {
				livesCounter.gameObject.SetActive (true);
				UpdateLivesText (0);
				runsOnLives = true;
			}
			if (curPuzzle.onPlay != null) {
				curPuzzle.onPlay (curPuzzle); //runs delegate function, if present.
			}

			isPuzzleRunning = true;
		}
	}

	public void CheckSolution(string s){
		solutionIPF.text = s;
		CheckSolution ();
	}

	public void CheckSolution(){
		if (solutionIPF.text.ToLower () == curPuzzle.solution) {
			ExecuteSolution ();
		} else {
			print ("Wrong solution :(");
			if (runsOnLives) {
				UpdateLivesText (-1);
				if(curPuzzle.lives < 0){
					timer = 0;
					ExecuteSolution ();
				}
			}
		}
	}

	void ExecuteSolution(){

		timerParent.SetActive(false);
		//give points
		if (timer > 0) {
			teamAgent.points += 1;
			outputText.text = curPuzzle.successclue;
		} else if (timer <= 0) {
			teamEvil.points += 1;
			if(curPuzzle.lives >= 0){
				outputText.text = curPuzzle.failureclue;
			}
			else {
				outputText.text = "You failed. You receive no clue.";
			}
		}


		isPuzzleRunning = false;
		curPuzzle = null;
		timer = 0;
		livesCounter.gameObject.SetActive (false);

		if(customPuzzleObject != null){
			customPuzzleObject.SetActive (false);
		}


		customPuzzleObject = null;

		//timerUI.text = "";

		//start next puzzle?
	}


	public void AddPuzzleToGame(Puzzle p, Room r){
		print ("ADDING PUZZLE TO GAME " + p.puzzleName);
		gamePuzzles.Add (p);
	}


	public void AddHelpTokenAgent(){
		teamAgent.tokens += 1;
	}

	public void AgentHelp(){
		if (teamAgent.tokens > 0) {
			DisplayHint (curPuzzle.agenthelp);
			if(curPuzzle.onHelpAgent != null){
				curPuzzle.onHelpAgent (curPuzzle);
			}
			teamAgent.tokens -= 1;
		}
	}

	public void EvilHelp(){
		if (teamEvil.tokens > 0) {
			DisplayHint (curPuzzle.evilhelp);
			if(curPuzzle.onHelpEvil != null){
				curPuzzle.onHelpEvil (curPuzzle);
			}
			teamEvil.tokens -= 1;
		}
	}

	public void DisplayHint(string textToDisp){
		if (hintPanel == null) {
			hintPanelText = hintPanel.GetComponentInChildren<Text> ();
			print ("set hintpaneltext");
		}
		print (curPuzzle.puzzleName);
		print (hintPanelText);
		hintPanelText.text = textToDisp;
		hintPanel.SetActive (true);
	}

	public void UpdateLivesText(int livesChange){
		curPuzzle.lives += livesChange;
		livesCounter.text = "Lives :" + curPuzzle.lives;
	}

	public void DebugGameStart(){
		gamePuzzles = allPuzzles;

		BeginGame ();
	}

	public void SetupTeams(){
		
	

	}


}

//QUESTOINS


// how do we deal with multiple puzzles at once? Two timers? Or one timer for both?
// should puzzles go directly from one to the next. what should happen in between?