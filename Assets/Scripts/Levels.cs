using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
using JBClasses;

public class Levels : MonoBehaviour 
{
	bool[] IsLevelCompleted = new bool[51];
	Image[] Level = new Image[51];
	//static JBGameLoading _loading;
	// Use this for initialization
	void Start () 
	{
		for (int i=0; i<50; i++) 
		{
			if(i<=JBDB.levelint-1)
			{
				IsLevelCompleted[i] = true;
			}
			else
			{
				IsLevelCompleted[i]=false;
			}
		}
		Transform scroll = transform.GetChild (0);
		Level = scroll.GetComponentsInChildren<Image> ();
	}
	// Update is called once per frame
	void Update () 
	{
		for (int i=1; i<51; i++)//because Level[0] is scroll object in game;
		{
			if(!IsLevelCompleted[i-1])
			{
				Level[i].color=new Vector4(1f,1f,1f,0.5f);
			}
		}
	}
	public void onclick(GameObject obj)
	{
		string obj_name = obj.name;
		int level = System.Convert.ToInt16 (obj_name);
		if(IsLevelCompleted[level-1])
		{
			JBDB.levelint = level;
			Invoke("GotoLoadingScene",0.5f);
		}
	}
	void GotoLoadingScene()
	{
		Application.LoadLevel("Loading_Scene2");
	}
}
