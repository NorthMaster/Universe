using UnityEngine;
using System.Collections;
/*水星、金星、地球、火星、木星、土星、天王星、海王星*/
public class PanetsUpdatePos : MonoBehaviour {

	public Transform[] Planets_Trans;//每一行星的父对象
	public static float R=20.0f;//控制球体半径
	private GameObject[] Planets=new GameObject[8];//实例化八大行星后存储在数组里
	private Vector3[] PlanetsPOS=new Vector3[8];//八大行星的空间坐标位置
	private Plane[] planes;//存放摄像机视锥体六个面
	private string[] PlanetsName = {"水星","金星","地球","火星","木星","土星","天王星","海王星"};
	public GameObject[] Pre_Planets;//实例化八大行星所需要的预制体

	void Start () {
		Start_shuixing ();//实例化八大行星【计算其位置】
		Start_jinxing ();
		Start_diqiu ();
		Start_huoxing ();
		Start_muxing ();
		Start_tuxing ();
		Start_tianwangxing ();
		Start_haiwangxing ();
	}
	
	void Update () {
		/*
		 * 八大行星缩放
		 */
		if(Camera.main.GetComponent<Camera>().fieldOfView<=30.0f)
		{
			for(int i=0;i<8;i++)
			{
				Planets[i].transform.localScale=new Vector3 (1,1,1);
			}
			Planets[0].transform.localScale=new Vector3 (0.7f,0.7f,0.7f);
			Planets[6].transform.localScale=new Vector3 (1.3f,1.3f,1.3f);
			Planets[7].transform.localScale=new Vector3 (1.5f,1.5f,1.5f);
			YueQiu_NW.YQ_Index.transform.localScale=new Vector3 (1.3f,1.3f,1.3f);
		}else if(Camera.main.GetComponent<Camera>().fieldOfView>30.0f)
		{
			for(int i=0;i<8;i++)
			{
				Planets[i].transform.localScale=new Vector3 (0.5f,0.5f,0.5f);
			}
			YueQiu_NW.YQ_Index.transform.localScale=new Vector3 (1.0f,1.0f,1.0f);
		}
	}
	void OnGUI()//绘制行星名称
	{
		planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);//当摄像机转动时不断获得摄像机的视锥体六个面
		GUIStyle StarSign_Label=new GUIStyle();
		StarSign_Label.normal.background = null;//这是设置背景填充的
		StarSign_Label.normal.textColor=new Color(0.5f,0.7f,0.7f);//设置字体颜色的
		StarSign_Label.fontSize = 25;//设置字体大小
		for(int i=0;i<8;i++)
		{
			if (GeometryUtility.TestPlanesAABB(planes, Planets[i].GetComponent<SphereCollider>().bounds))
			{
				Vector3 Star_ScreenPos = Camera.main.WorldToScreenPoint(PlanetsPOS[i]);//从空间坐标转换到屏幕坐标
				Star_ScreenPos = new Vector2(Star_ScreenPos.x, Screen.height - Star_ScreenPos.y);//屏幕左下角为（0,0）
				//如果星座在摄像机视锥体内则绘制星座名称
				GUI.Label(new Rect(Star_ScreenPos.x+10.0f, Star_ScreenPos.y-10.0f, 100, 30), PlanetsName[i], StarSign_Label);
			}
		}
		Vector2 YQ_ScreenPos = new Vector2(Camera.main.WorldToScreenPoint(Constraints.YQ_POS).x, Screen.height 
		                             - Camera.main.WorldToScreenPoint(Constraints.YQ_POS).y);//屏幕左下角为（0,0）
		GUI.Label(new Rect(YQ_ScreenPos.x+10.0f, YQ_ScreenPos.y-10.0f, 100, 30), "月球", StarSign_Label);
	}
	/*
	 * 以下八个方法为实例化八大行星的方法
	 */
	void Start_shuixing ()
	{
		float x=-(float)(R*Mathf.Cos(Constraints.W[0])*Mathf.Cos(Constraints.N[0]));//计算获得XYZ
		float y=(float)(R*Mathf.Cos(Constraints.W[0])*Mathf.Sin(Constraints.N[0]));
		float z=(float)(R*Mathf.Sin(Constraints.W[0]));
		Vector3 pos = new Vector3 (x,y,z);
		PlanetsPOS [0] = pos;
		GameObject go = (GameObject)Instantiate(Pre_Planets[0],pos,Quaternion.identity);
		go.transform.parent = Planets_Trans[0].transform;
		Planets [0] = go;
	}
	void Start_jinxing ()
	{
		float x=-(float)(R*Mathf.Cos(Constraints.W[1])*Mathf.Cos(Constraints.N[1]));//计算获得XYZ
		float y=(float)(R*Mathf.Cos(Constraints.W[1])*Mathf.Sin(Constraints.N[1]));
		float z=(float)(R*Mathf.Sin(Constraints.W[1]));
		Vector3 pos = new Vector3 (x,y,z);
		PlanetsPOS [1] = pos;
		GameObject go = (GameObject)Instantiate(Pre_Planets[1],pos,Quaternion.identity);
		go.transform.parent = Planets_Trans[1].transform;
		Planets [1] = go;
	}
	void Start_diqiu ()
	{
		float x=-(float)(R*Mathf.Cos(Constraints.W[2])*Mathf.Cos(Constraints.N[2]));//计算获得XYZ
		float y=(float)(R*Mathf.Cos(Constraints.W[2])*Mathf.Sin(Constraints.N[2]));
		float z=(float)(R*Mathf.Sin(Constraints.W[2]));
		Vector3 pos = new Vector3 (x,y,z);
		PlanetsPOS [2] = pos;
		GameObject go = (GameObject)Instantiate(Pre_Planets[2],pos,Quaternion.identity);
		go.transform.parent = Planets_Trans[2].transform;
		Planets [2] = go;
	}
	void Start_huoxing ()
	{
		float x=-(float)(R*Mathf.Cos(Constraints.W[3])*Mathf.Cos(Constraints.N[3]));//计算获得XYZ
		float y=(float)(R*Mathf.Cos(Constraints.W[3])*Mathf.Sin(Constraints.N[3]));
		float z=(float)(R*Mathf.Sin(Constraints.W[3]));
		Vector3 pos = new Vector3 (x,y,z);
		PlanetsPOS [3] = pos;
		GameObject go = (GameObject)Instantiate(Pre_Planets[3],pos,Quaternion.identity);
		go.transform.parent = Planets_Trans[3].transform;
		Planets [3] = go;
	}
	void Start_muxing()
	{
		float x=-(float)(R*Mathf.Cos(Constraints.W[4])*Mathf.Cos(Constraints.N[4]));//计算获得XYZ
		float y=(float)(R*Mathf.Cos(Constraints.W[4])*Mathf.Sin(Constraints.N[4]));
		float z=(float)(R*Mathf.Sin(Constraints.W[4]));
		Vector3 pos = new Vector3 (x,y,z);
		PlanetsPOS [4] = pos;
		GameObject go = (GameObject)Instantiate(Pre_Planets[4],pos,Quaternion.identity);
		go.transform.parent = Planets_Trans[4].transform;
		Planets [4] = go;
	}
	void Start_tuxing ()
	{
		float x=-(float)(R*Mathf.Cos(Constraints.W[5])*Mathf.Cos(Constraints.N[5]));//计算获得XYZ
		float y=(float)(R*Mathf.Cos(Constraints.W[5])*Mathf.Sin(Constraints.N[5]));
		float z=(float)(R*Mathf.Sin(Constraints.W[5]));
		Vector3 pos = new Vector3 (x,y,z);
		PlanetsPOS [5] = pos;
		GameObject go = (GameObject)Instantiate(Pre_Planets[5],pos,Quaternion.identity);
		go.transform.parent = Planets_Trans[5].transform;
		Planets [5] = go;
	}
	void Start_tianwangxing ()
	{
		float x=-(float)(R*Mathf.Cos(Constraints.W[6])*Mathf.Cos(Constraints.N[6]));//计算获得XYZ
		float y=(float)(R*Mathf.Cos(Constraints.W[6])*Mathf.Sin(Constraints.N[6]));
		float z=(float)(R*Mathf.Sin(Constraints.W[6]));
		Vector3 pos = new Vector3 (x,y,z);
		PlanetsPOS [6] = pos;
		GameObject go = (GameObject)Instantiate(Pre_Planets[6],pos,Quaternion.identity);
		go.transform.parent = Planets_Trans[6].transform;
		Planets [6] = go;
	}
	void Start_haiwangxing ()
	{
		float x=-(float)(R*Mathf.Cos(Constraints.W[7])*Mathf.Cos(Constraints.N[7]));//计算获得XYZ
		float y=(float)(R*Mathf.Cos(Constraints.W[7])*Mathf.Sin(Constraints.N[7]));
		float z=(float)(R*Mathf.Sin(Constraints.W[7]));
		Vector3 pos = new Vector3 (x,y,z);
		PlanetsPOS [7] = pos;
		GameObject go = (GameObject)Instantiate(Pre_Planets[7],pos,Quaternion.identity);
		go.transform.parent = Planets_Trans[7].transform;
		Planets [7] = go;
	}
}
