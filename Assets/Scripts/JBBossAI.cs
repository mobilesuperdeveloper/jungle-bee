using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
using JBClasses;

public class JBBossAI : MonoBehaviour
{
	public JBBoss m_boss;
	public JBMainCharacterObj m_Mainchar;
	public GameObject m_bullet;
	float num=0.0f;
	//float alpha=0.0f;
	//float X0 = 284;
	//float Y0 = 0;
	//float R = 250;
	int count=0;
	int shootCount=0;
	int bulletCount=0;
	int repetition=0;
	int rushCount=1;
	public int rushFlag=0; //0:normal 1: attack 2: runaway
	Vector3 curDir;

    Vector3 m_TargetPos;

    bool m_bArrivedTarget = false;
    float m_speed = 0.5f;

	// Use this for initialization
	void Start ()
	{
		if (JBMainCharacterObj._instance != null) 
		{
			m_Mainchar = JBMainCharacterObj._instance;
		}

		m_boss.transform.localPosition=new Vector3(284,0,0);
		curDir = randDir ();

        m_bArrivedTarget = true;
	}
	// Update is called once per frame
	void Update ()
	{
		if (JBKernel.GM_state == JBKernel.GameState.playing) 
		{
			BossShootBullet();
		}

        _updateBossPos();

        /*
		count++;
		//Vector3 temp = m_boss.transform.localPosition+curDir;
		if (m_boss.m_BossName == "mantis_king") {
			rushCount++;
			if(rushCount>100) {
				rushCount=0;
				rushFlag=1;
			}
		}
		Vector3 temp;
		if (m_boss.m_BossName != "mantis_king")
			temp = m_boss.transform.localPosition + curDir;
		else {
			if(rushFlag==1){
				Vector3 dir=m_Mainchar.transform.localPosition-m_boss.transform.localPosition;
				dir.Normalize();
				temp = m_boss.transform.localPosition +dir*3;
			}
			else if(rushFlag==2){
				Vector3 dir=new Vector3(1,0,0);
				temp = m_boss.transform.localPosition +dir*8;
			}
			else {
				temp = m_boss.transform.localPosition + curDir;
			}
		}
		if (count <= 30 && isInRange (temp)) 
		{
			m_boss.transform.localPosition = temp;
		} 
		else if (count > 30&&isInRange (temp)) 
		{
			count = 0;
			curDir=randDir();
		}
		else 
		{
			curDir=new Vector3(curDir.x,-curDir.y,curDir.z);
		}
         */
	}

    void _updateBossPos()
    {
        if (m_bArrivedTarget)
        {
            float randx = Random.Range(-400, 400);
            float randy = Random.Range(-300, 300);

            m_TargetPos = new Vector3(randx, randy, 0);

            m_bArrivedTarget = false;
        }
        else
        {
            if (JBDB.levelint <= 10)
                m_speed = 1.0f;
            else if (JBDB.levelint <= 20)
                m_speed = 1.5f;
            else if (JBDB.levelint <= 30)
                m_speed = 2.0f;
            else if (JBDB.levelint <= 40)
                m_speed = 2.5f;
            else if (JBDB.levelint <= 50)
                m_speed = 3.0f;

            m_boss.transform.localPosition = Vector3.Lerp(m_boss.transform.localPosition, m_TargetPos, Time.deltaTime * m_speed);

            if (Mathf.Abs(m_TargetPos.x - m_boss.transform.localPosition.x) < 2 && Mathf.Abs(m_TargetPos.y - m_boss.transform.localPosition.y) <2)
                m_bArrivedTarget = true;
        }

    }

	void BossShootBullet()
	{
		int levelNum = (int)JBDB.levelint;
		float shootingTime = (300 - (levelNum + 20) * 4) * 0.01f;
		num += 1 / 60f;
		if (num >= shootingTime) 
		{

			if (shootCount % 20 == 0) 
			{
				repetition=1;
				//GameObject boss_bullet = Instantiate (m_bullet) as GameObject;
				string png;
				if(m_boss.m_BossName=="wasp_king")
				{
					png="WaspKing";
				}
				else if(m_boss.m_BossName=="yellow_hornet")
				{
					png="YellowHornet";
				}
				else
				{
					png="YellowHornet";
				}
				if(m_boss.m_BossName=="yellow_hornet")
				{
					repetition=3;
				}
				for(int i=0;i<repetition;i++)
				{
				GameObject boss_bullet = Instantiate (m_bullet) as GameObject;
				boss_bullet.GetComponent<Image>().sprite=Resources.Load<Sprite>("Atlas/"+png+"/"+png+"Bullet");
				boss_bullet.transform.SetParent (GameObject.Find ("JBBullet").transform);
				boss_bullet.transform.position = transform.position;
				boss_bullet.transform.localScale = new Vector2 (1, 1);
				boss_bullet.GetComponent<JBBossBullet>().direction=i;
				}
				bulletCount++;
				//scorpion special
				if(m_boss.m_BossName=="scorpion"){
					stoneFall();
				}
				if(m_boss.m_BossName=="dark_spirit(Clone)"){
					//EnemyFall();
				}
			}
			shootCount++;
			if (bulletCount > levelNum + 2) 
			{
				num = 0f;
				shootCount = 0;
				bulletCount = 0;
			}
		}

	}
	IEnumerator BossMove()
	{
		while (true) 
		{
			yield return new WaitForSeconds (0.5f);
			m_boss.transform.position = m_Mainchar.transform.position + new Vector3 (Mathf.Lerp(1,2,0.5f)*200,Mathf.PingPong(0.5f,300),0);
		}
	}
	Vector3 randDir()
	{
		float levelNum = JBDB.levelint;
		float deltay = m_Mainchar.transform.localPosition.y - m_boss.transform.localPosition.y;
		deltay *= (1 + levelNum / 10 * 2);
        float randx = Random.Range(-400, 400);

		Vector3 dir = new Vector3 (randx,deltay/50,0);
		return dir;
	}
	bool isInRange(Vector3 bpoint)
	{
		if (bpoint.x > -586 && bpoint.x < 586 && bpoint.y > -320 && bpoint.y < 320)
			return true;
		else {
			rushFlag = 0;
			return false;
		}
	}
	void stoneFall(){
		int number = Random.Range (1,3);
		for (int i=0; i<number; i++) {
			GameObject obsObj = Resources.Load<Object> ("Prefabs/FallingRock") as GameObject;
			GameObject _obs = Instantiate (obsObj) as GameObject;
			_obs.name = "obstacle";
			_obs.GetComponent<JBObstacle> ().m_type = 0;
			_obs.transform.SetParent (GameObject.Find("Enemy").transform);
			_obs.transform.localPosition = new Vector3 (2,-250,0);
			_obs.transform.localScale = Vector3.one;
		}
	}
	
	void EnemyFall()
	{
		GameObject.Find("JB_Kernel").GetComponent<JBKernel>().EnemyPresenting (0);
	}
}

