using UnityEngine;
using System.Collections;

public class JBBossParticleObj : MonoBehaviour {
	// Use this for initialization
	void Start () 
	{
		playparticle ();
		DestroyObject(gameObject,0.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
//		transform.localScale = new Vector2 (transform.localScale.x + 0.05f * 1, transform.localScale.y + 0.05f * 1);
//		if (transform.localScale.x >= 2.5) {
//			Destroy(gameObject);
//		}
	}
	public void playparticle()
	{
		transform.position = GameObject.Find ("Boss").transform.position;
		GetComponent<JBSpriteAnim> ().enabled = true;
		GetComponent<JBSpriteAnim> ().Loop = false;
	}
}
