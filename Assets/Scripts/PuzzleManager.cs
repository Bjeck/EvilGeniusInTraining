using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PuzzleManager : Singleton<PuzzleManager> {
	protected PuzzleManager() { }

	public bool DebugGame = false;

	public House house;

	public List<Puzzle> allPuzzles = new List<Puzzle>();
	public List<Puzzle> gamePuzzles = new List<Puzzle>();

	[SerializeField] Evil_Genius teamEvil;
	[SerializeField] Agents teamAgent;
	[SerializeField] PuzzleInstantiator puzzleInstantiator;

	Puzzle curPuzzle;
	public bool isPuzzleRunning = false;

	[SerializeField] InputField solutionIPF;
	[SerializeField] Text timerUI;
	[SerializeField] Text outputText;
	float timer;

	int puzzleIterator = 0;


	// Use this for initialization
	void Start () {
		allPuzzles = puzzleInstantiator.CreatePuzzles ();

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


	public void StartPuzzle(Puzzle p){
		if (gamePuzzles.Exists (x => x == p) && !p.hasRun) {
			curPuzzle = p;
			timer = p.timeToComplete;
			isPuzzleRunning = true;
		}
	}

	public void CheckSolution(){
		if (solutionIPF.text.ToLower () == curPuzzle.solution) {
			ExecuteSolution ();
		} else {
			print ("Wrong solution :(");
		}
	}

	void ExecuteSolution(){

		print ("CORRECT!");
		//give points
		if (timer > 0) {
			teamAgent.points += 1;
		} else if (timer < 0) {
			teamEvil.points += 1;
		}

		//give output.
		outputText.text = curPuzzle.outputclue;

		isPuzzleRunning = false;
		curPuzzle = null;
		timer = 0;
		//timerUI.text = "";

		//start next puzzle?

		puzzleIterator += 1;
		if (gamePuzzles.Count > puzzleIterator) {
			StartPuzzle (gamePuzzles [puzzleIterator]);
		} else {
			print ("NO MORE PUZZLES!");
		}

	}


	public void AddPuzzleToGame(Puzzle p){
		print ("ADDING PUZZLE TO GAME " + p.puzzleName);
		gamePuzzles.Add (p);
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