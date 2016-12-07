using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PuzzleToRoom : MonoBehaviour {

	[SerializeField] InstructionManager im;

	[SerializeField] GameObject roomButtonPrefab;
	[SerializeField] GameObject puzzleDoesNotFitWarningPrefab;
	[SerializeField] Transform roomParent;
	[SerializeField] Text puzzleText;


	public void FillInRooms(){

		foreach (Transform g in roomParent) { //clearing the list of rooms, in case it was filled already
			Destroy (g.gameObject);
		}

		foreach (Room r in PuzzleManager.Instance.house.rooms) {
			GameObject g = (GameObject)Instantiate(roomButtonPrefab);
			g.transform.SetParent(roomParent,false);
			Text[] texts = g.GetComponentsInChildren<Text>();
			texts[0].text = r.type.ToString();

			texts [1].text = "";
			foreach(roomSpecs s in r.roomSpecifications){
				texts[1].text += s.ToString()+", ";
			}
			//			texts[1] = REQUIREMENTS

			Room room = r;
			g.GetComponent<Button> ().onClick.AddListener(()=> { TryAssignPuzzleToRoom(room); } );
			
		}

		puzzleText.text = InstructionManager.Instance.selectedPuzzle.puzzleName + "\n\nNeeds: ";
		foreach (roomSpecs s in InstructionManager.Instance.selectedPuzzle.requirements) {
			puzzleText.text += s.ToString () + ", ";
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
			PuzzleManager.Instance.AddPuzzleToGame (InstructionManager.Instance.selectedPuzzle, r);
			InstructionManager.Instance.AccessPanel (InstructionManager.Instance.panels.FindIndex(x=>x.name=="Panel6Puzzles"));
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
		buttons[0].onClick.AddListener(()=> { rinHouse.roomSpecifications.AddRange(failedreqs); TryAssignPuzzleToRoom(rinHouse); InstructionManager.Instance.DestroyGameObject(g); } );
		buttons[1].onClick.AddListener(()=> { InstructionManager.Instance.DestroyGameObject(g); } ); //that saved a whole script! Wooh!

	}

}


//Now i need to create the dynamic puzzle list
//to avoid readding puzzles
//so we only show the puzzles that aren't assigned to a room etc.