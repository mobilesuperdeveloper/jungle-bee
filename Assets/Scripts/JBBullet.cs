using UnityEngine;
using System.Collections;
using dataread;
using JBClasses;

public class JBBullet : MonoBehaviour {

	float m_bulletspeed;
	public GameObject particle;
	public GameObject Boss_particle;
	public static Vector2 pos;
	public int m_bulletPower;
	private string m_charName;
    public int direction;//0-directly 1-up 2-down
    bool m_bAttackDirInvert = false;
	// Use this for initialization
	void Start () 
	{
		m_bulletspeed =800f;
        m_bAttackDirInvert = JBKernel.m_bAttackDirInvert;

		m_charName = JBDB.characterName;
		if (m_charName == "JungleBee") 
		{
			m_bulletspeed=800f;
			m_bulletPower=15;
		} 
		else if (m_charName == "CarpenterBee") 
		{
			m_bulletspeed=900f;
			m_bulletPower=30;
		}
		else if (m_charName == "BumbleBee") 
		{
			m_bulletspeed=1000f;
			m_bulletPower=40;
		} 
		else if (m_charName == "KillerBee")
		{
			m_bulletspeed=1100f;
			m_bulletPower=50;
		} 
		else 
		{
			m_bulletspeed=800f;
			m_bulletPower=15;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
        Vector2 position = transform.position;
        if (m_bAttackDirInvert)
        {
            if (direction == 0)
            {
                position = new Vector2(position.x - m_bulletspeed * Time.deltaTime, position.y);
                transform.position = position;
            }
            else if (direction == 1)
            {
                position = new Vector2(position.x - m_bulletspeed * Time.deltaTime, position.y - m_bulletspeed * Time.deltaTime * 0.3f);
                transform.position = position;
            }
            else if (direction == 2)
            {
                position = new Vector2(position.x - m_bulletspeed * Time.deltaTime, position.y + m_bulletspeed * Time.deltaTime * 0.3f);
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
                position = new Vector2(position.x + m_bulletspeed * Time.deltaTime, position.y);
                transform.position = position;
            }
            else if (direction == 1)
            {
                position = new Vector2(position.x + m_bulletspeed * Time.deltaTime, position.y - m_bulletspeed * Time.deltaTime * 0.3f);
                transform.position = position;
            }
            else if (direction == 2)
            {
                position = new Vector2(position.x + m_bulletspeed * Time.deltaTime, position.y + m_bulletspeed * Time.deltaTime * 0.3f);
                transform.position = position;
            }

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

		if(!IsinArea())
		{
			Destroy(gameObject);
		}
	}
	bool IsinArea()
	{
		float width = 1200 / 2;
		//float height = 640 / 2;
		if(transform.localPosition.x>=width || transform.localPosition.x <= -width)
		{
			return false;
		}
		return true;
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.name!="bullet(Clone)"&&other.name!="bar"&&other.transform.parent.name!="Item"&&other.transform.parent.name!="Bonus"&&other.gameObject.name != "JBMainChar"&&other.transform.parent.name!="JBUIObj"&&other.name!="bossbullet(Clone)") 
		{
            //JBKernel.ResetBulletTime();

			Destroy(gameObject);
			if(other.gameObject.name=="Boss")
			{
				Boss_PlayExplosion();
			}
			else  PlayExplosion();
		}
	}
	void PlayExplosion()
	{
		GameObject explosion = (GameObject)Instantiate (particle);
		explosion.transform.localScale=new Vector2(1,1);
		pos = transform.position;
		explosion.transform.position = transform.localPosition;
	}
	void Boss_PlayExplosion()
	{
		GameObject explosion = (GameObject)Instantiate (Boss_particle);
		explosion.transform.localScale=new Vector2(1,1);
		pos = transform.position;
		explosion.transform.position = transform.localPosition;
	}
}
