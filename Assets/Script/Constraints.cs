using UnityEngine;
using System.Collections;

public class Constraints : MonoBehaviour {
    public static string NameStar = "";//普通太阳系场景中3D拾取到的物体名称
	public static string MS_Selected="PuTong";//主场景中模式选择标志区分（普通/AR/VR）
	public static string GOD_MANYOU = "MANYOU";//选择VR、AR模式中是上帝视角或者漫游
	public static string BackTemp = "";//记忆从某个场景返回后主菜单显示什么
	public static string YinXiao = "open";//是否开启音效
	public static float timeScale = 0.2f;//时间因子缩放比【默认是0.2s】
	public static float YG_CanShu = 5.0f;//设置摇杆灵敏度【默认是5.0】
	public static float[] N=new float[8];//八大行星黄经黄纬
	public static float[] W=new float[8];
	public static Vector3 YQ_POS;//月球位置
	public static string VR_Alignment = "开启";
}