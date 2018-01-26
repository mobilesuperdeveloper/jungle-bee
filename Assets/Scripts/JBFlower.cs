using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
public class JBFlower : MonoBehaviour {
	int Price, Effect, Amount, Health, Score;
	public GameObject LifeBar;
	void Start ()
	{
		Price = 2500;
		Effect = 500;
		Health = 10000;
		Score = 50000;
		Amount = JBDB.items.FlowerCount;
	}
	void Update ()
	{
		Amount = JBDB.items.FlowerCount;
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
			JBDB.items.FlowerCount--;
			Score -= Price;
			Health += Effect;
			JBMainCharacterObj.m_CharPower += 3;

			//LifeBar.GetComponent<Image> ().fillAmount = 1;
		}

	}
}
