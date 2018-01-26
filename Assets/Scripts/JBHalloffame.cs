using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class JBHalloffame : MonoBehaviour {
	public GameObject HallOfFame;
	public GameObject[] Obj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Close ()
	{
		HallOfFame.SetActive (false);
	}
	
	public void Open ()
	{
		HallOfFame.SetActive (true);
	}
}
