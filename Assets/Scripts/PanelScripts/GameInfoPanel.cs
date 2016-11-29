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
		
		s = "Tokens: \n";
		s += "Evil Genius: " + PuzzleManager.Instance.teamEvil.tokens+"\n";
		s += "Agents: " + PuzzleManager.Instance.teamAgent.tokens+"\n";

		s += "\nPoints: "+"\n";
		s += "Evil Genius: " + PuzzleManager.Instance.teamEvil.points+"\n";
		s += "Agents: " + PuzzleManager.Instance.teamAgent.points+"\n";

		text.text = s;
	}
}
