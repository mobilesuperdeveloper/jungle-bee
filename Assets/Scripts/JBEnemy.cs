using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
using JBClasses;

public class JBEnemy : MonoBehaviour {

	float EnemySpeed; 
	public GameObject particle;
	public GameObject EnemyHouse;
    public GameObject SubBomb;
	public GameObject m_bullet;
	public  Vector2 pos;
	public static JBEnemy _instance;
	public int cur_level;
	public delegate void EliminateSelf();
	public event EliminateSelf _killed;
    
	int shootCount=50;
	const int shootFrame = 50; //shooting once per 80 frames
	GameObject m_bee;
	bool rushFlag=false;
	bool flyState=true; //true:up false:down
	public string m_name 
	{
		get;
		set;
	}
	public EnemyWeapon m_weapon 
	{
		get;
		set;
	}
	public MovingMode m_movingmode 
	{
		get;
		set;
	}
	public  EnemyExistPlace m_existplace 
	{
		get;
		set;
	}
	public int m_attackpower 
	{
		get;
		set;
	}
	public int m_attackmode 
	{
		get;
		set;
	}
	public int m_attackdistance 
	{
		get;
		set;
	}
	public int m_speed 
	{
		get;
		set;
	}
	public int m_hp 
	{
		get;
		set;
	}
	public  int m_score 
	{
		get;
		set;
	}
	// Use this for initialization
	void Awake()
	{

	}
	void Start () 
	{
		m_bee = GameObject.Find ("JBMainChar");
		if(m_name=="wasp")
		{
			m_weapon = EnemyWeapon.none;
			m_movingmode = MovingMode.flying;
			m_attackmode = 0;
			m_attackpower = 50;
			m_attackdistance = 300;
			m_existplace = EnemyExistPlace.sky;
			m_hp = 20;
			m_speed = 7;
			m_score = 50;
		}
        if (m_name == "wasp_new")
        {
            m_weapon = EnemyWeapon.none;
            m_movingmode = MovingMode.flying;
            m_attackmode = 0;
            m_attackpower = 80;
            m_attackdistance = 300;
            m_existplace = EnemyExistPlace.sky;
            m_hp = 20;
            m_speed = 7;
            m_score = 50;
        }
		else if(m_name=="redAnt")
		{
			m_weapon = EnemyWeapon.none;
			m_movingmode = MovingMode.crawling;
			m_attackmode = 0;
			m_attackpower = 60;
			m_attackdistance = 300;
			m_existplace = EnemyExistPlace.land;
			m_hp = 20;
			m_speed = 6;
			m_score = 50;
		}
		else if(m_name=="blackAnt")
		{
			m_weapon = EnemyWeapon.none;
			m_movingmode = MovingMode.crawling;
			m_attackmode = 0;
			m_attackpower = 60;
			m_attackdistance = 300;
			m_existplace = EnemyExistPlace.land;
			m_hp = 20;
			m_speed = 6;
			m_score = 50;
		}
		else if(m_name=="mite")
		{
			m_weapon = EnemyWeapon.none;
			m_movingmode = MovingMode.crawling;
			m_attackmode = 0;
			m_attackpower = 60;
			m_attackdistance = 300;
			m_existplace = EnemyExistPlace.land;
			m_hp = 20;
			m_speed = 3;
			m_score = 50;
		}
		else if(m_name=="mantis")
		{
			m_weapon = EnemyWeapon.none;
			m_movingmode = MovingMode.crawling;
			m_attackmode = 0;
			m_attackpower = 60;
			m_attackdistance = 300;
			m_existplace = EnemyExistPlace.land;
			m_hp = 20;
			m_speed = 4;
			m_score = 50;
		}
		else if(m_name=="spider1")
		{
			m_weapon = EnemyWeapon.none;
			m_movingmode = MovingMode.hanging;
			m_attackmode = 0;
			m_attackpower = 60;
			m_attackdistance = 300;
			m_existplace = EnemyExistPlace.land;
			m_hp = 20;
			m_speed = 5;
			m_score = 50;
		}
		else if(m_name=="spider2")
		{
			m_weapon = EnemyWeapon.none;
			m_movingmode = MovingMode.hanging;
			m_attackmode = 0;
			m_attackpower = 60;
			m_attackdistance = 300;
			m_existplace = EnemyExistPlace.land;
			m_hp = 20;
			m_speed = 5;
			m_score = 50;
		}

		EnemyHouse = GameObject.Find ("Enemy");
		float ran_x, ran_y;
		if(m_existplace==EnemyExistPlace.land)
		{
			ran_x=Random.Range(450.0f,500.0f);
			ran_y=-270.0f;
		}
		else if(m_existplace==EnemyExistPlace.sky)
		{
			ran_x=Random.Range (450.0f, 500.0f);
			ran_y=Random.Range (-160.0f, 160.0f);
		}
		else if(m_existplace==EnemyExistPlace.land_sky)
		{
			ran_x=Random.Range (450.0f, 500.0f);
			ran_y=Random.Range (-160.0f, 160.0f);
		}
		else 
		{
			ran_x=Random.Range (450.0f, 500.0f);
			ran_y=Random.Range (-160.0f, 160.0f);
		}

		if(m_movingmode==MovingMode.hanging)
		{
			ran_x=Random.Range(600,1000);
			ran_y=300;
			ProcessRope(ran_x,ran_y);
		}
		transform.SetParent(EnemyHouse.transform);
		transform.localScale=Vector3.one;

        if (m_name != "wasp" && m_name != "wasp_new") 
		{
			pos=new Vector2(ran_x,ran_y);
		}
		transform.localPosition = pos;

		//SetEnemyAnimIMG ();
	}
	void OnEnable()
	{
		this._killed += DisEnemy;
	}
	void OnDisable()
	{
		this._killed -= DisEnemy;
	}
	// Update is called once per frame
	void Update () 
	{
		if (JBKernel.GM_state ==JBKernel.GameState.playing) 
		{
		
			Vector2 position = transform.position;
			if(m_movingmode!=MovingMode.hanging)
			{
				position = new Vector2 (position.x - m_speed * Time.deltaTime * 30, position.y);
			}
			else
			{
                position = new Vector2(position.x - m_speed * Time.deltaTime * 30, position.y - m_speed * Time.deltaTime * 10);
			}
			transform.position = position;
			//bottom left point
			if (!IsinScreenArea()) 
			{
				Destroy (gameObject);
			}
            if (IsEnterBombArea())
            {
                JBKernel.score += m_score;
                JBKernel._killedEnemyCount++;
                if (JBKernel._killedEnemyCount > 5)
                {
                    JBKernel._killedEnemyCount = 0;
                    JBMainCharacterObj.m_CharHP += 50;
                    JBMainCharacterObj.m_CharPower += 1;
                    if (JBMainCharacterObj.m_CharHP > 500)
                    {
                        JBMainCharacterObj.m_CharHP = 500;
                    }
                    if (JBMainCharacterObj.m_CharPower > 10)
                    {
                        JBMainCharacterObj.m_CharPower = 10;
                    }
                }
                PlayExplosion();
                Invoke("DisEnemy", 0.2f);
            }
			if(m_name.Contains("redAnt")){
				shootCount++;
				if(shootCount>shootFrame){
					shoot(1);
					shootCount=0;
				}
			}
			if(!rushFlag){
				if(m_name.Contains("mite"))
				{
					float thrsh=Mathf.Abs(m_bee.transform.localPosition.y+270);
					if(thrsh<10){
						rushFlag=true;
						rushAttack();
					}
				}
			}
			if(m_name.Contains("mantis"))
			{
				if(flyState)
				{
					position = new Vector2 (position.x, position.y+3);
					if(transform.localPosition.y>-100)
						flyState=false;
				}
				else{
					position = new Vector2 (position.x, position.y-3);
					if(transform.localPosition.y<-260)
						flyState=true;
				}
				transform.position=position;
			}
		} 
	}
	void OnTriggerEnter(Collider other)
	{
		//Collision of player ship and bullet
		if (other.gameObject.name=="JBMainChar")
		{
			this._killed+=DisEnemy;
			_killed();
		}
		else if(other.gameObject.name=="bullet(Clone)")
		{
            if (m_name == "wasp_new")
            {
                JBKernel.m_Bomb = true;
                JBKernel.m_BombPos = gameObject.transform.localPosition;
                SubBomb.GetComponent<JBSubBomb>().SubBombExplosion();
                Invoke("DisEnemy", 0.9f);
            }
            else
            {
                PlayExplosion();
                Invoke("DisEnemy", 0.2f);
            }
			JBKernel.score+=m_score;
			JBKernel._killedEnemyCount++;
			if(JBKernel._killedEnemyCount>5)
			{
				JBKernel._killedEnemyCount=0;
				JBMainCharacterObj.m_CharHP+=50;
				JBMainCharacterObj.m_CharPower+=1;
				if(JBMainCharacterObj.m_CharHP>500)
				{
					JBMainCharacterObj.m_CharHP=500;
				}
				if(JBMainCharacterObj.m_CharPower>10)
				{
					JBMainCharacterObj.m_CharPower=10;
				}
			}
		}

	}

	void PlayExplosion()
	{
		GameObject explosion = (GameObject)Instantiate (particle);
		explosion.transform.SetParent (transform);
		explosion.transform.localScale=Vector2.one;
		explosion.transform.localPosition = Vector2.zero;
	}
	void DisEnemy()
	{
//		GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Atlas/"+m_name+"/"+m_name+"Killed");
//		GetComponent<JBSpriteAnim> ().Loop = true;
//		GetComponent<JBSpriteAnim> ().frames = 2;
//		GetComponent<JBSpriteAnim> ()._prefix = m_name + "Killed";
//		GetComponent<JBSpriteAnim> ()._folderName = "Atlas/"+m_name;

		gameObject.AddComponent<TweenPosition> ();
		gameObject.GetComponent<TweenPosition> ().from = transform.localPosition;
		gameObject.GetComponent<TweenPosition> ().to = transform.localPosition + new Vector3 (0, -700, 0);
		gameObject.GetComponent<TweenPosition> ().duration = 1.0f;
		gameObject.GetComponent<TweenPosition> ().PlayForward ();
		DestroyObject (gameObject);
        //Invoke("destoryObject", 0.6f);
	}

    void destoryObject()
    {
        DestroyObject(gameObject);
    }
	bool IsinScreenArea()
	{
		float width = 1136 / 2;
		//float height = 640 / 2;
		if(transform.localPosition.x<=-width)
		{
		   return false;
		}
		if(transform.localPosition.y<-270)
		{
			return false;
		}
		return true;
	}

    bool IsEnterBombArea()
    {
        if (!JBKernel.m_Bomb)
            return false;

        float dis = Mathf.Abs(JBKernel.m_BombPos.x - transform.localPosition.x);
        float dis1 = Mathf.Abs(JBKernel.m_BombPos.y - transform.localPosition.y);
        if (dis < 150 && dis1 < 150)
        {
            return true;
        }
        return false;
    }

	void SetEnemyAnimIMG()
	{
		GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Atlas/"+m_name+"/"+m_name+"Flying");
		GetComponent<JBSpriteAnim> ().Loop = true;
		GetComponent<JBSpriteAnim> ().frames = 2;
		GetComponent<JBSpriteAnim> ()._prefix = m_name + "Flying";
		GetComponent<JBSpriteAnim> ()._folderName = "Atlas/"+m_name;
	}
	public void setPos(float posX,float posY)
	{
		pos = new Vector2 (posX,posY);
	}
	void ProcessRope(float _x,float _y)
	{
		//float length = 320 - _y;
		//transform.GetChild (0).localPosition = new Vector3 (0,length/2+10,0);
		//transform.GetChild (0).GetComponent<BoxCollider> ().size = new Vector3 (5,length,0);
		transform.GetChild (0).GetComponent<BoxCollider> ().isTrigger = true;
	}
	void shoot(int kind){
		//kind 1: red ant,2: black ant
		GameObject enemy_bullet = Instantiate (m_bullet) as GameObject;
		enemy_bullet.transform.SetParent (GameObject.Find ("JBBullet").transform);
		enemy_bullet.transform.position = transform.position;
		enemy_bullet.transform.localScale = new Vector2 (1, 1);
	}
	void rushAttack(){
		m_speed = 10;
	}
}
