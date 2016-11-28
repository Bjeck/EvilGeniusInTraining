using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleInstantiator : MonoBehaviour {

	[SerializeField] GameObject puzzlePrefab;
	[SerializeField] Transform puzzleParent; 
	public List<Puzzle> possiblePuzzles = new List<Puzzle>();


	public List<Puzzle> CreatePuzzles(){
		possiblePuzzles.Clear ();

		GameObject newPuzzle = (GameObject)Instantiate(puzzlePrefab,puzzleParent);
		Puzzle np = newPuzzle.GetComponent<Puzzle> ();
		np.puzzleName = "FreezerPuzzle";
		np.requirements.Add (roomSpecs.Freezer);
		np.solution = "frozen";
		np.outputclue = "This will display the clue!";
		np.mentor = MentorID.MrFreeze;
		possiblePuzzles.Add (np);

		newPuzzle = (GameObject)Instantiate(puzzlePrefab,puzzleParent);
		np = newPuzzle.GetComponent<Puzzle> ();
		np.puzzleName = "CardPuzzle";
		np.requirements.Clear (); np.requirements.Add (roomSpecs.Table);
		np.solution = "cards";
		np.outputclue = "This will display another clue!";
		np.mentor = MentorID.None;
		possiblePuzzles.Add (np);

		newPuzzle = (GameObject)Instantiate(puzzlePrefab,puzzleParent);
		np = newPuzzle.GetComponent<Puzzle> ();
		np.puzzleName = "windowCodePuzzle";
		np.requirements.Clear (); np.requirements.Add (roomSpecs.DoubleSidedWindow);
		np.mentor = MentorID.None;
		possiblePuzzles.Add (np);

		newPuzzle = (GameObject)Instantiate(puzzlePrefab,puzzleParent);
		np = newPuzzle.GetComponent<Puzzle> ();
		np.puzzleName = "Seating Puzzle";
		np.requirements.Clear (); np.requirements.Add (roomSpecs.Seating);
		np.solution = "sit";
		np.outputclue = "This will display another clue!";
		np.mentor = MentorID.None;
		possiblePuzzles.Add (np);

		return possiblePuzzles;
	
	}

}
