using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JBSpriteAnim : MonoBehaviour
{
	public string _prefix;
	public string _folderName;
	private Sprite[] _sprites;
	public bool Loop;
	public int frames;
	public float settingTime = 0.05f;
	float sumTime;
	int FrameNumber;
	void Start()
	{
		_sprites=new Sprite[20];
		settingTime = 0.05f;
		sumTime = 0;
		FrameNumber = 0;
		for(int i=0;i<frames;i++)
		{
			string path=_folderName+"/"+_prefix+i;
			_sprites[i]=Resources.Load<Sprite>(path);
		}

	}
	void OnEnable()
	{
		FrameNumber = 0;
	}
	void Update()
	{
		sumTime += Time.deltaTime;
		if (sumTime >= settingTime) 
		{
			sumTime=0;
			ChangeFrame ();
		}
	}
	void ChangeFrame()
	{
		if (Loop) 
		{
			if (FrameNumber >= frames) 
			{

				FrameNumber = 0;
				return;
			}
			transform.GetComponent<Image> ().sprite = _sprites [FrameNumber];
			FrameNumber++;
		}
		else 
		{
			if (FrameNumber >= frames) 
			{
				return;
			}
			transform.GetComponent<Image>().sprite=_sprites[FrameNumber];
			FrameNumber++;
		}
	}
}

