using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MentorInstantiator : MonoBehaviour {

	[SerializeField] GameObject MentorPrefab;
	[SerializeField] Transform mentorParent; 
	public List<Mentor> possibleMentors = new List<Mentor>();

	public List<Mentor> CreateMentors(){
		possibleMentors.Clear ();

		GameObject newMentor = (GameObject)Instantiate(MentorPrefab,mentorParent);
		Mentor nm = newMentor.GetComponent<Mentor> ();
		nm.mentorName = "Mr. Freeze";
		nm.description = "Master of all things frozen!";
		nm.ID = MentorID.MrFreeze;
		possibleMentors.Add (nm);

		newMentor = (GameObject)Instantiate(MentorPrefab,mentorParent);
		nm = newMentor.GetComponent<Mentor> ();
		nm.mentorName = "Ernst Blofeld";
		nm.description = "Eternally stroking his cat.";
		nm.ID = MentorID.Blofeld;
		possibleMentors.Add (nm);

		return possibleMentors;
	}
}
