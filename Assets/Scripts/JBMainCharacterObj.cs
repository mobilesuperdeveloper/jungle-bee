using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using dataread;
using JBClasses;
public class JBMainCharacterObj : MonoBehaviour{

	public GameObject bee;
	public GameObject gameover;
	public string m_CharName ;
	public static float m_CharPower;
	public float m_CharSkyAttackPower;
	public float m_CharGroundAttackPower;
	public static float m_CharSkySpeed; 
	public float m_CharGroundSpeed;
	public float m_CharAccerelationSpeed;
	public float m_CharSkyLaunchSpeed;
	public float m_CharGroundLaunchSpeed;
	public float m_CharSkyHoneyConsumption;
	public float m_CharGroundHoneyConsumption;
	public float m_CharAccelerationHoneyConsumption;
	public float m_CharHomeHoneyAmount;
	public float m_CharSelfHoneyAmount;
	//public SuperAction m_super;
	public static JBMainCharacterObj _instance;
	public  GameObject _shield;
	public GameObject beeHp;
	public static float m_CharHP;
    public float m_HP;
	public float m_Power;
	public static bool IsInShield;
	public float m_ShieldTime;
	bool ItemGet;
	public bool BossPresent;
	float m_Tick;

	void Awake()
	{
		m_CharName = JBDB.characterName;
		if (m_CharName == "JungleBee") {
			m_CharHP = 500;
			m_HP=500;
			m_CharPower=10;
			m_Power=10;
			m_CharSkyAttackPower = 20;
			m_CharGroundAttackPower = 0;
			m_CharSkySpeed = 10;
			m_CharGroundSpeed = 5;
			m_CharAccerelationSpeed = 20;
			m_CharSkyLaunchSpeed = 20;
			m_CharGroundSpeed = 0;
			m_CharSkyHoneyConsumption = 1;
			m_CharGroundHoneyConsumption = 1;
			m_CharAccelerationHoneyConsumption = 2;
			m_CharHomeHoneyAmount = 100;
			m_CharSelfHoneyAmount = 10;
		} else if (m_CharName == "CarpenterBee") 
		{
			m_CharHP = 600;
			m_HP=600;
			m_CharPower=15;
			m_Power=15;
			m_CharSkyAttackPower = 20;
			m_CharGroundAttackPower = 10;
			m_CharSkySpeed = 10;
			m_CharGroundSpeed = 8;
			m_CharAccerelationSpeed = 20;
			m_CharSkyLaunchSpeed = 20;
			m_CharGroundSpeed = 15;
			m_CharSkyHoneyConsumption = 1;
			m_CharGroundHoneyConsumption = 1;
			m_CharAccelerationHoneyConsumption = 2;
			m_CharHomeHoneyAmount = 100;
			m_CharSelfHoneyAmount = 80;
		} else if (m_CharName == "BumbleBee") {
			m_CharHP = 800;
			m_HP=800;
			m_CharPower=20;
			m_Power=20;
			m_CharSkyAttackPower = 20;
			m_CharGroundAttackPower = 0;
			m_CharSkySpeed = 10;
			m_CharGroundSpeed = 5;
			m_CharAccerelationSpeed = 20;
			m_CharSkyLaunchSpeed = 20;
			m_CharGroundSpeed = 0;
			m_CharSkyHoneyConsumption = 2;
			m_CharGroundHoneyConsumption = 1;
			m_CharAccelerationHoneyConsumption = 3;
			m_CharHomeHoneyAmount = 100;
			m_CharSelfHoneyAmount = 10;
		} else if (m_CharName == "KillerBee") {
			m_CharHP = 1000;
			m_HP=1000;
			m_CharPower=50;
			m_Power=50;
			m_CharSkyAttackPower = 20;
			m_CharGroundAttackPower = 0;
			m_CharSkySpeed = 10;
			m_CharGroundSpeed = 5;
			m_CharAccerelationSpeed = 20;
			m_CharSkyLaunchSpeed = 20;
			m_CharGroundSpeed = 0;
			m_CharSkyHoneyConsumption = 1;
			m_CharGroundHoneyConsumption = 1;
			m_CharAccelerationHoneyConsumption = 2;
			m_CharHomeHoneyAmount = 100;
			m_CharSelfHoneyAmount = 10;
		} 
		else 
		{
			m_CharHP=500;
			m_HP=500;
			m_CharPower=10;
			m_Power=10;
			m_CharSkyAttackPower=20;
			m_CharGroundAttackPower=0;
			m_CharSkySpeed=10;
			m_CharGroundSpeed=5;
			m_CharAccerelationSpeed=20;
			m_CharSkyLaunchSpeed=20;
			m_CharGroundSpeed=0;
			m_CharSkyHoneyConsumption=1;
			m_CharGroundHoneyConsumption=1;
			m_CharAccelerationHoneyConsumption=2;
			m_CharHomeHoneyAmount=100;
			m_CharSelfHoneyAmount=10;
		}

	}
	// Use this for initialization
	void Start () 
	{
		//m_CharHP = 500;
		SetCharAnimIMG();
		//JBHP.cur_hp=m_CharHP/m_HP;
		//JBPower.energy = m_CharPower/m_Power;
		m_Tick = 0;
		IsInShield = false;
		m_ShieldTime = 0;
		//bee.rigidbody.isKinematic=true;
		_instance=this;
		BossPresent = false;
	}
	// Update is called once per frame
	void Update () 
	{
        if (JBKernel.GM_state != JBKernel.GameState.playing)
            return;

			collider ();
			if(JBKernel.MainCharacter_State!=JBKernel.JB_MainCharacterState.Killed)
			{
				m_Tick+=Time.deltaTime;
				transform.Find ("JBMainCharFlying").GetComponent<Image>().enabled=true;
				if(IsInShield)
				{
					_shield.SetActive(true);
					m_ShieldTime+=Time.deltaTime;
					if(m_ShieldTime>=7f)
					{
						m_ShieldTime=0;
						IsInShield=false;
						_shield.SetActive(false);
					}
				}
				if(m_Tick>=1)
				{
					m_Tick=0;
					ReduceSelfPower();
				}
				if(m_CharPower<=1)
				{
					bee.GetComponent<Rigidbody>().isKinematic=false;
				}
				else
				{
					bee.GetComponent<Rigidbody>().isKinematic=true;
				}

                JBHP.cur_hp = m_CharHP / m_HP;
                JBPower.energy = m_CharPower / m_Power;
			}
	}
	void collider()
    {
        if (bee.transform.localPosition.x >= 500.0f)
            bee.transform.localPosition = new Vector2(500.0f, bee.transform.localPosition.y);
        if (bee.transform.localPosition.x <= -500.0f)
            bee.transform.localPosition = new Vector2(-500.0f, bee.transform.localPosition.y);
        if (bee.transform.localPosition.y >= 270.0f)
            bee.transform.localPosition = new Vector2(bee.transform.localPosition.x, 270.0f);
        if (bee.transform.localPosition.y <= -270.0f)
            bee.transform.localPosition = new Vector2(bee.transform.localPosition.x, -270.0f);

        if (BossPresent && JBKernel.m_JBBoss != null)
        {
            if (JBKernel.m_JBBoss.transform.localPosition.x < bee.transform.localPosition.x)
            {
                Vector3 scale = bee.transform.localScale;
                scale.x = -Mathf.Abs(scale.x);
                bee.transform.localScale = scale;

                Vector3 scale1 = JBKernel.m_JBBoss.transform.localScale;
                scale1.x = -Mathf.Abs(scale1.x);
                JBKernel.m_JBBoss.transform.localScale = scale1;

                JBKernel.m_bAttackDirInvert = true;
            }
            else
            {
                Vector3 scale = bee.transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                bee.transform.localScale = scale;

                Vector3 scale1 = JBKernel.m_JBBoss.transform.localScale;
                scale1.x = Mathf.Abs(scale1.x);
                JBKernel.m_JBBoss.transform.localScale = scale1;

                JBKernel.m_bAttackDirInvert = false;
            }
        }
        /*
		if (!BossPresent) {
			if (bee.transform.localPosition.x >= 500.0f)
				bee.transform.localPosition = new Vector2 (500.0f, bee.transform.localPosition.y);
			if (bee.transform.localPosition.x <= -500.0f)
				bee.transform.localPosition = new Vector2 (-500.0f, bee.transform.localPosition.y);
			if (bee.transform.localPosition.y >= 270.0f)
				bee.transform.localPosition = new Vector2 (bee.transform.localPosition.x, 270.0f);
			if (bee.transform.localPosition.y <= -270.0f)
				bee.transform.localPosition = new Vector2 (bee.transform.localPosition.x, -270.0f);

            Vector3 scale = bee.transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            bee.transform.localScale = scale;
		}
		else 
		{
			if (bee.transform.localPosition.x >= 100.0f)
				bee.transform.localPosition = new Vector2 (100.0f, bee.transform.localPosition.y);
			if (bee.transform.localPosition.x <= -500.0f)
				bee.transform.localPosition = new Vector2 (-500.0f, bee.transform.localPosition.y);
			if (bee.transform.localPosition.y >= 270.0f)
				bee.transform.localPosition = new Vector2 (bee.transform.localPosition.x, 270.0f);
			if (bee.transform.localPosition.y <= -270.0f)
				bee.transform.localPosition = new Vector2 (bee.transform.localPosition.x, -270.0f);
		}
         * */
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.parent.name!="Item"&&other.transform.parent.name != "JB_UIObj"&&other.gameObject.name != "bullet(Clone)") 
		{
			if(other.name=="heart")
			{
				ItemGet=true;
				JBDB.items.HeartCount++;
				Destroy(other.gameObject);
			}
			if(other.name=="flower")
			{
				ItemGet=true;
				JBDB.items.FlowerCount++;
				Destroy(other.gameObject);
			}
			if(other.name=="honey")
			{
				ItemGet=true;
				JBDB.items.HoneyCount++;
				Destroy(other.gameObject);	
			}
			if(other.name=="kiss")
			{
				ItemGet=true;
				JBDB.items.KissCount++;
				Destroy(other.gameObject);
			}
			if (other.gameObject.name == "bossbullet(Clone)"||other.transform.parent.name=="Enemy") 
			{
				if(IsInShield==true)
				{
					ItemGet=true;
					//Destroy (other.gameObject);
				}
				else
				{
					ItemGet=false;
					//Destroy (other.gameObject);
				}
			}
			if(ItemGet==false) 
			{
				m_CharHP-=20;

				if (m_CharHP <= 0) 
				{
					JBHP.cur_hp=0;
					JBKernel.GM_state = JBKernel.GameState.end_failed;
					KilledAnim ();
				} 
				else
				{
					//JBHP.cur_hp=m_CharHP/m_HP;
					AttackedAnim ();
				}
			}
		}
		if(other.name=="obstacle")
		{
			int damage=other.GetComponent<JBObstacle>().m_strength;
			m_CharHP-=damage;
			AttackedAnim ();
			//JBHP.cur_hp=m_CharHP/m_HP;
		}
		if(other.name=="rope")
		{
			AttackedAnim();
			m_CharHP-=5;
			//JBHP.cur_hp=m_CharHP/m_HP;
		}

        // TEST
        //m_CharHP = 10000;
	}
	public void ShootingAnim()
	{
		transform.Find ("JBMainCharFlying").gameObject.SetActive (false);
		transform.Find ("JBMainCharKilled").gameObject.SetActive (false);
		transform.Find ("JBMainCharAttacked").gameObject.SetActive (false);
		GameObject other_state;
		other_state = transform.Find ("JBMainCharShooting").gameObject;
		other_state.SetActive (true);
		StartCoroutine ("ReturnFlyingState");
	}
	IEnumerator ReturnFlyingState()
	{
		yield return new WaitForSeconds (0.3f);
		transform.Find ("JBMainCharFlying").gameObject.SetActive (true);
		transform.Find ("JBMainCharKilled").gameObject.SetActive (false);
		transform.Find ("JBMainCharAttacked").gameObject.SetActive (false);
		transform.Find ("JBMainCharShooting").gameObject.SetActive (false);
	}
	public void AttackedAnim()
	{
		transform.Find ("JBMainCharFlying").gameObject.SetActive (false);
		transform.Find ("JBMainCharKilled").gameObject.SetActive (false);
		transform.Find ("JBMainCharShooting").gameObject.SetActive (false);

		GameObject other_state;
		other_state = transform.Find ("JBMainCharAttacked").gameObject;
		other_state.SetActive (true);
		StartCoroutine ("ReturnFlyingState");
	}
	public void KilledAnim()
	{
			GameObject other_state;
			transform.Find ("JBMainCharFlying").gameObject.SetActive(false);
			transform.Find ("JBMainCharShooting").gameObject.SetActive (false);
			transform.Find ("JBMainCharAttacked").gameObject.SetActive (false);
			other_state = transform.Find ("JBMainCharKilled").gameObject;

			other_state.SetActive(true);
			other_state.AddComponent<TweenPosition> ();
			other_state.GetComponent<TweenPosition> ().from = other_state.transform.localPosition;
			other_state.GetComponent<TweenPosition> ().to = other_state.transform.localPosition + new Vector3 (0, -600, 0);
			other_state.GetComponent<TweenPosition> ().duration = 0.5f;
			other_state.GetComponent<TweenPosition> ().PlayForward ();
			JBUIsystem._intance.OpenGameOverDlg ();
		    JBKernel.MainCharacter_State = JBKernel.JB_MainCharacterState.Killed;
	}
	void ReduceSelfPower()
	{
		if(transform.localPosition.y>-270)
		{
			m_CharPower=m_CharPower-0.2f*m_CharSkyHoneyConsumption;
			//JBPower.energy=m_CharPower/m_Power;
		}
		else
		{
			if(m_CharPower<m_Power)
			{
				m_CharPower=m_CharPower+m_CharGroundHoneyConsumption;
			}
			//JBPower.energy=m_CharPower/m_Power;
		}
	}
	void SetCharAnimIMG()
	{
		Transform CharFlying = transform.Find ("JBMainCharFlying");
		Transform CharAttacked = transform.Find ("JBMainCharAttacked");
		Transform CharKilled = transform.Find ("JBMainCharKilled");
		Transform CharShooting=transform.Find("JBMainCharShooting");

		CharFlying.GetComponent<Image> ().sprite =JBDB.ImportedCharIMG[2];//2,3
		CharFlying.GetComponent<JBSpriteAnim> ().Loop = true;
		CharFlying.GetComponent<JBSpriteAnim> ()._prefix = JBDB.characterName + "Flying";
		CharFlying.GetComponent<JBSpriteAnim> ()._folderName = "Atlas/" + JBDB.characterName;
		CharFlying.GetComponent<JBSpriteAnim> ().frames = 2;
		CharFlying.GetComponent<Image> ().gameObject.SetActive(true);

		CharAttacked.GetComponent<Image> ().sprite = JBDB.ImportedCharIMG [2];
		CharAttacked.gameObject.SetActive(false);

		CharKilled.GetComponent<Image>().sprite=JBDB.ImportedCharIMG[6];
		CharKilled.gameObject.SetActive(false);

//		CharShooting.GetComponent<Image>().sprite=JBDB.ImportedCharIMG[9];
//		CharShooting.GetComponent<JBSpriteAnim> ().Loop = false;
//		CharShooting.GetComponent<JBSpriteAnim> ()._prefix = JBDB.characterName + "Shooting";
//		CharShooting.GetComponent<JBSpriteAnim> ()._folderName = "Atlas/" + JBDB.characterName;
//		CharShooting.GetComponent<JBSpriteAnim> ().frames = 4;
//		CharShooting.gameObject.SetActive(false);

	}
}
