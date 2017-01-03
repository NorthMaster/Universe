using UnityEngine;
using System.Collections;

public class SelectCamera : MonoBehaviour {

	public GameObject[] DoubleORSingle;//两个摄像机引用（PT、VR）
	public AudioClip Sound;//声音资源
	void Start () {
	
	}
	void Awake()
	{
		/*
		 * 由于普通模式和虚拟现实模式公用一个场景，所以在加载该场景时需要判断使用哪一个摄像机
		 */
		if (Constraints.MS_Selected == "PuTong") {
			DoubleORSingle [0].SetActive (true);
		} else if (Constraints.MS_Selected == "VR") {
			DoubleORSingle[1].SetActive(true);
		}
	}
	public void BackToMenu()
	{
		if(Constraints.YinXiao=="open")//添加音效
		{
			AudioSource.PlayClipAtPoint(Sound,new Vector3(0,0,0));//设置播放片段的位置，离摄像机越近越清晰【一般设置与摄像机位置相同】
		}
		Application.LoadLevelAsync ("Start");
		Constraints.BackTemp = "back";
	}
	void Update () {
		
	}
}
