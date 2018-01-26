using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using dataread;
using JBClasses;

public class JBDisguiseEnemy : MonoBehaviour {
	int kind;//0--disguise Enemy 1-- real moth;
	public JBMainCharacterObj m_Mainchar;
	public JBUIsystem _uisystem;
	public GameObject m_bullet;
	int count=0;
	int shootCount=0;
	int bulletCount=0;
	Vector3 curDir;
	float num=0.0f;
    bool m_Died = false;
	// Use this for initialization
	void Start () 
	{

		m_Mainchar = JBMainCharacterObj._instance;
		transform.localPosition=new Vector3(284,0,0);
		curDir = randDir ();
		kind = Random.Range (0,2);
		if(kind!=0)
		{
			gameObject.AddComponent<TweenPosition> ();
			gameObject.GetComponent<TweenPosition> ().from = transform.localPosition;
			gameObject.GetComponent<TweenPosition> ().to = transform.localPosition + new Vector3 (-500,0,0);
			gameObject.GetComponent<TweenPosition> ().duration = 1f;
			gameObject.GetComponent<TweenPosition> ().PlayForward ();
			_uisystem.DisplayMothText("You rescued the Moth!");
			StartCoroutine("setting_state",3f);
			JBKernel.score+=2000;
		}

        m_Died = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if(kind==0)
		{
            JBKernel.m_JBBoss = this.gameObject;
		    if (JBKernel.GM_state == JBKernel.GameState.playing) 
		    { 
    			ShootBullet();
    		}

			count++;
			Vector3 temp = transform.localPosition+curDir;
			if (count <= 30 && isInRange (temp)) 
			{
			transform.localPosition = temp;
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
		}
	}
	void ShootBullet()
	{
		int levelNum = (int)JBDB.levelint;
		float shootingTime = (300 - (levelNum + 20) * 4) * 0.01f;
		num += 1 / 60f;
		if (num >= shootingTime) 
		{
			if (shootCount % 20 == 0) 
			{
				GameObject boss_bullet = Instantiate (m_bullet) as GameObject;
				string png;
				png="YellowHornet";
				boss_bullet.GetComponent<Image>().sprite=Resources.Load<Sprite>("Atlas/"+png+"/"+png+"Bullet");
				boss_bullet.transform.SetParent (GameObject.Find ("JBBullet").transform);
				boss_bullet.transform.position = transform.position;
				boss_bullet.transform.localScale = new Vector2 (1, 1);
				bulletCount++;
			}
			shootCount++;
			if (bulletCount > levelNum + 2) {
				num = 0f;
				shootCount = 0;
				bulletCount = 0;
			}
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name=="bullet(Clone)"&&kind==0)
		{
            if (!m_Died)
			    DisSelf();
		}
	}
	void DisSelf()
	{
        JBKernel.m_JBBoss = null;
        m_Died = true;

		gameObject.AddComponent<TweenPosition> ();
		gameObject.GetComponent<TweenPosition> ().from = transform.localPosition;
		gameObject.GetComponent<TweenPosition> ().to = transform.localPosition + new Vector3 (0,-700,0);
		gameObject.GetComponent<TweenPosition> ().duration = 0.8f;
		gameObject.GetComponent<TweenPosition> ().PlayForward ();

		DestroyObject (gameObject, 1.2f);
		_uisystem.DisplayMothText ("You killed the Moth!");
		StartCoroutine("setting_state",1.0f);
	}
	IEnumerator setting_state(float t)
	{
		yield return new WaitForSeconds (t);
        JBKernel.GM_state = JBKernel.GameState.end_complete;
	}
	Vector3 randDir()
	{
		float levelNum = JBDB.levelint;
		float deltay = m_Mainchar.transform.localPosition.y - transform.localPosition.y;
		deltay *= (1 + levelNum / 10 * 2);
		Vector3 dir = new Vector3 (0,deltay/50,0);
		return dir;
	}
	bool isInRange(Vector3 bpoint)
	{
		if (bpoint.x > -586 && bpoint.x < 586 && bpoint.y > -320 && bpoint.y < 320)
			return true;
		else 
			return false;
	}
}
