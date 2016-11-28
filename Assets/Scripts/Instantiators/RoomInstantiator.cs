using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomInstantiator : MonoBehaviour {

	[SerializeField] GameObject roomPrefab;
	[SerializeField] Transform roomParent; 
	public List<Room> possibleRooms = new List<Room>();

	// Use this for initialization
	void Start () {
	
	}

	public List<Room> CreateRooms(){
		possibleRooms.Clear ();

		GameObject newRoom = (GameObject)Instantiate(roomPrefab,roomParent);
		Room nr = newRoom.GetComponent<Room> ();
		nr.type = roomType.Kitchen;
		nr.roomSpecifications.Add (roomSpecs.Sink);
		possibleRooms.Add (nr);

		newRoom = (GameObject)Instantiate(roomPrefab,roomParent);
		nr = newRoom.GetComponent<Room> ();
		nr.type = roomType.Living_Room;
		nr.roomSpecifications.Add (roomSpecs.Seating);
		possibleRooms.Add (nr);

		newRoom = (GameObject)Instantiate(roomPrefab,roomParent);
		nr = newRoom.GetComponent<Room> ();
		nr.type = roomType.Bedroom;
		nr.roomSpecifications.Add (roomSpecs.Bed);
		possibleRooms.Add (nr);

		newRoom = (GameObject)Instantiate(roomPrefab,roomParent);
		nr = newRoom.GetComponent<Room> ();
		nr.type = roomType.Bathroom;
		nr.roomSpecifications.Add (roomSpecs.Sink); nr.roomSpecifications.Add (roomSpecs.Toilet);
		possibleRooms.Add (nr);

		newRoom = (GameObject)Instantiate(roomPrefab,roomParent);
		nr = newRoom.GetComponent<Room> ();
		nr.type = roomType.Hallway;
		possibleRooms.Add (nr);

		newRoom = (GameObject)Instantiate(roomPrefab,roomParent);
		nr = newRoom.GetComponent<Room> ();
		nr.type = roomType.Other;
		possibleRooms.Add (nr);

		newRoom = (GameObject)Instantiate(roomPrefab,roomParent);
		nr = newRoom.GetComponent<Room> ();
		nr.type = roomType.Other;
		possibleRooms.Add (nr);

		newRoom = (GameObject)Instantiate(roomPrefab,roomParent);
		nr = newRoom.GetComponent<Room> ();
		nr.type = roomType.Other;
		possibleRooms.Add (nr);

		newRoom = (GameObject)Instantiate(roomPrefab,roomParent);
		nr = newRoom.GetComponent<Room> ();
		nr.type = roomType.Other;
		possibleRooms.Add (nr);


		return possibleRooms;

	}

}
