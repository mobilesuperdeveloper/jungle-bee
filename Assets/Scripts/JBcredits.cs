using UnityEngine;
using System.Collections;

public class JBcredits : MonoBehaviour {
	public GameObject Credits;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Close ()
	{
		Credits.SetActive (false);
	
	}
	
	public void Open ()
	{
		Credits.SetActive (true);
	}
}
