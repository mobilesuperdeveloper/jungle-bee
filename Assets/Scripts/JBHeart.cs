using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
public class JBHeart : MonoBehaviour {
	int Price, Effect, Amount, Score, BoxHealth;

	void Start ()
	{
		Price = 3000;
		Effect = 2000;
		//Amount = JBDB.kissnum;
		Amount = JBDB.items.HeartCount;
		Score = 50000;
		BoxHealth = 10000;
	}
	void Update ()
	{
		Amount = JBDB.items.HeartCount;
		GetComponentInChildren<Text> ().text = Amount.ToString ();
	}
	public void Use ()
	{
		if (Amount > 0) 
		{
			--Amount;
			JBDB.items.HeartCount--;
			Score -= Price;

			BoxHealth += Effect;

            JBDB.Lives++;
		}
	}
}
