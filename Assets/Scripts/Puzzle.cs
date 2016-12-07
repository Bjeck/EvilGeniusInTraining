using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Puzzle : MonoBehaviour {

	public string puzzleName = "";
	public MentorID mentor;
	public List<roomSpecs> requirements = new List<roomSpecs>();

	public string agenthelp = "";
	public string evilhelp = "";

	public string instruction = "";
	public string solution;
	public string successclue;
	public string failureclue;
	public float timeToComplete = 10f;

	public bool hasRun = false;

	public int lives = -1; //if -1 = puzzle has no lives system.

	public List<PuzzleInstructionPanel> instructionPanels;

	public Action<Puzzle> onHelpAgent;
	public Action<Puzzle> onHelpEvil;

	public Action<Puzzle> onPlay;



	//function that finds instructionmanager for onclick

}