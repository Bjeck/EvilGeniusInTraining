using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleManager : Singleton<PuzzleManager> {
	protected PuzzleManager() { }

	public House house;

	List<Puzzle> allPuzzles = new List<Puzzle>();
	List<Puzzle> gamePuzzles = new List<Puzzle>();

	Evil_Genius teamEvil;
	Agents teamAgent;

	Puzzle curPuzzle;

	float timer;


	// Use this for initialization
	void Start () {
	
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
