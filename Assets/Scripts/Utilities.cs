using UnityEngine;
using System.Collections;


public enum roomType {Kitchen, Living_Room, Bathroom, Bedroom, Hallway, Other}
public enum roomSpecs {Freezer, Sink, Seating, Toilet, Table, Double_sided_Window, Bed};

public enum MentorID {None, MrFreeze, Blofeld}


public static class Utilities {

	public static string ConvertSpaces(string s){

		return s.Replace ("_", " ");

	}


}
