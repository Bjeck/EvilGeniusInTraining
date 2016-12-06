using UnityEngine;
using System.Collections;

public class DateManager : MonoBehaviour {

	System.DateTime Eventdate;

	// Use this for initialization
	void Start () {
	
	}

	public void PickDate(){

		Eventdate = System.DateTime.Now;

	}


}
