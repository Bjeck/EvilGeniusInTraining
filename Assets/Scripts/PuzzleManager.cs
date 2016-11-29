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
	[SerializeField] PuzzleInstantiator puzzleInstantiator;
	[SerializeField] RoomInstantiator roomInstantiator;
	[SerializeField] MentorInstantiator mentorInstantiator;

	Puzzle curPuzzle;
	public bool isPuzzleRunning = false;
	public bool runsOnLives = false;

	[SerializeField] InputField solutionIPF;
	[SerializeField] Text timerUI;
	[SerializeField] Text outputText;
	[SerializeField] Text curPuzzleText;
	[SerializeField] Text livesCounter;

	[SerializeField] GameObject hintPanel; [SerializeField] Text hintPanelText;

	public float timer;

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
			timerUI.text = timer.ToString();
		}
	}


	public void BeginGame(){

		StartPuzzle (gamePuzzles [puzzleIterator]);

	}


	public void StartNextPuzzleInIteration(){
		puzzleIterator += 1;
		if (gamePuzzles.Count > puzzleIterator) {
			StartPuzzle (gamePuzzles [puzzleIterator]);
		} else {
			print ("NO MORE PUZZLES!");
		}
	}

	public void StartPuzzle(Puzzle p){
		if (gamePuzzles.Exists (x => x == p) && !p.hasRun) {
			curPuzzle = p;
			curPuzzleText.text = curPuzzle.puzzleName;
			timer = p.timeToComplete;
			if (p.lives > 0) {
				livesCounter.gameObject.SetActive (true);
				UpdateLivesText (0);
				runsOnLives = true;
			}
			isPuzzleRunning = true;
		}
	}

	public void CheckSolution(){
		if (solutionIPF.text.ToLower () == curPuzzle.solution) {
			ExecuteSolution ();
		} else {
			print ("Wrong solution :(");
			if (runsOnLives) {
				UpdateLivesText (-1);
				if(curPuzzle.lives <= 0){
					timer = 0;
					ExecuteSolution ();
				}
			}
		}
	}

	void ExecuteSolution(){

		//give points
		if (timer > 0) {
			teamAgent.points += 1;
		} else if (timer <= 0) {
			teamEvil.points += 1;
		}

		//give output.
		outputText.text = curPuzzle.outputclue;

		isPuzzleRunning = false;
		curPuzzle = null;
		timer = 0;
		livesCounter.gameObject.SetActive (false);
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
			curPuzzle.onHelpAgent (curPuzzle);
			teamAgent.tokens -= 1;
		}
	}

	public void EvilHelp(){
		if (teamEvil.tokens > 0) {
			DisplayHint (curPuzzle.evilhelp);
			curPuzzle.onHelpEvil (curPuzzle);
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