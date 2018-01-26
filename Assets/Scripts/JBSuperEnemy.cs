using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
using JBClasses;

public class JBSuperEnemy : MonoBehaviour
{
	public string m_name;
	public float m_existTime;
	public int m_score;
	public int m_attackdistance;
	public int m_attackamount;
	public int m_attackpower;
	public float m_pseudoTime;
	public float m_readyTime;
	public BossAttackMode m_attackType;
	public Text _message;
	public bool _present;
	public float _curTime;
	// Use this for initialization
	void Start ()
	{
		_curTime = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_curTime += Time.deltaTime;
		if (_curTime < m_readyTime) 
		{
			DisplayText1 ();
		}
		else if(_curTime>m_readyTime && _curTime<m_readyTime+m_existTime)
		{
			DisplayText2();
		}
		else if(_curTime>m_readyTime+m_existTime)
		{
			ReduceBee();
			_curTime=0;
			StartCoroutine("ActiveFalse");
		}
	}
	IEnumerator ActiveFalse()
	{
		yield return new WaitForSeconds (0.1f);
		gameObject.SetActive (false);
	}
	void Killed()
	{
		DestroyObject (gameObject);
	}
	void DisplayText1()
	{
		string prewarning=m_name+" is going to attack you in "+m_pseudoTime+" time!";

		_message.text = prewarning;
	}
	void DisplayText2()
	{
		string warning = "You are being attacked "+m_name+" now!";
		_message.text = warning;
	}
	void ReduceBee()
	{
		if(m_name=="bee_eater")
		{
			JBDB.items.FlowerCount-=1;
			if(JBDB.items.FlowerCount<0)
			{
				JBDB.items.FlowerCount=0;
			}
		}
		else if(m_name=="honey_badger")
		{
			JBDB.items.HoneyCount-=2;
			if(JBDB.items.HoneyCount<0)
			{
				JBDB.items.HoneyCount=0;
			}
		}
		else if(m_name=="bear")
		{
			JBDB.items.FlowerCount-=2;
			JBDB.items.HoneyCount-=2;
			if(JBDB.items.FlowerCount<0)
			{
				JBDB.items.FlowerCount=0;
			}
			if(JBDB.items.HoneyCount<0)
			{
				JBDB.items.FlowerCount=0;
			}
		}
	}
	public void SetEnemyData()
	{
		if(m_name=="bee_eater")
		{
			m_existTime=5;
			m_score=800;
			m_attackdistance=0;
			m_attackType=BossAttackMode.ONLY_ATTACK;
			m_attackpower=1000;
			m_attackamount=0;
			m_pseudoTime=0;
			m_readyTime=3;
		
		}
		else if(m_name=="bear")
		{
			m_existTime=5;
			m_score=2000;
			m_attackdistance=0;
			m_attackType=BossAttackMode.ONLY_HONEY;
			m_attackamount=5;
			m_pseudoTime=0;
			m_readyTime=3;
			m_attackpower=0;
		}
		else if(m_name=="honey_badger")
		{
			m_existTime=5;
			m_score=1000;
			m_attackdistance=0;
			m_attackType=BossAttackMode.ONLY_HONEY;
			m_attackamount=3;
			m_pseudoTime=0;
			m_readyTime=3;
			m_attackpower=0;
		}
		else if(m_name=="poison_gas")
		{
			m_existTime=5;
			m_score=1500;
			m_attackdistance=0;
			m_attackType=BossAttackMode.ONLY_ATTACK;
			m_attackpower=1000;
			m_attackamount=0;
			m_pseudoTime=0;
			m_readyTime=3;
		}
		else if(m_name=="moth")
		{
			m_existTime=10;
			m_score=2000;
			m_attackdistance=30;
			m_attackType=BossAttackMode.ONLY_ATTACK;
			m_attackpower=1000;
			m_attackamount=0;
			m_pseudoTime=0;
			m_readyTime=3;
		}
	}
}

