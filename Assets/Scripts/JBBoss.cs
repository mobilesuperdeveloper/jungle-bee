using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JBClasses;
public class JBBoss : MonoBehaviour {
		//public GameObject particle;
		public static Vector2 pos;
		public GameObject m_bullet;
		private GameObject EnemyHouse;
		public static JBBoss _instance;
	    public GameObject particle;
		Sprite _killedIMG;
        bool m_Died = false;
		public string m_BossName 
		{
			get;
			set;
		}
		public float m_BossHp 
		{
			get;
			set;
		}
		public BossExistType m_BossType
		{
			get;
			set;
		}
		public BossAttackMode m_BossAttackType 
		{
			get;
			set;
		}
		public float m_BossSkyAttackPower 
		{
			get;
			set;
		}
		public float m_BossGroundAttackPower 
		{
			get;
			set;
		}
		public static float m_BossSkySpeed 
		{
			get;
			set;
		}
		public float m_BossGroundSpeed {
			get;
			set;
		}
		public float m_BossAccerelationSpeed {
			get;
			set;
		}
		public float m_BossSkyLaunchSpeed {
			get;
			set;
		}
		public float m_BossGroundLaunchSpeed {
			get;
			set;
		}
		public float m_BossHoneyEat 
		{
			get;
			set;
		}
		public int m_BossScore
		{
		get;
		set;
		}
	public BossWeapon m_weapon 
	{
		get;
		set;
	}
	public int m_AttackDistance {
		get;
		set;
	}
	// Use this for initialization
	void Start () 
	{

		_instance=this;
		if (m_BossName == "wasp_king") {
			m_BossAccerelationSpeed = 5;
			m_BossAttackType = BossAttackMode.ONLY_ATTACK;
			m_BossGroundAttackPower = 0;
			m_BossGroundLaunchSpeed = 0;
			m_BossGroundSpeed = 0;
			m_BossHoneyEat = 0;
			m_BossHp = 60;
			m_BossSkyAttackPower = 100;
			m_BossSkyLaunchSpeed = 10;
			m_BossSkySpeed = 10;
			m_BossType = BossExistType.SKY;
			m_BossScore = 500;
			m_weapon = BossWeapon.stinger;
			m_AttackDistance = 300;
		}
		else if (m_BossName == "yellow_hornet") 
		{
			m_BossAccerelationSpeed = 5;
			m_BossAttackType = BossAttackMode.ONLY_ATTACK;
			m_BossGroundAttackPower = 0;
			m_BossGroundLaunchSpeed = 0;
			m_BossGroundSpeed = 0;
			m_BossHoneyEat = 0;
			m_BossHp = 300;
			m_BossSkyAttackPower = 100;
			m_BossSkyLaunchSpeed = 10;
			m_BossSkySpeed = 15;
			m_BossType = BossExistType.SKY;
			m_BossScore = 600;
			m_weapon = BossWeapon.stinger;
			m_AttackDistance = 800;
		} else if (m_BossName == "mantis_king") {
			m_BossAccerelationSpeed = 5;
			m_BossAttackType = BossAttackMode.ONLY_ATTACK;
			m_BossGroundAttackPower = 150;
			m_BossGroundLaunchSpeed = 5;
			m_BossGroundSpeed = 15;
			m_BossHoneyEat = 0;
			m_BossHp = 100;
			m_BossSkyAttackPower = 0;
			m_BossSkyLaunchSpeed = 10;
			m_BossSkySpeed = 0;
			m_BossType = BossExistType.LAND;
			m_BossScore = 500;
			m_weapon = BossWeapon.sword;
			m_AttackDistance = 300;
		} else if (m_BossName == "spider_king") {

			m_BossAccerelationSpeed = 5;
			m_BossAttackType = BossAttackMode.ONLY_ATTACK;
			m_BossGroundAttackPower = 0;
			m_BossGroundLaunchSpeed = 0;
			m_BossGroundSpeed = 5;
			m_BossHoneyEat = 0;
			m_BossHp = 50;
			m_BossSkyAttackPower = 150;
			m_BossSkyLaunchSpeed = 10;
			m_BossSkySpeed = 10;
			m_BossType = BossExistType.SKY;
			m_AttackDistance = 400;
			m_BossScore = 200;
		} 
		else if (m_BossName == "scorpion") 
		{
			m_BossAccerelationSpeed = 5;
			m_BossAttackType = BossAttackMode.ONLY_ATTACK;
			m_BossGroundAttackPower = 300;
			m_BossGroundLaunchSpeed = 0;
			m_BossGroundSpeed = 5;
			m_BossHoneyEat = 0;
			m_BossHp = 200;
			m_BossSkyAttackPower = 100;
			m_BossSkyLaunchSpeed = 10;
			m_BossSkySpeed = 10;
			m_BossType = BossExistType.LAND;
			m_AttackDistance = 500;
			m_BossScore = 300;
		} 
		else
		{
			m_BossAccerelationSpeed = 5;
			m_BossAttackType = BossAttackMode.ONLY_ATTACK;
			m_BossGroundAttackPower = 0;
			m_BossGroundLaunchSpeed = 0;
			m_BossGroundSpeed = 0;
			m_BossHoneyEat = 0;
			m_BossHp = 60;
			m_BossSkyAttackPower = 100;
			m_BossSkyLaunchSpeed = 10;
			m_BossSkySpeed = 10;
			m_BossType = BossExistType.SKY;
			m_BossScore = 500;
			m_weapon = BossWeapon.stinger;
			m_AttackDistance = 300;
		}
		float ran_x, ran_y;
		EnemyHouse = GameObject.Find ("Enemy");
		if(m_BossType==BossExistType.LAND)
		{
			ran_x=Random.Range(450.0f,600.0f);
			ran_y=-300.0f;
		}
		else if(m_BossType==BossExistType.SKY)
		{
			ran_x=Random.Range (450.0f, 600.0f);
			ran_y=Random.Range (-160.0f, 160.0f);
		}
		else if(m_BossType==BossExistType.LAND_SKY)
		{
			ran_x=Random.Range (450.0f, 600.0f);
			ran_y=Random.Range (-160.0f, 160.0f);
		}
		else 
		{
			ran_x=Random.Range (450.0f, 600.0f);
			ran_y=Random.Range (-160.0f, 160.0f);
		}
		
		transform.SetParent(EnemyHouse.transform);
		transform.localScale=Vector3.one;

        m_Died = false;
		//transform.localPosition=new Vector3(ran_x,ran_y);
		//SetBossCharAnimIMG ();
	}
	// Update is called once per frame
	void Update () 
	{
		if (JBKernel.GM_state == JBKernel.GameState.playing) 
		{
			if(!GameObject.Find("bossbullet(Clone)"))
			{
//			    num += 0.025f;
//				if (num >= 10f) 
//				{
//					GameObject boss_bullet=Instantiate(m_bullet) as GameObject;
//					boss_bullet.transform.SetParent (GameObject.Find ("JBBullet").transform);
//					boss_bullet.transform.position = transform.position;
//					boss_bullet.transform.localScale = new Vector2 (1, 1);
//					num = 0f; 
//				}
			}
		}
	}
	void OnTriggerEnter(Collider other)
	{
		//Collision of player ship and bullet
		if (other.gameObject.name=="bullet(Clone)")
		{
			//m_BossHp-=20;
			int charbullet=other.GetComponent<JBBullet>().m_bulletPower;
			m_BossHp-=charbullet;
			PlayExplosion();
			if(m_BossHp<=0 && !m_Died)
			{
                m_Died = true;
				BossKilled();
			}
		}
		if (other.gameObject.name == "JBMainChar") {
			gameObject.GetComponent<JBBossAI>().rushFlag=2;
		}
	}
	void PlayExplosion()
	{
		pos = transform.position;
		GameObject explosion = (GameObject)Instantiate (particle);
		explosion.transform.SetParent (transform);
		explosion.transform.localScale=new Vector2(1,1);
		explosion.transform.localPosition = Vector2.zero;
		//explosion.transform.position = transform.localPosition;
	}
	void BossKilled()
	{
        JBKernel.m_JBBoss = null;

		if (_killedIMG != null) {
			GetComponent<Image> ().sprite = _killedIMG;
		}
		//GetComponent<JBSpriteAnim> ().Loop = true;
		//GetComponent<JBSpriteAnim> ().frames = 2;
		//GetComponent<JBSpriteAnim> ()._prefix = m_BossName + "Killed";
		//GetComponent<JBSpriteAnim> ()._folderName = "Atlas/"+m_BossName;

		gameObject.AddComponent<TweenPosition> ();
		gameObject.GetComponent<TweenPosition> ().from = transform.localPosition;
		gameObject.GetComponent<TweenPosition> ().to = transform.localPosition + new Vector3 (0,-700,0);
		gameObject.GetComponent<TweenPosition> ().duration = 1.0f;
		gameObject.GetComponent<TweenPosition> ().PlayForward ();

		DestroyObject (gameObject, 1.1f);
		//JBKernel.GM_state = JBKernel.GameState.end_complete;
		StartCoroutine("setting_state");
	}
	IEnumerator setting_state()
	{
		yield return new WaitForSeconds (1.0f);
		//JBKernel.GM_state = JBKernel.GameState.end_complete;
		GameObject.Find("JB_Kernel").SendMessage ("DisguiseAnimalAct");
	}
	void SetBossCharAnimIMG()
	{
//		GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Atlas/"+m_BossName+"/"+m_BossName+"Flying");
//		GetComponent<JBSpriteAnim> ().Loop = true;
//		GetComponent<JBSpriteAnim> ().frames = 2;
//		GetComponent<JBSpriteAnim> ()._prefix = m_BossName + "Flying";
//		GetComponent<JBSpriteAnim> ()._folderName = "Atlas/"+m_BossName;
		if(m_BossName=="wasp_king")
		{
			_killedIMG=Resources.Load<Sprite>("Atlas/WaspKing/WaspKingKilled");
		}
		else if(m_BossName=="yellow_hornet")
		{
			_killedIMG=Resources.Load<Sprite>("Atlas/YellowHornet/YellowHornetKilled");
		}
		else
		{
			_killedIMG=null;
		}
	}
}
