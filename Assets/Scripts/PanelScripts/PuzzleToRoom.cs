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

		foreach (Room r in PuzzleManager.Instance.house.rooms) {
			GameObject g = (GameObject)Instantiate(roomButtonPrefab);
			g.transform.SetParent(roomParent,false);
			Text[] texts = g.GetComponentsInChildren<Text>();
			texts[0].text = r.type.ToString();

			//			texts[1] = REQUIREMENTS

			Room room = r;
			g.GetComponent<Button> ().onClick.AddListener(()=> { TryAssignPuzzleToRoom(room); } );
			
		}

		print (PuzzleManager.Instance.house.rooms[0]+" ROOM ");

	}

	public void TryAssignPuzzleToRoom(Room r){
		print ("Try Assigning to "+r.type.ToString());
		List<roomSpecs> failedReqs = new List<roomSpecs>();
		foreach(roomSpecs s in InstructionManager.Instance.selectedPuzzle.requirements){
			if(r.roomSpecifications.Exists(x=>x==s)){
				//all good!
			}
			else{
				failedReqs.Add(s);
			}
		}

		if(failedReqs.Count > 0){
			print ("failed reqs ");
			SpawnPuzzleDoesNotFitWarning(failedReqs, r);
		}
		else {
			//if we get here, we assume that the puzzle fits the room.
			r.AddPuzzle(InstructionManager.Instance.selectedPuzzle); //THIS IS ALSO WRONG THEN
			PuzzleManager.Instance.AddPuzzleToGame (InstructionManager.Instance.selectedPuzzle);
			InstructionManager.Instance.AccessPanel (4);
			InstructionManager.Instance.selectedPuzzle = null;
			print ("sucess. added puzzle to room.");
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

		Button[] buttons = g.GetComponentsInChildren<Button> ();
		Room rinHouse = PuzzleManager.Instance.house.rooms.Find (x => x == r);
		buttons[0].onClick.AddListener(()=> { print("BUTTON PRESSED"); rinHouse.roomSpecifications.AddRange(failedreqs); TryAssignPuzzleToRoom(rinHouse); InstructionManager.Instance.DestroyGameObject(g); } );
		buttons[1].onClick.AddListener(()=> { InstructionManager.Instance.DestroyGameObject(g); } ); //that saved a whole script! Wooh!

	}

}


//Now i need to create the dynamic puzzle list
//to avoid readding puzzles
//so we only show the puzzles that aren't assigned to a room etc.