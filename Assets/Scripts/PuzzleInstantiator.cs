﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleInstantiator : MonoBehaviour {

	[SerializeField] GameObject puzzlePrefab;
	public List<Puzzle> possiblePuzzles = new List<Puzzle>();


	public List<Puzzle> CreatePuzzles(){
		possiblePuzzles.Clear ();

		GameObject newPuzzle = (GameObject)Instantiate(puzzlePrefab,transform);
		Puzzle np = newPuzzle.GetComponent<Puzzle> ();
		np.puzzleName = "FreezerPuzzle";
		np.requirements.Add (roomSpecs.Freezer);
		possiblePuzzles.Add (np);

		newPuzzle = (GameObject)Instantiate(puzzlePrefab,transform);
		np = newPuzzle.GetComponent<Puzzle> ();
		np.puzzleName = "CardPuzzle";
		np.requirements.Clear (); np.requirements.Add (roomSpecs.Table);
		possiblePuzzles.Add (np);

		newPuzzle = (GameObject)Instantiate(puzzlePrefab,transform);
		np = newPuzzle.GetComponent<Puzzle> ();
		np.puzzleName = "windowCodePuzzle";
		np.requirements.Clear (); np.requirements.Add (roomSpecs.DoubleSidedWindow);
		possiblePuzzles.Add (np);

		return possiblePuzzles;
	
	}

}