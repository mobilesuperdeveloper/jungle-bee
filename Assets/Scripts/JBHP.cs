using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
using JBClasses;
public class JBHP : MonoBehaviour {

	public Image HpBar;
	public static float cur_hp;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		HpBar.GetComponent<Image> ().fillAmount = cur_hp;
	}
}
