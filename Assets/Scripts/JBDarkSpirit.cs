using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JBClasses;
using dataread;

public class JBDarkSpirit : MonoBehaviour {

	public string m_BossName;
	public float m_BossHp;
	public BossExistType m_BossType;
	public BossAttackMode m_BossAttackType;
	public float m_BossSkyAttackPower;
	public float m_BossGroundAttackPower;
	public static float m_BossSkySpeed;
	public float m_BossGroundSpeed;
	public float m_BossAccerelationSpeed;
	public float m_BossSkyLaunchSpeed;
	public float m_BossGroundLaunchSpeed;
	public float m_BossHoneyEat;
	public int m_BossScore;
	public BossWeapon m_weapon;
	public int m_AttackDistance;
	public int m_presentLevel;

	public delegate void BeckyPresentHandler();
	public static event BeckyPresentHandler BeckyPresent;

	float m_AlphaTime;
	float m_Tick;

	// Use this for initialization
	void Start () 
	{
		//m_BossAccerelationSpeed = 5;
		m_BossName = "dark_spirit";
		m_BossAttackType = BossAttackMode.ONLY_ATTACK;
		m_BossGroundAttackPower = 1500;
		m_BossGroundLaunchSpeed = 5;
		m_BossGroundSpeed = 0;
		m_BossHoneyEat = 0;
		m_BossHp = 200;
		m_BossSkyAttackPower = 100;
		m_BossSkyLaunchSpeed = 5;
		m_BossSkySpeed = 10;
		m_BossType = BossExistType.LAND_SKY;
		m_AttackDistance = 2000;
		m_BossScore = 10000;
		m_AlphaTime = 0;
		m_Tick = 0;
	}
	// Update is called once per frame
	void Update () 
	{
		m_Tick += Time.deltaTime;
		m_AlphaTime += Time.deltaTime*10;
		if(m_Tick>=2)
		{
			m_Tick=0;
			Move();
			Attack();
			IMGAlphaControl(m_AlphaTime);
		}
		IMGAlphaControl(m_AlphaTime);
		if(m_AlphaTime>=1)
		{
			m_AlphaTime=0;
			IMGAlphaControl(m_AlphaTime);
		}

	}
	void OnTriggerEnter(Collider other)
	{
		//Collision of player ship and bullet
		if (other.gameObject.name=="bullet(Clone)")
		{
			m_BossHp-=20;
			//PlayExplosion();
			if(m_BossHp<=0)
			{
				BossKilled();
			}
		}
	}
	void BossKilled()
	{
		GetComponent<Image> ().color = Vector4.one;
		gameObject.AddComponent<TweenPosition> ();
		gameObject.GetComponent<TweenPosition> ().from = transform.localPosition;
		gameObject.GetComponent<TweenPosition> ().to = transform.localPosition + new Vector3 (0,-700,0);
		gameObject.GetComponent<TweenPosition> ().duration = 1.0f;
		gameObject.GetComponent<TweenPosition> ().PlayForward ();
		DestroyObject (gameObject, 3.1f);
		if (JBDB.levelint == 50) 
		{
			//JBKernel.GM_state = JBKernel.GameState.end_total;
			StartCoroutine("SettingState",true);
		} 
		else 
		{
			//JBKernel.GM_state = JBKernel.GameState.end_complete;
			StartCoroutine("SettingState",false);
		}
		StartCoroutine("BeckyActing");
	}
	IEnumerator BeckyActing()
	{
		yield return new WaitForSeconds (1.0f);
		//JBKernel.GM_state = JBKernel.GameState.end_complete;
		if(BeckyPresent!=null)
		{
			BeckyPresent ();
		}

	}
	IEnumerator SettingState(bool GameFinish)
	{
		yield return new WaitForSeconds (3.0f);
		if(GameFinish)
		{
			JBKernel.GM_state=JBKernel.GameState.end_total;
		}
		else
		{
			JBKernel.GM_state = JBKernel.GameState.end_complete; 	
		}
	}
	void Move()
	{
		//Vector3 char_pos = JBMainCharacterObj._instance.transform.localPosition;
		transform.position = JBMainCharacterObj._instance.transform.position + new Vector3 (Mathf.Lerp(2,4,0.5f)*200,Mathf.PingPong(0.5f,300),0);

	}
	void IMGAlphaControl(float _alpha)
	{
		if (_alpha <= 0) 
		{
			GetComponent<Image> ().color = new Vector4 (1, 1, 1, _alpha);
			GetComponent<BoxCollider> ().enabled = false;
		}
		else 
		{
			GetComponent<Image> ().color = new Vector4 (1, 1, 1, _alpha);
			GetComponent<BoxCollider>().enabled=true;
		}
	}
	void Attack()
	{
		GameObject.Find("JB_Kernel").GetComponent<JBKernel>().PresentWaspTroop ();
	}
}
