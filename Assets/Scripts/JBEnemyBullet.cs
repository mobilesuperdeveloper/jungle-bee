using UnityEngine;
using System.Collections;

public class JBEnemyBullet : MonoBehaviour {

	float bulletspeed;
	Vector2 bulletDir;
	// Use this for initialization
	void Start () {
		bulletspeed = 300;
		Vector2 temp,temp2;
		temp = GameObject.Find ("JBMainChar").transform.position;
		temp2=transform.position;
		temp -= temp2;
		bulletDir = temp.normalized;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 position = transform.position;
		position += bulletDir * bulletspeed * Time.deltaTime;
		transform.position = position;
		if (transform.position.x <=-Screen.width/2) 
		{
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name == "JBMainChar")
			Destroy (gameObject);
	}
}
