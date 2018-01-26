using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JBClasses;
using dataread;
public class JBSetting : MonoBehaviour {
	public GameObject Option;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Close ()
	{
		Option.SetActive (false);
	}

	public void Open ()
	{
		Option.SetActive (true);
	}
	public void SettingFlag(GameObject obj)
	{
		if(obj.name=="Music")
		{
			JBDB.MusicFlag=obj.GetComponent<Toggle>().isOn;
		}
		else if(obj.name=="Sound")
		{
			JBDB.SoundFlag=obj.GetComponent<Toggle>().isOn;
		}
		else if(obj.name=="Hand")
		{
			JBDB.HandFlag=obj.GetComponent<Toggle>().isOn;
		}
	}
}
