using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
public class JBKiss : MonoBehaviour {
	int Price, Effect, Amount, Score, LevelScore;

	void Start ()
	{
		Price = 2500;
		Effect = 1000;
		//Amount = JBDB.flowernum;
		Amount = JBDB.items.KissCount;
		LevelScore = 10000;
		Score = 50000;
	}

	void Update ()
	{
		Amount = JBDB.items.KissCount;
		GetComponentInChildren<Text> ().text = Amount.ToString ();
	}

	public void Use ()
	{
		if (Amount > 0) 
		{
			--Amount;
			JBDB.items.KissCount--;
			Score -= Price;
			LevelScore += Effect;
			JBMainCharacterObj.IsInShield=true;
			//JBMainCharacterObj._shield.SetActive(true);
		}
	}
}
