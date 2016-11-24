using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class InstructionManager : Singleton<InstructionManager> {
	protected InstructionManager() { }

	[SerializeField] GameObject roomPrefab;

	[SerializeField] GameObject roomParent;

	//List of Panels (For recall, if user wants to edit something further.)
	public List<GameObject> panels = new List<GameObject>();

	[SerializeField] PuzzleToRoom puzzleToRoom;

	Puzzle selectedPuzzle;

	// Use this for initialization
	void Start () {
	
	}

	public void AssignRooms(){
		List<Room> rooms = roomParent.GetComponentsInChildren<Room>().ToList();
		List<Room> roomsInHouse = new List<Room>();

		foreach(Room r in rooms){
			if(r.GetComponentInChildren<Toggle>().isOn){
				roomsInHouse.Add(r);
			}
		}

		PuzzleManager.Instance.house.SetRooms(roomsInHouse);
		print("rooms assigned "+PuzzleManager.Instance.house.rooms.Count);
	}

	public Room SpawnRoom(roomType typ, Transform parent){
		GameObject nr = (GameObject)Instantiate(roomPrefab,parent);
		Room room = nr.GetComponent<Room>();
		room.type = typ;
		return room;
	}

	public void RemoveRoom(Room r){
		PuzzleManager.Instance.house.rooms.Remove(r);
		Destroy(r.gameObject);
	}

	public void HandlePuzzleSelection(Button b){
		Puzzle puz = b.GetComponent<Puzzle>();
		selectedPuzzle = puz;
		AccessPanel(5);
		print("Puzzle "+puz.name+" handling");
		//fill in stuff in panel based on puzzle.
		puzzleToRoom.FillInRooms();
		
	}


	public void TryAssignPuzzleToRoom(Room r){
		List<roomSpecs> failedReqs = new List<roomSpecs>();
		foreach(roomSpecs s in selectedPuzzle.requirements){
			if(r.roomSpecifications.Exists(x=>x==s)){
				//all good!
			}
			else{
				failedReqs.Add(s);
			}
		}

		if(failedReqs.Count > 0){
			puzzleToRoom.SpawnPuzzleDoesNotFitWarning(failedReqs, r);
		}
		else {
			//if we get here, we assume that the puzzle fits the room.
			r.AddPuzzle(selectedPuzzle);
		}


	}



	public void AccessPanelFromButton(GameObject panelToAccess){
		AccessPanel(panels.IndexOf(panelToAccess));

	}

	public void AccessPanel(int id){
		for (int i = 0; i < panels.Count; i++) {
			if(i==id){
				panels[i].SetActive(true);
			}
			else{
				panels[i].SetActive(false);
			}
		}
	}


}
