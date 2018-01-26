using UnityEngine;
using System.Collections;
using JBClasses;

public class BG_Alpha : MonoBehaviour {

    public GameObject m_MainUI;
    public GameObject m_Blossom;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HideMainUI()
    {
        m_MainUI.SetActive(false);
    }

    public void StartShowBlossom()
    {
        m_Blossom.SetActive(true);
    }
}
