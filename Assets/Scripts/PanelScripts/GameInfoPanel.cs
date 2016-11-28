using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameInfoPanel : MonoBehaviour {

	string s = "";
	[SerializeField] Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
		UpdateText ();
	}

	public void UpdateText(){

		s = "Evil Genius: " + PuzzleManager.Instance.teamEvil.points+" Points\n";
		s += "Agents: " + PuzzleManager.Instance.teamAgent.points + " Points\n";

		text.text = s;
	}
}
