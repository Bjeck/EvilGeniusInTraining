using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class InstructionManager : Singleton<InstructionManager> {
	protected InstructionManager() { }

	[SerializeField] GameObject roomPrefab;
	[SerializeField] GameObject puzzleButtonPrefab;
	[SerializeField] Transform puzzleButtonParent;
	[SerializeField] Transform puzzleChainParent;
	[SerializeField] GameObject roomParent;
	[SerializeField] GameObject gamePanel;
	[SerializeField] GameObject instructionPanel;

	//List of Panels (For recall, if user wants to edit something further.)
	public List<GameObject> panels = new List<GameObject>();

	[SerializeField] PuzzleToRoom puzzleToRoom;

	public Puzzle selectedPuzzle;

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

	public Room SpawnRoomOnHouse(roomType typ, Transform parent){
		GameObject nr = (GameObject)Instantiate(roomPrefab,parent);
		Room room = nr.GetComponent<Room>();
		room.type = typ;
		return room;
	}

	public void RemoveRoomFromHouse(Room r){
		PuzzleManager.Instance.house.rooms.Remove(r);
		Destroy(r.gameObject);
	}

	public void HandlePuzzleSelection(Button b){
		Puzzle puz = b.GetComponent<Puzzle>();
		selectedPuzzle = PuzzleManager.Instance.allPuzzles.Find(x=>x.puzzleName==puz.puzzleName);
		AccessPanel(5);
		print("Puzzle "+puz.puzzleName+" handling");
		//fill in stuff in panel based on puzzle.
		puzzleToRoom.FillInRooms();
	}

	public void FillInPuzzles(){
		foreach (Puzzle p in PuzzleManager.Instance.allPuzzles) {
			GameObject g = (GameObject)Instantiate(puzzleButtonPrefab);
			g.transform.SetParent(puzzleButtonParent,false);
			Text[] texts = g.GetComponentsInChildren<Text>();
			texts[0].text = p.puzzleName.ToString();
			Puzzle pp = g.GetComponent<Puzzle> ();
			pp.requirements = p.requirements;
			pp.puzzleName = p.puzzleName;
//			print (p.puzzleName+" " + p.requirements.Count);

			Button b = g.GetComponent<Button> ();
			b.onClick.AddListener(()=> { HandlePuzzleSelection(b); } );
		}
	}

	public void FillInPuzzleChain(){
		foreach (Puzzle p in PuzzleManager.Instance.gamePuzzles) {
			GameObject g = (GameObject)Instantiate(puzzleButtonPrefab);
			g.transform.SetParent(puzzleChainParent,false);
			Text[] texts = g.GetComponentsInChildren<Text>();
			texts[0].text = p.puzzleName.ToString();
			Puzzle pp = g.GetComponent<Puzzle> ();
			pp.requirements = p.requirements;
			pp.puzzleName = p.puzzleName;

		//	Button b = g.GetComponent<Button> ();
			//b.onClick.AddListener(()=> { HandlePuzzleSelection(b); } );
		}
	}



	public void StartGame(){
		gamePanel.SetActive (true);
		instructionPanel.SetActive (false);

		PuzzleManager.Instance.BeginGame ();
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
		
	public void DestroyGameObject(GameObject objToDest){
		Destroy (objToDest);
	}

}
