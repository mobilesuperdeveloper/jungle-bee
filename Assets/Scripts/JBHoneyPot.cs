using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
public class JBHoneyPot : MonoBehaviour {
	public GameObject Energy;
	int Price,  Score;
	public static int Amount;
	void Start ()
	{
		Price = 2000;
		Amount = JBDB.items.HoneyCount;
		Score = 50000;
	}
	void Update ()
	{

		Amount = JBDB.items.HoneyCount;
		if(Amount<0)
		{
			Amount=0;
		}
		GetComponentInChildren<Text> ().text = Amount.ToString ();
	}
	public void Use ()
	{
		if (Amount > 0) 
		{
			--Amount;
			JBDB.items.HoneyCount--;
			Score -= Price;
			JBMainCharacterObj.m_CharHP += 100;

			//Energy.GetComponent<Image>().fillAmount = 1;
		}
	}
}
