using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class JBPower : MonoBehaviour {

	public GameObject PowerBar;
	public GameObject Bee;
	string State;
	float speed; 
	public static float energy,limit;

	// Use this for initialization
	void Start () 
	{
//		speed = 0.002f;
//		limit = 0.1f;
//		State = "Down";
	}

	// Update is called once per frame
	void Update () 
	{
		//energy = Bee.GetComponent<JBMainCharacterObj> ().m_CharPower;
//		if (PowerBar.GetComponent<Image> ().fillAmount == 0) StartCoroutine ("Up");
//		if (PowerBar.GetComponent<Image> ().fillAmount == 1) StartCoroutine ("Down");
//		if ((State == "Down") && (speed > 0))
//			speed *= -1.0f;
//		if ((State == "Up") && (speed < 0))
//			speed *= -1.0f;
//		energy = PowerBar.GetComponent<Image> ().fillAmount;
//		if (energy <= limit)
//			Bee.rigidbody.isKinematic = false;
//		else
//			Bee.rigidbody.isKinematic = true;
//		PowerBar.GetComponent<Image> ().fillAmount += speed;
		PowerBar.GetComponent<Image> ().fillAmount = energy;

	}

//	IEnumerator Up()
//	{
//		yield return new WaitForSeconds (2.0f);
//		State = "Up";
//	}
//
//	IEnumerator Down()
//	{
//		yield return new WaitForSeconds (2.0f);
//		State="Down";
//	}
}
