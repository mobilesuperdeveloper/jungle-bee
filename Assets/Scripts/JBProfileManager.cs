using UnityEngine;
using System.Collections;
using dataread;
using System.IO;
using UnityEngine.UI;
using JBClasses;
public class JBProfileManager : MonoBehaviour {

	private GameObject _windowDecided;
	private GameObject _windowStart;
	public GameObject _ProfileName;
	public GameObject _ProfilePhoneNum;
	public Text decidedName;
	public Text decidedNumber;
	private bool IsFirstEntered;
	JBUserProfile k;
	// Use this for initialization
	void Start () 
	{
		_windowDecided = transform.Find ("Window(decided)").gameObject;
		_windowStart = transform.Find ("Window(start)").gameObject;
		string filepath = Application.persistentDataPath + "/savedata.txt";
		if (!File.Exists (filepath)) 
		{
			IsFirstEntered = true;
			JBDB.enableChars=new int[3];
			JBDB.ReadGameDataText ();
		} 
		else 
		{
			IsFirstEntered = false;
			JBDB.enableChars=new int[3];
			JBDB.ReadGameDataText ();
			WriteYourProfile ();
		}
		SelectRightWindow (IsFirstEntered);
	}
	// Update is called once per frame
	void Update () {
	
	}
	void SelectRightWindow(bool isFirst)
	{
		if (isFirst) 
		{
			_windowDecided.SetActive(false);
			_windowStart.SetActive(true);

		} 
		else 
		{
			_windowDecided.SetActive(true);
			_windowStart.SetActive(false);
		}
	}
	void WriteYourProfile()
	{
		decidedName.text = JBDB._profile.YourID;
		decidedNumber.text = JBDB._profile.YourPhoneNumber;
	}
	public void ProfileDataInput()
	{
		string temp1=_ProfileName.GetComponent<Text> ().text;
		string temp2=_ProfilePhoneNum.GetComponent<Text> ().text;
		if(temp1=="" || temp2=="")
		{
			return;
		}
		else
		{
			JBDB._profile = new JBUserProfile ();
			JBDB._profile.YourID=_ProfileName.GetComponent<Text> ().text;
			JBDB._profile.YourPhoneNumber = _ProfilePhoneNum.GetComponent<Text> ().text;
			Application.LoadLevel ("Start_Scene");
		}
	}
}
