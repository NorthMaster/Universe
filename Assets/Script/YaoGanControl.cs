using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/*
摇杆控制脚本（挂载在摄像机上）【上帝视角+漫游】
 */
public class YaoGanControl : MonoBehaviour
{
	private float[] axisInput = new float[2];//遥感参数变量
	public GameObject PT_Camera;
	public GameObject Left;
	void Start()
	{
		for (int i = 0; i < axisInput.Length; i++)//初始化遥感参数
			axisInput[i] = 0.0f;
	}
	void Awake()
	{
		gameObject.AddComponent <XuanZhuan>();//场景加载后给摄像机挂载脚本
	}
	void Update()
	{
		axisInput[0] = Input.GetAxisRaw("Horizontal") * Time.deltaTime;//获取遥感参数
		axisInput[1] = Input.GetAxisRaw ("Vertical") * Time.deltaTime;
		
		if(Constraints.GOD_MANYOU=="GOD"&&Constraints.MS_Selected=="PuTong")
		{
			YG_God();
		}
		if(Constraints.GOD_MANYOU=="GOD"&&Constraints.MS_Selected=="VR")
		{
			if(Constraints.VR_Alignment=="开启")
			{
				transform.GetComponent<Cardboard>().EnableAlignmentMarker=true;
			}else if(Constraints.VR_Alignment=="关闭")
			{
				transform.GetComponent<Cardboard>().EnableAlignmentMarker=false;
			}
			VR_ManYou();
		}
		if(Constraints.GOD_MANYOU=="MANYOU"&&Constraints.MS_Selected=="VR")
		{
			if(Constraints.VR_Alignment=="开启")
			{
				transform.GetComponent<Cardboard>().VRModeEnabled=true;
			}else if(Constraints.VR_Alignment=="关闭")
			{
				transform.GetComponent<Cardboard>().VRModeEnabled=false;
			}
			VR_ManYou();
		}
		if(Constraints.GOD_MANYOU=="MANYOU"&&Constraints.MS_Selected=="PuTong")
		{
			YG_ManYou();
		}
	}
	void VR_ManYou()
	{
		transform.Translate (Left.transform.forward*35*Time.deltaTime);
	}
	void YG_ManYou()//默认是【漫游】模式
	{
		//改变X值即抬头/低头，改变Y值即左右旋转
//		transform.Rotate(Vector3.up*axisInput[1]*5.0f);
//		transform.Rotate (Vector3.right*(-axisInput[0])*5.0f);		
//		transform.Translate (Vector3.forward*35*Time.deltaTime);
		transform.Rotate(Vector3.up*axisInput[1]*Constraints.YG_CanShu);
		transform.Rotate (Vector3.right*(-axisInput[0])*Constraints.YG_CanShu);		
		transform.Translate (Vector3.forward*35*Time.deltaTime);
	}
	void YG_God()
	{
		//改变X值即抬头/低头，改变Y值即左右旋转
		transform.Rotate(Vector3.up*axisInput[1]*30.0f);
		transform.Rotate (Vector3.right*axisInput[0]*30.0f);
		
		if (Input.GetKey(KeyCode.Joystick1Button10))//对应摇杆上的“Start”键事件监听
		{
			//摄像机上帝视角是否旋转控制
			if(PT_Camera.GetComponent<XuanZhuan>().AngularVelocity==0)
			{
				PT_Camera.GetComponent<XuanZhuan>().AngularVelocity=20;
			}else
			{
				PT_Camera.GetComponent<XuanZhuan>().AngularVelocity=0;
			}
			
		}
	}
}

