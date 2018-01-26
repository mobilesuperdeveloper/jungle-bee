using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
using JBClasses;

public class JBLifeManager : MonoBehaviour 
{
	public Text _present;
	// Use this for initialization
	void Start () 
	{
		if(_present!=null)
		{
			_present.text=JBDB.Lives.ToString();
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(_present!=null)
		{
			_present.text=JBDB.Lives.ToString();
		}
	}
	public void BuyLives(GameObject btn)
	{
		////todo ////
	}
}
