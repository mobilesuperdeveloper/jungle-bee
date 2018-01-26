using UnityEngine;
using System.Collections;

public class JBPauseController : MonoBehaviour {
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ReturnToGame()
	{
		JBKernel.GM_state = JBKernel.GameState.playing;
		gameObject.SetActive (false);
	}
	public void ReturnToMenu()
	{
		Application.LoadLevel ("Start_Scene");
	}
	public void Restart()
	{
		JBKernel.GM_state = JBKernel.GameState.playing;
		Application.LoadLevel ("Loading_Scene2");
		//gameObject.SetActive (false);
	}
}
