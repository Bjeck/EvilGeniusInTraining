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
	[SerializeField] GameObject gamePanel;
	[SerializeField] GameObject instructionPanel;
	[SerializeField] GameObject roomTogglePrefab;
	[SerializeField] Transform roomParent;
	[SerializeField] GameObject mentorButtonPrefab;
	[SerializeField] Transform mentorParent;
	[SerializeField] Button mentorProgressButton;
	[SerializeField] Text mentorInfo;
	[SerializeField] GameObject mentorIconPrefab;

	//List of Panels (For recall, if user wants to edit something further.)
	public List<GameObject> panels = new List<GameObject>();

	[SerializeField] PuzzleToRoom puzzleToRoom;

	public Puzzle selectedPuzzle;


	public void AssignRooms(){
		List<Room> rooms = roomParent.GetComponentsInChildren<Room>().ToList();
		List<Room> roomsInHouse = new List<Room>();

		foreach(Room r in rooms){
			if(r.GetComponentInChildren<Toggle>().isOn){
				roomsInHouse.Add(PuzzleManager.Instance.allRooms.Find(x=>x.type==r.type));
			}
		}

		PuzzleManager.Instance.house.SetRooms(roomsInHouse);
		print("rooms assigned "+PuzzleManager.Instance.house.rooms.Count);
	}

	public void HandlePuzzleSelection(Button b){
		Puzzle puz = b.GetComponent<Puzzle>();
		selectedPuzzle = PuzzleManager.Instance.allPuzzles.Find(x=>x.puzzleName==puz.puzzleName);
		AccessPanel(5);
		print("Puzzle "+puz.puzzleName+" handling");
		//fill in stuff in panel based on puzzle.
		puzzleToRoom.FillInRooms();
	}

	public void FillInMentors(){
		foreach (Mentor m in PuzzleManager.Instance.allMentors) {
			GameObject g = (GameObject)Instantiate(mentorButtonPrefab);
			g.transform.SetParent(mentorParent,false);
			Text text = g.GetComponentInChildren<Text>();
			g.GetComponent<Mentor> ().mentorName = m.mentorName;
			text.text = m.mentorName;

			Mentor mentor = m;
			g.GetComponent<Button> ().onClick.AddListener(()=> { SelectMentor(mentor); } );
		}
	}

	public void SelectMentor(Mentor m){
		PuzzleManager.Instance.selectedMentor = PuzzleManager.Instance.allMentors.Find (x => x.mentorName == m.mentorName);
		mentorInfo.text = m.mentorName+"\n\n"+m.description;
		mentorProgressButton.interactable = true;
	}

	public void FillInRooms(){
		print ("FILLING IN ROOMS");
		foreach (Room r in PuzzleManager.Instance.allRooms) {
			
			GameObject g = (GameObject)Instantiate(roomTogglePrefab);
			g.transform.SetParent(roomParent,false);
			Text text = g.GetComponentInChildren<Text>();
			g.GetComponent<Room> ().type = r.type;
			text.text = r.type.ToString();
			print ("many times "+g.name);
		}

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


			if (p.mentor == PuzzleManager.Instance.selectedMentor.ID) {
				GameObject icon = (GameObject)Instantiate(mentorIconPrefab);
				icon.transform.SetParent(g.transform,false);
				icon.GetComponent<RectTransform> ().position = (g.GetComponent<RectTransform> ().position + new Vector3(g.GetComponent<RectTransform>().localScale.x/2,0,0));
			}
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
