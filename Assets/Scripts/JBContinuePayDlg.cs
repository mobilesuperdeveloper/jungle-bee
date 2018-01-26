using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
using JBClasses;

public class JBContinuePayDlg : MonoBehaviour {
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

    public void PayContinue()
    {
        JBDB.IsPayCompleted = true;
        JBDB.levelint++;
        JBDB.SaveGameDataText();
        Application.LoadLevel("Loading_Scene2");
    }
}
