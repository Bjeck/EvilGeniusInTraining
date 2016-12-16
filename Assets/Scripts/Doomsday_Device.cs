using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

enum Adjective {snake};
enum Noun {Ray_Gun};
enum Target {Paris};

public class Doomsday_Device : MonoBehaviour {

	Adjective adjective;
	Noun noun;
	Target target;

	string doomsdayDevice = "ray gun";

	[SerializeField] InputField ipf;
	[SerializeField] GameObject successMonologuePanel;
	[SerializeField] Text monologue;

	List<string> successClues = new List<string>();
	List<string> failureClues = new List<string>();

	float finalTime = 60;
	[SerializeField] Text finalTimer;
	[SerializeField] Image finalMeter;
	[SerializeField] GameObject explosionPanel;

	[SerializeField] AudioSource explosion;
	[SerializeField] AudioSource music;

	[SerializeField] Text needyText;

	void Start(){

		InstantiateClues ();
		//FillInClues ();
		InitializeDevice ();
	}


	public void InitializeDevice(){

		//Set properties


		ConstructEvilMonologue ();


		//set the needy text (if applicable)
		needyText.text = "SNAKES AWAKE!";

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
				p.successclue = successClues [0];


				p.failureclue = successClues [0]; // FOR NOW. THEY ARE THE SAME!

				//successClues.Remove (successClues[r]);

			//	failureClues.Remove (failureClues[r]);
			}

		}

	}


	public void WhatIsDoomsdayDevice(){
		
	}


	public void InputDoomsday(){
		if(ipf.text.ToLower().Contains(doomsdayDevice)){
			print("Yes. THat is the correct doomsday Device");
			successMonologuePanel.SetActive(true);
		}
	}


	public void ConstructEvilMonologue(){
		monologue.text = "Ahah! <i>[Taunt them]</i>. You have discovered the true power of this device! I will use this <i>" + ConvertSpaces(adjective.ToString()) + "-powered " + ConvertSpaces(noun.ToString()) + "</i> to _______ <i>" +ConvertSpaces(target.ToString()) +
			"!</i> You thought <i>[embarrassing thing they did tonight]</i> would help, you thought <i>[silly thing they did tonight]</i> would lead to your salvation. But no, tonight, evil wins! And I, " +
			PuzzleManager.Instance.teamEvil.teamName + ", will make it so! <i>[Evil laugh]</i>";
	}




	void InstantiateClues(){

		//successClues.Add("Our intelligence reports that our rival agency has developed technology that harnesses the lining potential in a thundercloud and weaponises it as an electromagnetic pulse. In testing it was confirmed to not be effective in turning things into stone.");
		successClues.Add ("Due to not having the precision afforded by a laser, the weather control ray gun is only effective on large a geographical target.");
		//successClues.Add ("Experiments in time travel technology have to date never left their subjects alive. Lasers although useful have been shown to have no application in causing time travel.");
		//successClues.Add ("The mind controlling acid can only be effectively used on human targets and will not be deployed by a bomb or by drones.");
		//successClues.Add ("The rare snake venom, that has the ability to petrify things, needs the snakes to still be alive when activated.");
		//successClues.Add ("It takes an atomic energy source to have enough power to control the weaponised thundercloud. This is not the device designed to shrink the polar ice caps.");
		//successClues.Add ("the moon will be destroyed by nano-technology, but they will not do so by shrinking it. As with all modern monarchies, the queen has been bred to be impervious to all forms of robot assault.");

		//failureClues.Add ("failure clue");
		//failureClues.Add ("failure clue");
		//failureClues.Add ("failure clue");
		//failureClues.Add ("failure clue");
		//failureClues.Add ("failure clue");
		//failureClues.Add ("failure clue");
		//failureClues.Add ("failure clue");

	}


	public void StartFinalPuzzle(){

		music.Play ();
		StartCoroutine (puzzleTimer ());

	}

	IEnumerator puzzleTimer(){
		float tentimer = 10;
		needyText.gameObject.SetActive(false);

		while (finalTime > 0) {
			finalTime -= Time.deltaTime;
			finalTimer.text = finalTime.ToString("F2");

			Vector3 sc = finalMeter.transform.localScale;
			sc.x = -finalTime/121*3;
			finalMeter.transform.localScale = sc;

			tentimer -= Time.deltaTime;
			if(tentimer < 0){
				ShowNeedy();
				tentimer = 10;
			}

			yield return new WaitForEndOfFrame ();
		}

		//OH NO, TIME RAN OUT
		LoseGame();

		yield return new WaitForEndOfFrame ();

	}

	public void ShowNeedy(){
		needyText.gameObject.SetActive(true);
	}

	public void HideNeedy(){
		needyText.gameObject.SetActive(false);

	}

	public void Mistake(){
		finalTime -= 10f;
	}

	public void LoseGame(){
		music.Stop ();

		explosionPanel.SetActive (true);
		explosion.Play ();

	}
	public void WinGame(){
		music.Stop ();
	}






	public string ConvertSpaces(string s){

		return s.Replace ("_", " ");

	}


}
