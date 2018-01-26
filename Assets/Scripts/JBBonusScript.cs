using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class JBBonusScript : MonoBehaviour {
	private float m_ItemSpeed;
	public string m_Itemname;
	// Use this for initialization
	void Start () 
	{
		m_ItemSpeed = 200f;

		//Sprite[] _sprites=new Sprite[10];
		Sprite[] _sprite_IMG=new Sprite[20];
		//_sprites=Resources.LoadAll<Sprite>("Atlas/Bonus/bonus");
		_sprite_IMG = Resources.LoadAll<Sprite> ("Image/common15x");
		if(m_Itemname=="flower")
		{
			transform.GetComponent<Image>().sprite=Resources.Load<Sprite>("Image/Flower");;
		}
		else if(m_Itemname=="heart")
		{
			//transform.GetComponent<Image>().sprite=_sprites[1];
			transform.GetComponent<Image>().sprite=_sprite_IMG[5];
		}
		else if(m_Itemname=="honey")
		{
			transform.GetComponent<Image>().sprite=Resources.Load<Sprite>("Image/HoneyPot");;

		}
		else if(m_Itemname=="kiss")
		{
			//transform.GetComponent<Image>().sprite=_sprites[3];
			transform.GetComponent<Image>().sprite=Resources.Load<Sprite>("Image/silver_necklace");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 position = transform.position;
		position = new Vector2 (position.x - m_ItemSpeed * Time.deltaTime, position.y);
		transform.position = position;
		if (transform.position.x< -1.8f) {
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter(Collider obj)
	{

	}

}
