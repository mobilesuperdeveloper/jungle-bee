using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
using JBClasses;

public class JBCoinManager : MonoBehaviour {
	public GameObject display;

	// Use this for initialization
	void Start () 
	{
		
	}

	// Update is called once per frame
	void Update () 
	{
		if(display!=null)
		{
			display.GetComponent<Text> ().text = JBDB.Coin.ToString();
		}
	}

}
