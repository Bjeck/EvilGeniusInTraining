using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PuzzleCustoms : MonoBehaviour {


	[SerializeField] GameObject windowPuzzle;
	[SerializeField] Text windowPuzzlePanel;


	public void WindowPuzzle(){

		windowPuzzle.SetActive (true);
		print ("WINDOW PUZZLE");
		PuzzleManager.Instance.customPuzzleObject = windowPuzzle;

		Button[] buttons = windowPuzzle.GetComponentsInChildren<Button> ();

		foreach (Button b in buttons) {
			if (b.name != "Clear") {
				Button bb = b;
				b.onClick.AddListener(()=> { WindowPuzzleButton(bb); } );
			}
		}


	}

	public void WindowPuzzleButton(Button b){
		windowPuzzlePanel.text += "" + b.name;

		if (windowPuzzlePanel.text.ToLower() == PuzzleManager.Instance.curPuzzle.solution) {
			PuzzleManager.Instance.CheckSolution (windowPuzzlePanel.text);
			print ("CORRECT! "+windowPuzzlePanel.text.ToLower());
			windowPuzzle.SetActive (false);
		}

	}

	public void WindowPuzzleClear(){
		windowPuzzlePanel.text = "";
	}



}
