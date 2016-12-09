using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

enum Adjective {};
enum Noun {};
enum Target {};

public class Doomsday_Device : MonoBehaviour {

	Adjective adjective;
	Noun noun;
	Target target;

	string doomsdayDevice = "bomb";

	[SerializeField] InputField ipf;
	[SerializeField] GameObject successVideoPanel;

	string EvilMonologue;

	List<string> successClues = new List<string>();
	List<string> failureClues = new List<string>();

	void Start(){

		InstantiateClues ();
		//FillInClues ();

	}

	public void InitializeDevice(){

		//Set properties

	}

	public void FillInClues(){
		print ("filling in clues");
		int nrOfPuzzles = PuzzleManager.Instance.gamePuzzles.Count;
		int cluePrPuzzle = successClues.Count / nrOfPuzzles;

		foreach (Puzzle p in PuzzleManager.Instance.gamePuzzles) {
			print ("filling in "+p.puzzleName+" "+cluePrPuzzle);
			for (int i = 0; i < cluePrPuzzle; i++) {
				print ("filling in "+i);
				int r = Random.Range (0, successClues.Count);
				p.successclue = successClues [r];
				successClues.Remove (successClues[r]);

				//int r = Random.Range (0, successClues.Count);		Are they tied? Should they be? Hm.
				p.failureclue = failureClues [r];
				failureClues.Remove (failureClues[r]);
			}

		}

	}


	public void WhatIsDoomsdayDevice(){
		
	}


	public void InputDoomsday(){
		if(ipf.text == doomsdayDevice){
			print("Yes. THat is the correct doomsday Device");
			successVideoPanel.SetActive(true);
		}
	}




	void InstantiateClues(){

		successClues.Add("Our intelligence reports that our rival agency has developed technology that harnesses the lining potential in a thundercloud and weaponises it as an electromagnetic pulse. In testing it was confirmed to not be effective in turning things into stone.");
		successClues.Add ("Due to not having the precision afforded by a laser, the weather control ray gun is only effective on large a geographical target.");
		successClues.Add ("Experiments in time travel technology have to date never left their subjects alive. Lasers although useful have been shown to have no application in causing time travel.");
		successClues.Add ("The mind controlling acid can only be effectively used on human targets and will not be deployed by a bomb or by drones.");
		successClues.Add ("The rare snake venom, that has the ability to petrify things, needs the snakes to still be alive when activated.");
		successClues.Add ("It takes an atomic energy source to have enough power to control the weaponised thundercloud. This is not the device designed to shrink the polar ice caps.");
		successClues.Add ("the moon will be destroyed by nano-technology, but they will not do so by shrinking it. As with all modern monarchies, the queen has been bred to be impervious to all forms of robot assault.");

		failureClues.Add ("failure clue");
		failureClues.Add ("failure clue");
		failureClues.Add ("failure clue");
		failureClues.Add ("failure clue");
		failureClues.Add ("failure clue");
		failureClues.Add ("failure clue");
		failureClues.Add ("failure clue");


	}


}
