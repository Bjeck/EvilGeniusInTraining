using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Room : MonoBehaviour {

	public List<Puzzle> puzzles = new List<Puzzle>();
	public List<roomSpecs> roomSpecifications = new List<roomSpecs>();

	public roomType type;



	public void AddPuzzle(Puzzle p){
		puzzles.Add(p);
	}
}
