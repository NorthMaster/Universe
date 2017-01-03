using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
该脚本实现从地平坐标系转换到赤道坐标系在转换到黄道坐标系【存在问题】
 */
public class CoordinatesTransform : MonoBehaviour {

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
	//***********************************************
	private float sinHuangWei=0;//黄纬正弦值
	private float cosHuangJingcosHuangWei=0;
	private float sinHuangJingcosHuangWei=0;

	private Vector3 HD_POS;//在黄道坐标系中摄像机方向向量

	void Start () {
		PI = Mathf.PI;
	}

	void Update () {
		//从地平坐标系转换到赤道坐标系
		sinδ = Mathf.Sin (φ / 180 * PI) * Mathf.Sin (α / 180 * PI)
			+ Mathf.Cos (φ / 180 * PI) * Mathf.Cos (α / 180 * PI) * Mathf.Cos (A / 180 * PI);
		cosδcosH = Mathf.Cos (φ / 180 * PI) * Mathf.Sin (α / 180 * PI)
			- Mathf.Sin (φ / 180 * PI) * Mathf.Cos (α / 180 * PI) * Mathf.Cos (A / 180 * PI);
		cosδsinH = -Mathf.Sin (A / 180 * PI) * Mathf.Cos (α / 180 * PI);
		
		//计算整理需要的变量值
		cosδ = Mathf.Sqrt(1 - sinδ * sinδ);//可以直接开根号
		cosH = cosδcosH / cosδ;
		sinH = cosδsinH / cosδ;
		
		H = Mathf.Atan2 (sinH, cosH);//返回一个值【时角】范围-pi~+pi，应该是0~2PI
		
		ChiJing =9.30f - H;//求赤经(这里有一个值【地方恒星时】需要从网页中读取)
		//赤道坐标转到黄道坐标
		sinHuangWei = Mathf.Cos (HCJJ / 180 * PI) * sinδ 
			- Mathf.Sin(ChiJing) * cosδ * Mathf.Sin (HCJJ / 180 * PI);
		cosHuangJingcosHuangWei = Mathf.Cos(ChiJing) * cosδ;
		sinHuangJingcosHuangWei = Mathf.Sin (HCJJ / 180 * PI) * sinδ 
			+ Mathf.Sin(ChiJing) * cosδ * Mathf.Cos (HCJJ / 180 * PI);
		//获取黄道坐标系点坐标
		HD_POS.x = cosHuangJingcosHuangWei;
		HD_POS.y = sinHuangJingcosHuangWei;
		HD_POS.z = sinHuangWei;
		
		Camera.main.transform.LookAt (HD_POS);
	}
}