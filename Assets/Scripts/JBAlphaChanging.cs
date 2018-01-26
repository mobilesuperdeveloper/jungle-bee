using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JBAlphaChanging : MonoBehaviour {
	public Image _IMG;
	//float settingTime;
	float m_Tick;
	float loops;
	// Use this for initialization
	void Start () 
	{
		//settingTime = 0.2f;
		m_Tick = 0;
		loops = 2;
	}
	void OnEnable()
	{
		//settingTime = 0.2f;
		m_Tick = 0;
		loops = 2;
	}
	// Update is called once per frame
	void Update () 
	{

		if(loops>=0)
		{
			m_Tick += Time.deltaTime*2;
			if(m_Tick>0.5f && m_Tick<=1)
			{
				_IMG.color=new Vector4(1,1,1,0);
			}
			else if(m_Tick>1)
			{
				_IMG.color=new Vector4(1,1,1,1);
				m_Tick=0;
				loops--;
			}
		}
	}
	void OnDisable()
	{
		_IMG.color = new Vector4 (1,1,1,1);
	}
}
