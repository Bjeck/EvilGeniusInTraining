using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleManager : Singleton<PuzzleManager> {
	protected PuzzleManager() { }

	public House house;

	public List<Puzzle> allPuzzles = new List<Puzzle>();
	List<Puzzle> gamePuzzles = new List<Puzzle>();

	Evil_Genius teamEvil;
	Agents teamAgent;
	[SerializeField] PuzzleInstantiator puzzleInstantiator;

	Puzzle curPuzzle;

	float timer;


	// Use this for initialization
	void Start () {
		allPuzzles = puzzleInstantiator.CreatePuzzles ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void ExecuteSolution(){

		//give points

		//give output.

		//start next puzzle?

	}
}
