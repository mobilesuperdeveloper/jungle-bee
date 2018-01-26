using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JBClasses;
using dataread;
public class JBParticleObj : MonoBehaviour {

	void Start()
	{
		playparticle ();
		DestroyObject(gameObject,0.3f);
	}
	void Update()
	{

	}
	// Use this for initialization
	public void playparticle()
	{
		string path;
		path="Atlas/"+JBDB.characterName+"/BulletParticle/"+JBDB.characterName+"Particle0";
		GetComponent<Image> ().sprite = Resources.Load<Sprite> (path);
		GetComponent<JBSpriteAnim> ().enabled = true;
		GetComponent<JBSpriteAnim> ().Loop = false;
		GetComponent<JBSpriteAnim> ()._prefix = JBDB.characterName + "Particle";
		path = "Atlas/" + JBDB.characterName + "/BulletParticle";
		GetComponent<JBSpriteAnim> ()._folderName = path;
		GetComponent<JBSpriteAnim> ().frames = 4;
	}
}
