using UnityEngine;
using System.Collections;
using dataread;
using JBClasses;

public class JBBecky : MonoBehaviour 
{
	int level;
	public GameObject _darkSpirit;
    public GameObject helpme;

	public JBUIsystem _uisys;
    public JBMainCharacterObj m_Mainchar;
	int curState;//0-kinapped; 1-rescued;

    

	// Use this for initialization
	void Start () 
	{
        if (JBDB.levelint == 1 || JBDB.levelint == 4 || JBDB.levelint == 5 || JBDB.levelint == 9 || JBDB.levelint == 12 || JBDB.levelint == 13
            || JBDB.levelint == 17 || JBDB.levelint == 20 || JBDB.levelint == 24 || JBDB.levelint == 25 || JBDB.levelint == 30
            || JBDB.levelint == 28 || JBDB.levelint == 33 || JBDB.levelint == 38 || JBDB.levelint == 39)
		{
			curState=0;
		}
		else if(JBDB.levelint==2)
		{
			curState=1;
		}
		else if(JBDB.levelint==3)
		{
			curState=3;
		}
		else 
		{
			curState=1;
		}
		if(curState==0)
		{
			GetComponent<Animator>().enabled=true;
			GetComponent<Animator>().Play("BeckyAnim");
			_uisys.DisplayMothText("Rescue Blossom!");
			StartCoroutine("settingState");
		}
		else if(curState==1)
		{
			gameObject.SetActive(false);
			GetComponent<Transform>().localPosition=new Vector3(521,0,0);
			_darkSpirit.SetActive(false);
			//GetComponent<TweenPosition>().enabled=true;
			//GetComponent<TweenPosition>().PlayForward();
		}
        else if (curState == 3)
        {

            _darkSpirit.SetActive(false);
            helpme.SetActive(false);
        }

        if (JBMainCharacterObj._instance != null)
        {
            m_Mainchar = JBMainCharacterObj._instance;
        }
	}
	IEnumerator settingState()
	{
		yield return new WaitForSeconds(2.0f);
		_uisys.DisappearText ();
		gameObject.SetActive (false);
	}
	// Update is called once per frame
	void Update () 
	{
        if (curState == 3)
        {
            Vector3 targetPos = m_Mainchar.transform.localPosition;
            if (JBKernel.m_bAttackDirInvert)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;
                targetPos.x += 60;
            }
            else
            {
                Vector3 scale = transform.localScale;
                scale.x = -Mathf.Abs(scale.x);
                transform.localScale = scale;
                targetPos.x -= 60;
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * 10);
        }
	}

}
