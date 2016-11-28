using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class InfoPanel : MonoBehaviour {

	string s = "";
	[SerializeField] Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateText ();
	}

	public void UpdateText(){
		s = "";
		if (PuzzleManager.Instance.selectedMentor != null) {
			s += "Mentor: " + PuzzleManager.Instance.selectedMentor.mentorName+"\n\n";
		}

	    s += "Rooms:\n";
		foreach (Room r in PuzzleManager.Instance.house.rooms) {
			s += r.type.ToString () + "\n";
			foreach (Puzzle p in r.puzzles) {
				s += " - " + p.puzzleName+"\n";
			}
			foreach (roomSpecs rs in r.roomSpecifications) {
				s += "     /has " + rs.ToString()+"\n";
			}

		}
		s += "\n";


		text.text = s;
	}
}
