using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/*
摇杆控制脚本（挂载在摄像机上）【上帝视角+漫游】
 */
public class VR_YaoGan : MonoBehaviour
{
//	public GameObject VR_Cardboard;
//	public GameObject Head;
//	public GameObject Camera_Fart;
	public GameObject Left;
	//public Text data;
	void Update()
	{
		transform.Translate (Left.transform.forward*35*Time.deltaTime);
	}
}

