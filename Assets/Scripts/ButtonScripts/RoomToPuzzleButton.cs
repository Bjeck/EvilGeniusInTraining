using UnityEngine;
using System.Collections;

public class RoomToPuzzleButton : MonoBehaviour {

	public Room roomIAm;

	public void TryAssignPuzzleToRoom(){

		InstructionManager.Instance.TryAssignPuzzleToRoom(roomIAm);

	}

}
