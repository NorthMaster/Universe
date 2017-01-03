using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
/*
该脚本主要用于绘制星座名称到屏幕上
*/
public class StarSign_Star_Name : MonoBehaviour {

	private Vector3 StarSignCenter_WorldPos;//临时变量（星座名称世界坐标位置）
	private Vector2[] StarSignCenter_ScreenPos;//存储星座名称屏幕坐标数组
	private Vector2 Temp;//临时变量（星座名称屏幕坐标位置）
	private Vector2 Star_ScreenPos;
	private int StarSign_Index=0;//星座索引

	public GameObject anObject;//预制体引用
	private GameObject[] CenterObj=new GameObject[LoadDataFromTXT.starsigns];//每个星座中心位置实例化空物体判断该点是否出现在视锥体内来代表该星座是否绘制名称
	private Plane[] planes;//存放摄像机视锥体六个面
	private Plane[] planesGUI;//存放摄像机视锥体六个面
	private bool[] IsOnCarema=new bool[LoadDataFromTXT.starsigns];//判断星座是否出现在视锥体内部.是：true，否：false
	
	void Start()
	{
		StarSignCenter_ScreenPos=new Vector2[LoadDataFromTXT.starsigns];//实例化星座名称屏幕坐标数组
		for(int i=0;i<LoadDataFromTXT.StarSignName_Pos.Length;)
		{
			StarSignCenter_WorldPos=new Vector3(LoadDataFromTXT.StarSignName_Pos[i],
			                        LoadDataFromTXT.StarSignName_Pos[i+1],LoadDataFromTXT.StarSignName_Pos[i+2]);
			//实例化中心点，去判断哪一个星座出现在摄像机视镜体内，以便绘制星座名称
			GameObject go =(GameObject)Instantiate(anObject,StarSignCenter_WorldPos,Quaternion.identity);
			CenterObj[StarSign_Index]=go;//存入数组

			Temp=Camera.main.WorldToScreenPoint(StarSignCenter_WorldPos);//从空间坐标转换到屏幕坐标
			StarSignCenter_ScreenPos[StarSign_Index] = new Vector2 (Temp.x, Screen.height - Temp.y);//屏幕左下角为（0,0）
			i+=3;
			StarSign_Index++;
		}
	}

	void Update()
	{
		planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);//当摄像机转动时不断获得摄像机的视锥体六个面
		StarSign_Index = 0;//每一帧的星座索引置0
		for(int i=0;i<LoadDataFromTXT.StarSignName_Pos.Length;)
		{
			StarSignCenter_WorldPos=new Vector3(LoadDataFromTXT.StarSignName_Pos[i],
				LoadDataFromTXT.StarSignName_Pos[i+1],LoadDataFromTXT.StarSignName_Pos[i+2]);
			Temp=Camera.main.WorldToScreenPoint(StarSignCenter_WorldPos);	
			StarSignCenter_ScreenPos[StarSign_Index] = new Vector2 (Temp.x, Screen.height - Temp.y);
			//判断物体是否存在摄像机视锥体内
			if (GeometryUtility.TestPlanesAABB(planes,CenterObj[StarSign_Index].GetComponent<Collider>().bounds))
				IsOnCarema[StarSign_Index]=true;//在视锥体内
			else
				IsOnCarema[StarSign_Index]=false;//在视锥体外

			i+=3;
			StarSign_Index++;
		}
	}
	
	void OnGUI()
	{
		GUIStyle StarSign_Label=new GUIStyle();
		StarSign_Label.normal.background = null;//这是设置背景填充的
		StarSign_Label.normal.textColor=new Color(0.21f,0.32f,0.47f);//设置字体颜色的
		StarSign_Label.fontSize = 30;//设置字体大小
		//绘制屏幕中星座名称（Label）
		for(int i=0;i<StarSignCenter_ScreenPos.Length;i++)
		{
			if (IsOnCarema[i]==true)
			{
				//如果星座在摄像机视锥体内则绘制星座名称
				GUI.Label (new Rect(StarSignCenter_ScreenPos[i].x,StarSignCenter_ScreenPos[i].y,100,30),
				           LoadDataFromTXT.StarSign_Name[i],StarSign_Label);
			}
		}
		float x = 0;
		float y = 0;
		float z = 0;
		StarSign_Label.normal.textColor=new Color(0.8f,0.7f,1);//设置字体颜色的
		StarSign_Label.fontSize = 20;//设置字体大小
		planesGUI = GeometryUtility.CalculateFrustumPlanes(Camera.main);//当摄像机转动时不断获得摄像机的视锥体六个面
		//绘制屏幕中比较重要的星星的名称
		for (int i=0; i<StarPOS_Vec_Array.StarObj.Length; i++) {
			for(int j=0;j<StarPOS_Vec_Array.StarObj[i].Length;j++)
			{
				if (GeometryUtility.TestPlanesAABB(planesGUI,StarPOS_Vec_Array.StarObj[i][j].GetComponent<SphereCollider>().bounds))
				{
					if (Regex.IsMatch (LoadDataFromTXT.Star_Level_Name[i][j], "^[\u4e00-\u9fa5]+$")) {
						x = StarPOS_Vec_Array.StarObj[i][j].transform.position.x;
						y = StarPOS_Vec_Array.StarObj[i][j].transform.position.y;
						z = StarPOS_Vec_Array.StarObj[i][j].transform.position.z;
						Star_ScreenPos = Camera.main.WorldToScreenPoint(new Vector3(x,y,z));//从空间坐标转换到屏幕坐标
						Star_ScreenPos = new Vector2 (Star_ScreenPos.x, Screen.height - Star_ScreenPos.y);//屏幕左下角为（0,0）
						//如果星座在摄像机视锥体内则绘制星座名称
						GUI.Label (new Rect(Star_ScreenPos.x+1.0f,Star_ScreenPos.y+1.0f,100,30),LoadDataFromTXT.Star_Level_Name[i][j*2-1],StarSign_Label);
					}
				}
			}
		}
        StarSign_Label.normal.textColor = new Color(0.5f, 0.8f, 0.9f);//设置字体颜色的
        StarSign_Label.fontSize = 20;//设置字体大小
        //绘制屏幕中M星云的名称
        for (int i = 0; i < StarPOS_Vec_Array.M_Obj.Length;i++)
        {
            if (GeometryUtility.TestPlanesAABB(planesGUI, StarPOS_Vec_Array.M_Obj[i].GetComponent<BoxCollider>().bounds))
            {
                x = LoadDataFromTXT.M_Pos[i * 3];
                y = LoadDataFromTXT.M_Pos[i * 3 + 1];
                z = LoadDataFromTXT.M_Pos[i * 3 + 2];
                Star_ScreenPos = Camera.main.WorldToScreenPoint(new Vector3(x, y, z));//从空间坐标转换到屏幕坐标
                Star_ScreenPos = new Vector2(Star_ScreenPos.x, Screen.height - Star_ScreenPos.y);//屏幕左下角为（0,0）
                //如果星座在摄像机视锥体内则绘制星座名称
                GUI.Label(new Rect(Star_ScreenPos.x+10.0f, Star_ScreenPos.y-10.0f, 100, 30), LoadDataFromTXT.M_Data[i][0], StarSign_Label);
            }
        }
	}
}