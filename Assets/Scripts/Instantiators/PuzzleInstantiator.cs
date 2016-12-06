using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PuzzleInstantiator : MonoBehaviour {

	public Transform puzzleInstructions;
	[SerializeField] GameObject puzzlePrefab;
	[SerializeField] Transform puzzleParent; 
	public List<Puzzle> possiblePuzzles = new List<Puzzle>();


	public List<Puzzle> CreatePuzzles(){
		possiblePuzzles.Clear ();

		GameObject newPuzzle = (GameObject)Instantiate(puzzlePrefab,puzzleParent);
		Puzzle np = newPuzzle.GetComponent<Puzzle> ();
		np.puzzleName = "Freezer Puzzle";
		np.requirements.Add (roomSpecs.Freezer);
		np.solution = "frozen";
		np.successclue = "This will display the success clue! positive about the device.";
		np.failureclue = "This will display the failure clue! negative about the device.";
		np.timeToComplete = 5;
		np.mentor = MentorID.MrFreeze;
		np.lives = 2;
		np.instructionPanels = puzzleInstructions.FindChild(np.puzzleName).GetComponentsInChildren<PuzzleInstructionPanel>().ToList();
		possiblePuzzles.Add (np);

		newPuzzle = (GameObject)Instantiate(puzzlePrefab,puzzleParent);
		np = newPuzzle.GetComponent<Puzzle> ();
		np.puzzleName = "Card Puzzle";
		np.requirements.Clear (); np.requirements.Add (roomSpecs.Table);
		np.solution = "cards";
		np.timeToComplete = 60;
		np.instruction = "Take the stack of cards, and build a card tower. Type in the word on the bottom-most card.";
		np.successclue = "This will display another clue!";
		np.agenthelp = "You may remove 10 cards from the deck.";
		np.evilhelp = "You may slam the table.";
		np.onHelpAgent = (p) => { PuzzleManager.Instance.timer += 20; };
		np.mentor = MentorID.None;
		np.instructionPanels = puzzleInstructions.FindChild(np.puzzleName).GetComponentsInChildren<PuzzleInstructionPanel>().ToList();
		possiblePuzzles.Add (np);
		/*
		newPuzzle = (GameObject)Instantiate(puzzlePrefab,puzzleParent);
		np = newPuzzle.GetComponent<Puzzle> ();
		np.puzzleName = "windowCodePuzzle";
		np.requirements.Clear (); np.requirements.Add (roomSpecs.DoubleSidedWindow);
		np.mentor = MentorID.None;
		np.lives = 2;
		possiblePuzzles.Add (np);

		newPuzzle = (GameObject)Instantiate(puzzlePrefab,puzzleParent);
		np = newPuzzle.GetComponent<Puzzle> ();
		np.puzzleName = "Seating Puzzle";
		np.requirements.Clear (); np.requirements.Add (roomSpecs.Seating);
		np.solution = "sit";
		np.successclue = "This will display another clue!";
		np.mentor = MentorID.None;
		possiblePuzzles.Add (np);
*/
		return possiblePuzzles;
	
	}

}
