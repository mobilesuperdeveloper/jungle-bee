using UnityEngine;
using System.Collections;

public class JBSubEnemy : MonoBehaviour {

    public GameObject particle;

    public delegate void EliminateSelf();
    public event EliminateSelf _killed;

    public int m_score = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //Collision of player ship and bullet
        if (other.gameObject.name == "JBMainChar")
        {
            this._killed += DisEnemy;
            _killed();
        }
        else if (other.gameObject.name == "bullet(Clone)")
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
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(particle);
        explosion.transform.SetParent(transform);
        explosion.transform.localScale = Vector2.one;
        explosion.transform.localPosition = Vector2.zero;
    }

    void DisEnemy()
    {
        //		GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Atlas/"+m_name+"/"+m_name+"Killed");
        //		GetComponent<JBSpriteAnim> ().Loop = true;
        //		GetComponent<JBSpriteAnim> ().frames = 2;
        //		GetComponent<JBSpriteAnim> ()._prefix = m_name + "Killed";
        //		GetComponent<JBSpriteAnim> ()._folderName = "Atlas/"+m_name;

        gameObject.AddComponent<TweenPosition>();
        gameObject.GetComponent<TweenPosition>().from = transform.localPosition;
        gameObject.GetComponent<TweenPosition>().to = transform.localPosition + new Vector3(0, -700, 0);
        gameObject.GetComponent<TweenPosition>().duration = 1.0f;
        gameObject.GetComponent<TweenPosition>().PlayForward();
        DestroyObject(gameObject);
    }
}
