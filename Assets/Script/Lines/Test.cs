using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
该脚本可以实现手机放在水平桌面上画面指向天底星座，手机举过头顶可以对准天顶星座，但是东南西北方向指向不明
 */
public class Test : MonoBehaviour {
	private float PI;//π值
	private float HCJJ = (float)(23 + 26 / 60 + (20.512 / 60)/60);//黄赤交角
	//***********************************************
	public float A=0;//代表方位角（不能用度数**需除以180度乘以PI）【0~360】
	public float α=0;//代表高度角（不能用度数**需除以180度乘以PI）【-90~+90】
	private float φ=39.62f;//代表观测者所在纬度（不能用度数**需除以180度乘以PI）
	private float H;//时角
	
	private float sinH=0;//时角正弦值
	private float cosH=0;//时角余弦值
	private float sinδ=0;//赤纬正弦值
	private float cosδ=0;//赤纬余弦值
	
	private float cosδcosH=0;
	private float cosδsinH=0;
	
	private float ChiJing=0;//赤经
	private float ChiWei=0;//赤纬
	//***********************************************

	
	private Vector3 HD_POS;//在黄道坐标系中摄像机方向向量
	private Vector3 HD_POS_Down;
	Gyroscope gyro;//陀螺仪传感器
	Quaternion quatMult;//四元数用于旋转
	Quaternion quatMap;
	public Transform fart;//整个天球引用
	void Start () {
		Vector3 h = Vector3.Cross (HD_POS_Down,Vector3.forward);
		fart.localRotation = Quaternion.AngleAxis (Vector3.Angle (HD_POS_Down, Vector3.forward), h);
	}
	void Awake()
	{
		PI = Mathf.PI;
		HD_POS_Down = TranForDirVec_Temp (147.00f, -56.00f);//天底经纬度（手动输入天底黄经黄纬）得到指向天底的向量
		gyro = Input.gyro;//开启传感器
		gyro.enabled = true;
		quatMult =new  Quaternion(0,0,1,0);
	}
	void Update()
	{
		quatMap = new Quaternion(gyro.attitude.x, gyro.attitude.y, gyro.attitude.z, gyro.attitude.w);//设置用于旋转的四元数
		//Quaternion qt =Quaternion.Euler(0,0,330)* quatMap * quatMult;
		Quaternion qt =quatMap * quatMult;
		transform.localRotation = qt;
	}
	Vector3 TranForDirVec_Temp(float N,float W)
	{	
		//获取黄道坐标系点坐标
		HD_POS.x =-(float)(Mathf.Cos (N/180*PI) * Mathf.Cos (W/180*PI));
		HD_POS.y =(float)(Mathf.Sin (N/180*PI)*Mathf.Cos (W/180*PI));
		HD_POS.z =(float) Mathf.Sin (W/180*PI);
		return HD_POS;
	}
}