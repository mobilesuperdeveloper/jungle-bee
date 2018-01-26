using UnityEngine;
using System.Collections;
using dataread;
public class JBGameLoading : MonoBehaviour {
	// Use this for initialization
	void Start () 
	{
		JBDB.levelEnemy.enemy_name=new string[10];
		JBDB.levelEnemy.enemy_count=new int[10];
		JBDB.levelBoss.boss_count=new int[5];
		JBDB.levelBoss.boss_name=new string[5];
		JBDB.LevelDataReading ();
		JBDB.ImportCharIMG ();
		Invoke ("Next", 2f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void Next()
	{
		Application.LoadLevel ("Play_Scene");
	}
}
