using UnityEngine;
using System.Collections;

public class ARBackToMenu : MonoBehaviour {
	public AudioClip Sound;//声音资源
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ARBackToMenu_Click()
	{
		if(Constraints.YinXiao=="open")//添加音效
		{
			AudioSource.PlayClipAtPoint(Sound,new Vector3(0,0,0));//设置播放片段的位置，离摄像机越近越清晰
		}
		Application.LoadLevelAsync ("Start");
	}
}
