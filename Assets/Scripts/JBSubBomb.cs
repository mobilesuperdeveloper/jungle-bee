using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JBSubBomb : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SubBombExplosion()
    {
        //bomb = true;
        GetComponent<Image>().enabled = false; 
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<BoxCollider>().size = new Vector3(800, 800, 1);
        //DestroyObject(gameObject, 0.7f);
    }

}
