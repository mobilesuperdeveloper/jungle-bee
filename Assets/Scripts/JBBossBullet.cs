using UnityEngine;
using System.Collections;
using dataread;
public class JBBossBullet: MonoBehaviour 
{
	float bulletspeed;
	public int direction;//0-directly 1-up 2-down
    bool m_bAttackDirInvert = false;

	// Use this for initialization
	void Start () 
	{
		bulletspeed = 300;
        m_bAttackDirInvert = JBKernel.m_bAttackDirInvert;
	}
	// Update is called once per frame
	void Update () 
	{
        Vector2 position = transform.position;

        if (m_bAttackDirInvert)
        {
            if (direction == 0)
            {
                position = new Vector2(position.x + bulletspeed * Time.deltaTime, position.y);
                transform.position = position;
            }
            else if (direction == 1)
            {
                position = new Vector2(position.x + bulletspeed * Time.deltaTime, position.y - bulletspeed * Time.deltaTime);
                transform.position = position;
            }
            else if (direction == 2)
            {
                position = new Vector2(position.x + bulletspeed * Time.deltaTime, position.y + bulletspeed * Time.deltaTime);
                transform.position = position;
            }

            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else
        {
            if (direction == 0)
            {
                position = new Vector2(position.x - bulletspeed * Time.deltaTime, position.y);
                transform.position = position;
            }
            else if (direction == 1)
            {
                position = new Vector2(position.x - bulletspeed * Time.deltaTime, position.y - bulletspeed * Time.deltaTime);
                transform.position = position;
            }
            else if (direction == 2)
            {
                position = new Vector2(position.x - bulletspeed * Time.deltaTime, position.y + bulletspeed * Time.deltaTime);
                transform.position = position;
            }

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }


        if (!IsinArea())
        {
            Destroy(gameObject);
        }
	}

    bool IsinArea()
    {
        float width = 1200 / 2;
        //float height = 640 / 2;
        if (transform.localPosition.x >= width || transform.localPosition.x <= -width)
        {
            return false;
        }
        return true;
    }

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "JBMainChar") {
			Destroy (gameObject);
		}
	}
}
