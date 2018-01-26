using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
using JBClasses;
public class JBScoreManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<Text> ().text = JBDB.TotalScore.ToString();
	}

}
