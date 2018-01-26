using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using dataread;
using JBClasses;

public  class JBLifeTimer : MonoBehaviour {

	public Text _watch;
	public static bool m_start;
	public static float nTick;
	//float settingTime=900;
	public static int time;//seconds;
	public static int min;
	public static int sec;
	// Use this for initialization
	void Start () 
	{
		nTick = 0;
		time = 3600;
	}
	// Update is called once per frame
	void Update () 
	{
		if(m_start==true)
		{
			string watchTime;
			nTick+=Time.deltaTime;
			if(nTick>=1)
			{
				if (time <= 0) 
				{
					JBDB.Lives=7;
                    JBDB.levelint -= 5;
                    if (JBDB.levelint < 1)
                        JBDB.levelint = 1;

					m_start=false;
					time=60;
                    Application.LoadLevel("Loading_Scene2");
					gameObject.SetActive(false);
				}
				nTick=0;
				time--;
				min=time/60;
				sec=time%60;
				if(min<10)
				{
					watchTime="0"+min;
				}
				else
				{
					watchTime=min.ToString();
				}
				if(sec<10)
				{
					watchTime+=" : 0"+sec;
				}
				else
				{
					watchTime+=" : "+sec.ToString();
				}
				if(_watch!=null){
				_watch.text=watchTime;
				}
			}
		}
	}
}
