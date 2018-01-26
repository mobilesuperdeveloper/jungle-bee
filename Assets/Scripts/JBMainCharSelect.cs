using UnityEngine;
using System.Collections;
using dataread;
public class JBMainCharSelect : MonoBehaviour {
	public GameObject SelectDlg;
	public GameObject coinDlg;
	bool CharNameIsSelected;
	private string charname;

	// Use this for initialization
	void Start () 
	{
		CharNameIsSelected = false;
		CharNameIsSelected = true;
		charname = "JungleBee";
	}
	void OnEnable()
	{
		CharNameIsSelected=true;
		charname="JungleBee";
		SelectDlg.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
		for(int i=0;i<3;i++)
		{
			SelectDlg.transform.GetChild(i+1).GetChild(1).gameObject.SetActive(false);//checkIcon;
			if(JBDB.enableChars[i]==1)
			{
				SelectDlg.transform.GetChild(i+1).GetChild(2).gameObject.SetActive(false);//buyButton;
			}
		}
	}
	// Update is called once per frame
	void Update () 
	{

	}
	void PresentCheck(int number)
	{
		for(int i=0;i<4;i++)
		{
			SelectDlg.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
		}
		SelectDlg.transform.GetChild(number).GetChild(1).gameObject.SetActive(true);
	}
	public void CharSelect(GameObject obj)
	{
		if (obj.name == "JungleBee") 
		{
			CharNameIsSelected=true;
			charname="JungleBee";
			PresentCheck(0);
		}
		if (obj.name == "CarpenterBee") 
		{
			if(JBDB.enableChars[0]==1)
			{
				PresentCheck(1);
				CharNameIsSelected=true;
				charname="CarpenterBee";
			}

		}
		if (obj.name == "BumbleBee") 
		{
			if(JBDB.enableChars[1]==1)
			{
				PresentCheck(2);
				CharNameIsSelected=true;
				charname="BumbleBee";
			}

		}
		if (obj.name == "KillerBee") 
		{
			if(JBDB.enableChars[2]==1)
			{
				PresentCheck(3);
				CharNameIsSelected=true;
				charname="KillerBee";
			}
		}
	}
	public void Onclick(GameObject obj)
	{
		if (CharNameIsSelected==true&&obj.name == "Ok") 
		{
			JBDB.gamestate=true;
			JBDB.characterName=charname;
			Application.LoadLevel("Map_Scene");
		} 
		if(obj.name=="Cancel") {
			gameObject.SetActive(false);
		}
		if(obj.name=="Coin")
		{
			coinDlg.SetActive(true);
		}
	}
	public void buyClick(GameObject obj)
	{
		int cost = JBDB.CharCost [obj.name];
		if (JBDB.Coin >= cost) 
		{
			obj.transform.GetChild (2).gameObject.SetActive (false);
			JBDB.Coin-=cost;
			EnableChars(obj.name);
		}
		else 
		{
			coinDlg.SetActive(true);
		}
	}
	void EnableChars(string name)
	{
		if(name=="CarpenterBee")
		{
			JBDB.enableChars[0]=1;
		}
		else if(name=="BumbleBee")
		{
			JBDB.enableChars[1]=1;
		}
		else if(name=="KillerBee")
		{
			JBDB.enableChars[2]=1;
		}
	}
}
