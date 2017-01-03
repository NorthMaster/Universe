using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//摄像机  陀螺仪转动
public class MobileGyro : MonoBehaviour
{
	Gyroscope gyro;//陀螺仪
	Quaternion quatMult;//四元数用于旋转
	Quaternion quatMap;
	GameObject player;
	GameObject camParent;
	public AudioClip Sound;//声音资源
	private Matrix4x4 QtMatrix4x4;
	
	public void BackToMenu()//返回主菜单

	{
		Application.LoadLevelAsync ("Start");
		Constraints.BackTemp = "";
		if(Constraints.YinXiao=="open")//添加音效
		{
			AudioSource.PlayClipAtPoint(Sound,new Vector3(0,0,0));//设置播放片段的位置，离摄像机越近越清晰
		}
	}
	public void ToMStar()//返回主菜单
	{
		if(Constraints.YinXiao=="open")//添加音效
		{
			AudioSource.PlayClipAtPoint(Sound,new Vector3(0,0,0));//设置播放片段的位置，离摄像机越近越清晰
		}
		Application.LoadLevelAsync ("MStar");
	}
	void Awake()
	{
		Transform currentParent = transform.parent;
		camParent = new GameObject ("camParent");
		camParent.transform.position = transform.position;
		transform.parent = camParent.transform;
		GameObject camGrandparent = new GameObject ("camGrandParent");
		camGrandparent.transform.position = transform.position;
		camParent.transform.parent = camGrandparent.transform;
		camGrandparent.transform.parent = currentParent;
		
		gyro = Input.gyro;//返回默认的陀螺仪
		gyro.enabled = true;//打开手机陀螺仪
		camParent.transform.eulerAngles = new Vector3(90,0, 0);
		quatMult = new Quaternion(0, 0, 1, 0);
	}
	void Update()
	{		
		quatMap = new Quaternion(gyro.attitude.x, gyro.attitude.y, gyro.attitude.z, gyro.attitude.w);//设置用于旋转的四元数
		Quaternion qt = quatMap * quatMult;
		transform.localRotation = qt;
	}
	Matrix4x4 QuatToMatrix4x4(Quaternion qt)//将四元数转换到四维矩阵
	{
		Matrix4x4 Mat=new Matrix4x4();
		float x, y, z, w;
		x = qt.x;
		y = qt.y;
		z = qt.z;
		w = qt.w;
		Vector4 Row0, Row1, Row2, Row3;
		Row0 = new Vector4 (1-2*y*y-2*z*z,2*x*y+2*w*z,2*x*z-2*w*y,0);
		Row1 = new Vector4 (2*x*y-2*w*z,1-2*x*x-2*z*z,2*y*z+2*w*x,0);
		Row2 = new Vector4 (2*x*z+2*w*y,2*y*z-2*w*x,1-2*x*x-2*y*y,0);
		Row3 = new Vector4 (0,0,0,1);
		Mat.SetRow (0,Row0);
		Mat.SetRow (1,Row1);
		Mat.SetRow (2,Row2);
		Mat.SetRow (3,Row3);
		return Mat;
	}
}










