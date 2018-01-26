using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using JBClasses;
namespace dataread
{
public class JBDB {
	
	public class MySaveData
	{
		public  JBUserProfile _profile;
		public  JBCharacters character;
		public  int TotalScore=0;
		public  float level;
		public  ItemCounts items;
	}
	public struct ItemCounts
	{
		public int HoneyCount;
		public int FlowerCount;
		public int KissCount;
		public int HeartCount;
	}
	public static MySaveData MyData=new MySaveData();
	
	public static string level;
	public static float levelint;
	public static int TotalScore=0;
	public static int Coin;
	public static JBUserProfile _profile;
	public static int Lives;
    public static bool IsPayCompleted = false;

	public static string enemy_name,boss_name,enemy_count,enemy_speed,boss_count,boss_speed;//"boss_speed" is value of speeed of launch of bossbullet;
	public static JBLevelEnemyData levelEnemy;
	public static JBLevelBossData levelBoss;
	public static bool IsFirstEntered;
	public static string YourID;
	public static string YourPhoneNumber;
	public static ItemCounts items;
	public static JBCharacters _character;
	public static int[] enableChars;

	public static DateTime lastTime;
	public static string characterName;
	public static JBEnemyCharacterData[] EnemyCharData;
	public static Sprite[] ImportedCharIMG;
	
	public static bool gamestate;
	public static bool SoundFlag;
	public static bool MusicFlag;
	public static bool HandFlag;
	
	public static  Dictionary<string,int> ItemCost=new Dictionary<string, int>(){{"Honey",2000},{"Heart",2500},{"Flower",2500},{"Kiss",3000}};
	public static Dictionary<string,int> CharCost=new Dictionary<string,int>(){{"CarpenterBee",5000},{"BumbleBee",15000},{"KillerBee",30000}};
    // Use this for initialization
	public static void LevelDataReading()
	{
		TextAsset data = Resources.Load ("Level/level"+levelint) as TextAsset;
		string[] content = data.text.Split('\n');
		string[] tmp=content[0].Split(' ');
		for(int j=1;j<tmp.Length;j++)
		{
			if(tmp[j-1]=="enemy_kind")
			{
				int n=System.Convert.ToInt32(tmp[j]);
				levelEnemy.enemy_kind=n;
				for(int k=0;k<n;k++)
				{
					for(int kk=3;kk<tmp.Length;kk++)
					{
					 if(tmp[kk-1]=="enemy_name"+(k+1)) 
					 {
							levelEnemy.enemy_name[k]=tmp[kk];
					 }
					 if(tmp[kk-1]=="count"+(k+1))
					 { 
							levelEnemy.enemy_count[k]=System.Convert.ToInt32(tmp[kk]);
					 }
				  }
				}
			}	
		}
		string[] Tmp=content[1].Split(' ');
		for(int j=1;j<Tmp.Length;j++)
		{
			if(Tmp[j-1]=="boss_kind")
			{
				int n=System.Convert.ToInt32(Tmp[j]);
				levelBoss.boss_kind=n;
			for(int k=0;k<n;k++)
			{
				for(int kk=3;kk<Tmp.Length;kk++)
				{
					if(Tmp[kk-1]=="boss_name"+(k+1)) 
					{
						levelBoss.boss_name[k]=Tmp[kk];
					}
					if(Tmp[kk-1]=="count"+(k+1))
					{ 
						levelBoss.boss_count[k]=System.Convert.ToInt32(Tmp[kk]);
					}
				}
			 }
			}
		}
	}
	public static void ReadGameDataText()
	{	
		string filepath = Application.persistentDataPath + "/savedata.txt";
			if (!File.Exists (filepath)) 
			{
				JBDB.TotalScore=0;
				JBDB.Coin=20000;
				JBDB.Lives=7;
				items.HoneyCount=0;
				items.HeartCount=5;
				items.FlowerCount=0;
				items.KissCount=0;
				JBDB.levelint=1;
                //TEST
                JBDB.levelint = 50;
				JBDB._character=JBCharacters.JUNGLEBEE;
                JBDB.IsPayCompleted = false;

				enableChars=new int[3];
				for(int i=0;i<3;i++)
				{
					enableChars[i]=0;
				}
				JBLifeTimer.time=300;
			}
			else 
			{
				StreamReader filestream=new StreamReader(filepath);
				levelint=System.Convert.ToInt16(filestream.ReadLine());
                //TEST
                JBDB.levelint = 50;
				
				TotalScore=System.Convert.ToInt32(filestream.ReadLine());
				Coin=System.Convert.ToInt32(filestream.ReadLine());
				Lives=System.Convert.ToInt16(filestream.ReadLine());
				_profile=new JBUserProfile();
				_profile.YourID=filestream.ReadLine();
				_profile.YourPhoneNumber=filestream.ReadLine();
				characterName=filestream.ReadLine();
				enableChars[0]=System.Convert.ToInt16(filestream.ReadLine());
				enableChars[1]=System.Convert.ToInt16(filestream.ReadLine());
				enableChars[2]=System.Convert.ToInt16(filestream.ReadLine());
				items.HoneyCount=System.Convert.ToInt16(filestream.ReadLine());
				items.HeartCount=System.Convert.ToInt16(filestream.ReadLine());
				items.FlowerCount=System.Convert.ToInt16(filestream.ReadLine());

				items.KissCount=System.Convert.ToInt16(filestream.ReadLine());
				lastTime=Convert.ToDateTime(filestream.ReadLine());
				TimeProcess();
                IsPayCompleted = System.Convert.ToBoolean(filestream.ReadLine());
                //JBDB.levelint = 1;
			}
			JBDB.MusicFlag=true;
			JBDB.SoundFlag=true;
			JBDB.HandFlag=false;
		}
	static public void TimeProcess()
	{
			TimeSpan temp =DateTime.Now-lastTime;
			if (temp.TotalMinutes >= 5) {
				JBDB.Lives = 7;
			}
			else 
			{
				//JBLifeTimer.m_start=true;
				//JBLifeTimer.time=300;
				 //JBLifeTimer.min=(int)temp.TotalMinutes%5;
				 //JBLifeTimer.sec=temp.Seconds;
			}
	}
	static public void SaveGameDataText()
	{
		string filepath = Application.persistentDataPath + "/savedata.txt";
		StreamWriter filestream = new StreamWriter (filepath);
		filestream.WriteLine (levelint);
		filestream.WriteLine (TotalScore);
		filestream.WriteLine (Coin);
		filestream.WriteLine (Lives);
		filestream.WriteLine (_profile.YourID);
		filestream.WriteLine (_profile.YourPhoneNumber);
		filestream.WriteLine (characterName);
		filestream.WriteLine (enableChars[0]);
		filestream.WriteLine (enableChars[1]);
		filestream.WriteLine (enableChars[2]);
		filestream.WriteLine (items.HoneyCount);
		filestream.WriteLine (items.HeartCount);
		filestream.WriteLine (items.FlowerCount);
		filestream.WriteLine (items.KissCount);
		filestream.WriteLine (DateTime.Now);
        filestream.WriteLine(IsPayCompleted);
		filestream.Close ();
	}
	public static void ReadGameDataBinary()
	{
			string filepath = Application.persistentDataPath + "/savedata.sav";
			if(!File.Exists(filepath))
			{
				return;
			}
			BinaryFormatter bf=new BinaryFormatter();
			FileStream stream = File.Open (filepath, FileMode.Open);
			MyData = bf.Deserialize (stream) as MySaveData;
			stream.Close ();
			GetData ();
	}
	public static void SaveGameDataBinary()
	{
			SetData ();
			////
			string filepath = Application.persistentDataPath + "savedata.sav";
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = File.Create (filepath);
			bf.Serialize (stream,MyData);
			stream.Close();
	}
	public static void SetData()
	{
			MyData._profile = _profile;
			MyData.character = _character;
			MyData.TotalScore = TotalScore;
			MyData.level = levelint;
			MyData.items = items;
	}
	public static void GetData()
	{
			_profile = MyData._profile;
			_character = MyData.character;
			TotalScore = MyData.TotalScore;
			levelint = MyData.level;
			items = MyData.items;
	}
	public static void ImportCharIMG()
	{
			ImportedCharIMG=new Sprite[20];
			ImportedCharIMG [0] = Resources.Load<Sprite> ("Atlas/"+characterName+"/"+characterName+"Failed0");
			ImportedCharIMG [1] = Resources.Load<Sprite> ("Atlas/"+characterName+"/"+characterName+"Failed1");
			ImportedCharIMG[2]=Resources.Load<Sprite> ("Atlas/"+characterName+"/"+characterName+"Flying0");
			ImportedCharIMG[3]=Resources.Load<Sprite> ("Atlas/"+characterName+"/"+characterName+"Flying1");
			ImportedCharIMG[4]=Resources.Load<Sprite> ("Atlas/"+characterName+"/"+characterName+"Happy0");
			ImportedCharIMG[5]=Resources.Load<Sprite> ("Atlas/"+characterName+"/"+characterName+"Happy1");
			ImportedCharIMG[6]=Resources.Load<Sprite> ("Atlas/"+characterName+"/"+characterName+"Killed");
			ImportedCharIMG[7]=Resources.Load<Sprite> ("Atlas/"+characterName+"/"+characterName+"Menu0");
			ImportedCharIMG[8]=Resources.Load<Sprite> ("Atlas/"+characterName+"/"+characterName+"Menu1");
			ImportedCharIMG [9] = Resources.Load<Sprite> ("Atlas/" + characterName + "/" + characterName + "Shooting0");
			ImportedCharIMG [10] = Resources.Load<Sprite> ("Atlas/" + characterName + "/" + characterName + "Shooting1");
			ImportedCharIMG [11] = Resources.Load<Sprite> ("Atlas/" + characterName + "/" + characterName + "Shooting2");
			ImportedCharIMG [12] = Resources.Load<Sprite> ("Atlas/" + characterName + "/" + characterName + "Shooting3");
	}
	public static void ReadEnemyCharacterTable()
	{
			int cap = 25;
			//EnemyCharData = new List<JBEnemyCharacterData> (cap);
			EnemyCharData=new JBEnemyCharacterData[cap];
			TextAsset data = Resources.Load ("GameEnemyCharacterTable") as TextAsset;
			string[] content = data.text.Split('\n');
			int character_length = content.Length;
			for(int i=0;i<character_length;i++)
			{
				string[] tmp=content[i].Split(' ');
				for(int j=1;j<tmp.Length;j++)
				{
					if(tmp[j-1]=="number") 
					{
					
						EnemyCharData[i].number=System.Convert.ToInt32(tmp[j]);
					}
					else if(tmp[j-1]=="name")
					{
						EnemyCharData[i].name=tmp[j];
					}
					else if(tmp[j-1]=="weapon")
					{
						EnemyCharData[i].weapon=GetRealWeaponData(tmp[j]);
					}
					else if(tmp[j-1]=="speed")
					{
						if(tmp[j]=="none")
						{
							EnemyCharData[i].speed=0;
							continue;
						}
						EnemyCharData[i].speed=System.Convert.ToInt32(tmp[j]);
					}
					else if(tmp[j-1]=="moving_mode")
					{
						EnemyCharData[i].movemode=GetRealMovingData(tmp[j]);
					}
					else if(tmp[j-1]=="attack_mode")
					{
						EnemyCharData[i].attackmode=0;
					}
					else if(tmp[j-1]=="attack_power")
					{
						if(tmp[j]=="none"){
							EnemyCharData[i].attackpower=0;
							continue;
						}
						EnemyCharData[i].attackpower=System.Convert.ToInt32(tmp[j]);
					}
					else if(tmp[j-1]=="attack_distance")
					{
						if(tmp[j]=="none"){
							EnemyCharData[i].attackdistance=0;
							continue;
						}
						EnemyCharData[i].attackdistance=System.Convert.ToInt32(tmp[j]);
					}
					else if(tmp[j-1]=="exist_place")
					{
						EnemyCharData[i].existType=GetRealExistData(tmp[j]);
					}
					else if(tmp[j-1]=="hp")
					{
						if(tmp[j]=="none"){
							EnemyCharData[i].hp=0;
							continue;
						}
						EnemyCharData[i].hp=System.Convert.ToInt32(tmp[j]);
					}
					else if(tmp[j-1]=="score")
					{
						if(tmp[j]=="none"){
							EnemyCharData[i].score=0;
							continue;
						}
						EnemyCharData[i].hp=System.Convert.ToInt32(tmp[j]);
					}
			}
		}
	}
	public static MovingMode GetRealMovingData(string moving)
	{
			if (moving == "flying") {
				return MovingMode.flying;
			} else if (moving == "crawling") {
				return MovingMode.crawling;
			} else if (moving == "walking") {
				return MovingMode.walking;
			} else if (moving == "spread") {
				return MovingMode.spread;
			} else if (moving == "wraping") {
				return MovingMode.wraping;
			} else {
				return MovingMode.none;
			}
	}
	public static BossWeapon GetRealWeaponData(string weapon)
	{
			if (weapon == "stinger") {
				return BossWeapon.stinger;
			} else if (weapon == "spear") {
				return BossWeapon.spear;
			} else if (weapon == "mouth") {
				return BossWeapon.mouth;
			} else if (weapon == "yap") {
				return BossWeapon.yap;
			}
			else if (weapon == "sword") 
			{
				return BossWeapon.sword;
			} 
			else if (weapon == "sword") 
			{
				return BossWeapon.sword;
			}
			else if(weapon=="net")
			{
				return BossWeapon.net;
			}
			else 
			{
				return BossWeapon.none;
			}
	}
	public static BossExistType GetRealExistData(string exist)
	{
			if (exist=="sky") 
			{
				return BossExistType.SKY;
			}
			else if(exist=="land")
			{
				return BossExistType.LAND;
			}
			else if(exist=="land_sky")
			{
				return BossExistType.LAND_SKY;
			}
			else 
			{
				return BossExistType.NONE;
			}
	}
}
public struct JBEnemyCharacterData
{
		public int number;
		public string name;
		public BossWeapon weapon;
		public MovingMode movemode;
		public int attackmode;
		public int attackpower;
		public int attackdistance;
		public BossExistType existType;
		public int hp;
		public int speed;
		public int score;

	public JBEnemyCharacterData(int level)
		{
			number = 0;
			name = "";
			weapon = BossWeapon.none;
			movemode = MovingMode.none;
			attackmode = 0;
			attackpower = 0;
			attackdistance = 0;
			existType = BossExistType.NONE;
			hp = 0;
			speed = 0;
			score = 0;
		}
}
public struct JBLevelEnemyData
{
		public int enemy_kind;
		public string[] enemy_name;
		public int[] enemy_count;
}
public struct JBLevelBossData
	{
		public int boss_kind;
		public string[] boss_name;
		public int[] boss_count;
	}
}