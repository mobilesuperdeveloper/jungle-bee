using UnityEngine;
using System.Collections;
using System.IO;
using dataread;
using UnityEngine.UI;
using System.Collections.Generic;

public class JBItemShop : MonoBehaviour {
	public GameObject ItemShop;
	public GameObject score;
	public GameObject coinDlg;
	string SelectedBonous;
	string datascore;
	int coin;
	// Use this for initialization
	void Start () 
	{
		datascore = JBDB.TotalScore.ToString ();
		coin = JBDB.Coin;
		score.GetComponent<Text> ().text =datascore;
	}
	
	// Update is called once per frame
	void Update () 
	{

		datascore = JBDB.TotalScore.ToString ();
		coin = JBDB.Coin;
		score.GetComponent<Text> ().text =datascore;
	}

	public void Open ()
	{
		ItemShop.SetActive (true);
	}

	public void Close ()
	{
		ItemShop.SetActive (false);
	}
	public void BonousSelect(GameObject obj)
	{
		string _item_name;
		int _cost;
		if (obj.name == "JBHoneyPot") {

			_item_name="Honey";
			_cost=JBDB.ItemCost[_item_name];
			if(CanBuyThisItem(_cost))
			{
				JBDB.items.HoneyCount++;
			}
			else
			{
				coinDlg.SetActive(true);
			}
		}
		if (obj.name == "JBFlower") {
			_item_name="Flower";
			_cost=JBDB.ItemCost[_item_name];
			if(CanBuyThisItem(_cost))
			{
			JBDB.items.FlowerCount++;
			}
			else
			{
				coinDlg.SetActive(true);
			}
		}
		if (obj.name == "JBKiss") {
			_item_name="Kiss";
			_cost=JBDB.ItemCost[_item_name];
			if(CanBuyThisItem(_cost))
			{
				JBDB.items.KissCount++;
			}
			else
			{
				coinDlg.SetActive(true);
			}
		}
		if (obj.name == "JBHeart") {
			_item_name="Heart";
			_cost=JBDB.ItemCost[_item_name];
			if(CanBuyThisItem(_cost))
			{
			 	JBDB.items.HeartCount++;
			}
			else
			{
				coinDlg.SetActive(true);
			}
		}
	}
	private bool CanBuyThisItem(int cost)
	{
		if (coin >= cost) 
		{
			coin -= cost;
			JBDB.Coin = coin;
			return true;
		}
		return false;
	}
}
