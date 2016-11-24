using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PuzzleDoesNotFitRoomWarning : MonoBehaviour {

	public Room room;
	public List<roomSpecs> failedSpecs = new List<roomSpecs>();


	public void MakeRoomFit(){
		PuzzleManager.Instance.house.rooms.Find(x=>x==room).roomSpecifications.AddRange(failedSpecs);

		DestroyWarningPanel();
	}

	public void DestroyWarningPanel(){
		Destroy(gameObject);
	}

}
