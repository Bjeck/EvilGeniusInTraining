using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class House : MonoBehaviour {

	public PuzzleManager pm;

	public List<Room> rooms = new List<Room>();


	//I'M ADDING THE WRONG ROOM. 
	//I need to add the one I spawn. Not the one from the button.

	public void SetRooms(List<Room> newRooms){
		List<Room> roomsToRemove = new List<Room>();
		foreach(Room r in rooms){
			if(newRooms.Exists(x=>x==r)){
				continue;
			}
			roomsToRemove.Add(r);
		}

		for (int i = 0; i < roomsToRemove.Count; i++) {
			rooms.Remove(roomsToRemove[i]);
		}

		List<Room> roomsToAdd = new List<Room>();
		if(rooms.Count > 0){
			foreach(Room r in newRooms){
				if(rooms.Exists(x=>x==r)){
					continue;
				}
				print(r.type+" now adding");

				Room room = r;
				//room.type = r.type;
				//room.roomSpecifications = r.roomSpecifications;

				roomsToAdd.Add(room);
			}

			rooms.AddRange(roomsToAdd);



		}
		else{
			foreach(Room r in newRooms){
				Room room = r;
				rooms.Add(room);
			}
		}
	}


}

//probably stop spawning the rooms now? Just add them to the list instead. 
