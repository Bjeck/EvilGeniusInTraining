using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Puzzle : MonoBehaviour {

	public string puzzleName = "";
	public MentorID mentor;
	public List<roomSpecs> requirements = new List<roomSpecs>();

	public string solution;
	public string outputclue;
	public float timeToComplete = 10f;

	List<string> hints = new List<string>(); //TBD
	public bool hasRun = false;




	//function that finds instructionmanager for onclick

}