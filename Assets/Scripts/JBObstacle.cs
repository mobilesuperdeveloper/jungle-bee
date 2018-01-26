using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JBClasses;
using dataread;

public class JBObstacle : MonoBehaviour
{
	public int m_strength;
	public ObstacleType m_type;
	public string m_name;
	private float speed;
	float yDelta;
	int bomb_time;
	// Use this for initialization
	void Start ()
	{
		float ran_x, ran_y;
		if (m_type == ObstacleType.Falling_Rock) {
			m_strength = 100;
			m_name = "FallingRock";
			//ran_x = Random.Range (-250.0f, 400.0f);
			ran_x=JBMainCharacterObj._instance.transform.localPosition.x;
			ran_y = 800f;
			speed = 4;
		} else if (m_type == ObstacleType.Static_Rock) {
			m_strength = 200;
			m_name = "Rock";
			ran_x = Random.Range (600.0f, 1600.0f);
			ran_y = -270f;
			speed = 6;
		} else if (m_type == ObstacleType.Rolling_Rock) {
			m_strength = 100;
			m_name = "RollingRock";
			ran_x = Random.Range (600.0f, 1600.0f);
			ran_y = -270f;
			speed = 7;
		} else if (m_type == ObstacleType.Plant) {
			m_strength = 50;
			m_name = "ePlant";
			ran_x = Random.Range (600f, 1600f);
			ran_y = -270f;
			speed = 5;
		} else if (m_type == ObstacleType.Gas) {
			m_strength = 80;
			m_name = "gas";
			ran_x = Random.Range (450.0f, 600.0f);
			ran_y = Random.Range (-160.0f, 160.0f);
			speed = 6;
		} else if (m_type == ObstacleType.Spining_Razor) {
			m_strength = 100;
			m_name = "spining_razor";
			ran_x = Random.Range (450.0f, 600.0f);
			ran_y = Random.Range (-200.0f, 200.0f);
			speed = 10;
		} else if (m_type == ObstacleType.SubBomb)
        {
            m_strength = 150;
            m_name = "subbomb";
            ran_x = 0;
            ran_y = 0;
        }
		else 
		{
			m_strength = 100;
			m_name = "spining_razor";
			ran_x = Random.Range (450.0f, 600.0f);
			ran_y = Random.Range (-200.0f, 200.0f);
			speed = 10;
		}
		transform.localPosition = new Vector3 (ran_x,ran_y,0);
		if (ran_y > 0) 
		{
			yDelta = -2;
		}
		else 
		{
			yDelta=2;
		}
		bomb_time = 0;
	}
	// Update is called once per frame
	void Update ()
	{
		if (JBKernel.GM_state ==JBKernel.GameState.playing) 
		{
			if(m_type==ObstacleType.Falling_Rock)
			{
				Vector2 position = transform.position; 
				position = new Vector2 (position.x, position.y-speed*2);
				transform.position = position;
				//bottom left point
				if (!IsinScreenArea()) 
				{
					Destroy (gameObject);
				}
			}
			else if(m_type==ObstacleType.Spining_Razor)
			{
				Vector2 position = transform.position; 
				position = new Vector2 (position.x - speed, position.y+yDelta*2);
				transform.position = position;
				if (!IsinScreenArea()) 
				{
					Destroy (gameObject);
				}
			}
			else if(m_type==ObstacleType.Static_Rock)
			{
				Vector2 position = transform.position; 
				position = new Vector2 (position.x - speed, position.y);
				transform.position = position;
				//bomb_time++;
				if(IsCloseBee())
				{
					//bomb_time=0;
					BombExplosion();
				}
				if (!IsinScreenArea()) 
				{
					Destroy (gameObject);
				}
			}
            else if (m_type == ObstacleType.SubBomb)
            {

            }
			else 
			{
				Vector2 position = transform.position; 
				position = new Vector2 (position.x - speed, position.y);
				transform.position = position;
				//bottom left point
				if (!IsinScreenArea()) 
				{
					Destroy (gameObject);
				}
			}
		} 
	}
	bool IsCloseBee(){
		float dis = Mathf.Abs (JBMainCharacterObj._instance.transform.localPosition.x-transform.localPosition.x);
		if(dis<200)
		{
			return true;
		}
		return false;
	}
	void BombExplosion()
	{
		GetComponent<Image> ().enabled = false;
		transform.GetChild (0).gameObject.SetActive (true);
		GetComponent<BoxCollider> ().size = new Vector3 (200,200,1);
		DestroyObject (gameObject,0.8f);
	}
	void OnTiggerEnter(Collider obj)
	{
		if(obj.gameObject.name=="JBMainChar")
		{
            if (m_type == ObstacleType.SubBomb)
                BombExplosion();

			BreakThis();
		}
	}

	void BreakThis()
	{
		Destroy (gameObject);
	}
	bool IsinScreenArea()
	{
		float width = 1136 / 2;
		float height = 640 / 2;
		if(transform.localPosition.x<=-width || transform.localPosition.y<=-height)
		{
			return false;
		}
		return true;
	}
}

