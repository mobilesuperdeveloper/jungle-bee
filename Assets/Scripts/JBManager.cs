using UnityEngine;
using System.Collections;
using dataread;
public class JBManager : MonoBehaviour {

	public GameObject CharSelect;
	public GameObject profileWindow;
	public GameObject levelmap;
	public GameObject audioMusic;
	public GameObject lifeDlg;
	public static JBManager _instance;
	float waitingtime=1.5f;
	float vibtime=1.5f;

	// Use this for initialization
	void Start () 
	{
		_instance = this;
		if(Application.loadedLevelName=="Loading_Scene1")
		{
			JBDB.MusicFlag=true;
		}
		if(JBDB.MusicFlag)
		{
			audioMusic.GetComponent<AudioSource>().mute=false;
		}
		else
		{
			audioMusic.GetComponent<AudioSource>().mute=true;
		}
		if (Application.loadedLevelName == "Loading_Scene1") 
		{
			Invoke ("OccurVibration",vibtime);
			Invoke("PresentProfile",waitingtime);
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(JBDB.MusicFlag)
		{
			if(audioMusic!=null)
			{
				audioMusic.GetComponent<AudioSource>().mute=false;
			}
		}
		else
		{
			if(audioMusic!=null)
			{
				audioMusic.GetComponent<AudioSource>().mute=true;
			}
		}
		if(Application.loadedLevelName=="Start_Scene"){
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			StartCoroutine("ExitGame");
		}
		}
		if(Application.loadedLevelName=="Map_Scene")
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				StartCoroutine("GotoStart_Scene");
			}
		}
	}
	public void OccurVibration()
	{
		Handheld.Vibrate ();
	}
	public void GotoStart_Scene()
	{
		Application.LoadLevel ("Start_Scene");
	}
	public void GotoLoading_Scene2()
	{
		if(JBDB.Lives>0)
		{
			CharSelect.SetActive (true);
		}
		else
		{
			lifeDlg.SetActive(true);
		}
	}
	public void GotoPlayScene()
	{

	}
	public void PresentProfile()
	{
		if(profileWindow!=null)
		{
			profileWindow.SetActive(true);
		}
	}
	public void ExitGame()
	{
		JBDB.SaveGameDataText ();
		Application.Quit ();
	}
}
