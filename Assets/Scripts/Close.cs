
using UnityEngine;
using System.Collections;

public class Close : MonoBehaviour {
	public GameObject Map;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Off ()
	{
		//Map.SetActive (false);
		Application.LoadLevel ("Start_Scene");
	}
}
