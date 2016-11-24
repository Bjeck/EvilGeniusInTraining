using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PuzzleToRoom : MonoBehaviour {

	[SerializeField] InstructionManager im;

	[SerializeField] GameObject roomButtonPrefab;
	[SerializeField] GameObject puzzleDoesNotFitWarningPrefab;
	[SerializeField] Transform roomParent;


	public void FillInRooms(){
		foreach(Room r in PuzzleManager.Instance.house.rooms){
			GameObject g = (GameObject)Instantiate(roomButtonPrefab);
			g.transform.SetParent(roomParent,false);
			Text[] texts = g.GetComponentsInChildren<Text>();
			texts[0].text = r.type.ToString();
//			texts[1] = REQUIREMENTS

			g.GetComponent<RoomToPuzzleButton>().roomIAm = r;
		}

	}


	public void SpawnPuzzleDoesNotFitWarning(List<roomSpecs> failedreqs, Room r){
		GameObject g = (GameObject)Instantiate(puzzleDoesNotFitWarningPrefab);
		g.transform.SetParent(transform,false);

		string failedString = "";
		foreach(roomSpecs s in failedreqs){
			failedString += "- "+s.ToString()+"\n";
		}

		g.GetComponentInChildren<Text>().text = failedString;
		g.GetComponent<PuzzleDoesNotFitRoomWarning>().room = r;
		g.GetComponent<PuzzleDoesNotFitRoomWarning>().failedSpecs = failedreqs; // This is dumb and slow. Look below for a better solution.


	}

}



//I might be able to do the puzzle script hting with EventSystems. instead. Or Listeners! Like I did in Wizard. That'd make way more sense, right? Than making customs scripts for each.
//Yes. Yes it would. Hmm. will research when I come home.