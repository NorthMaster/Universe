using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/*
该脚本是数据加载的入口（即LoadDataFromTXT.Read()）;将点坐标从数组转换到三维向量;绘制点;
将点按星座顺序依次存入列表
*/
public class StarPOS_Vec_Array : MonoBehaviour {//画球

	public static Vector3[] starPos_Vector3;//使用三维向量存储空间星坐标
	public static GameObject[][] StarObj=new GameObject[LoadDataFromTXT.starsigns][];//存放实例化的点
	public static ArrayList ArrayList_Star;//将星坐标存入列表中
	public GameObject sphere;//点预制体（实质上是球）

	public TextAsset txt_S;//TXT文件（TextAsset是Unity内置的文本处理类，跨平台使用）存储星座及星信息
	public TextAsset txt_M;//TXT文件存储M星系数据

	public GameObject M_Sprite;//预制体——M星云
    public Sprite[] M_Image;//星云图片
    public static GameObject[] M_Obj=new GameObject[LoadDataFromTXT.M_Num];
	public Transform fart;
	void Awake()
	{
		//加载TXT文件中的每一行存入数组
		LoadDataFromTXT.DataFromTXT = txt_S.text.Split ('\n');
		LoadDataFromTXT.M_Row = txt_M.text.Split ('\n');
		LoadDataFromTXT.Read();//加载数据（从TXT文件中加载数据(点坐标和连线信息)）并转化到锯齿数组中

		int VextexNum = 0;//VextexNum/3，计算坐标数
		for (int i=0; i<LoadDataFromTXT.Star_Pos.Length; i++) {
			VextexNum+=LoadDataFromTXT.Star_Pos[i].Length;
		}
		//为锯齿数组实例化
		for(int i=0;i<LoadDataFromTXT.starsigns;i++)
		{
			StarObj[i]=new GameObject[LoadDataFromTXT.Star_Pos[i].Length/3];
		}
		//将float数组转换成Vector3数组,控制球位置
		starPos_Vector3=new Vector3[VextexNum/3];//实例化三维向量
		int m = 0;//（星）点坐标索引
		int n = 0;//每个星座的星索引
		int d = 0;//星等索引
		float Star_Scale = 0;//星大小缩放
		ArrayList_Star = new ArrayList ();//实例化列表，将每个星座的每颗星存入列表中
		for (int i=0; i<LoadDataFromTXT.Star_Pos.Length;i++) {
			n=0;
			d=0;
			for(int j=0;j<LoadDataFromTXT.Star_Pos[i].Length;){
				starPos_Vector3[m]=new Vector3(LoadDataFromTXT.Star_Pos[i][j],LoadDataFromTXT.Star_Pos[i][j+1],LoadDataFromTXT.Star_Pos[i][j+2]);
				GameObject go = (GameObject)Instantiate(sphere,starPos_Vector3[m],Quaternion.identity);
				//控制星大小(Scale)
				Star_Scale=(6.0f-float.Parse(LoadDataFromTXT.Star_Level_Name[i][d]))/10-0.09f;
				if(Star_Scale>0.15f)
				{
					Star_Scale=0.15f;
				}else if(Star_Scale>-0.05&&Star_Scale<0.05f)
				{
					Star_Scale=0.05f;
				}else if(Star_Scale<-0.15f)
				{
					Star_Scale=0.15f;
				}
				go.transform.localScale=new Vector3(Star_Scale,Star_Scale,Star_Scale);

				StarObj[i][n]=go;
				StarObj[i][n].transform.parent=fart.transform;
				ArrayList_Star.Add(starPos_Vector3[m]);
				j+=3;
				d+=2;
				m++;
				n++;
			}
		}
        int M_Image_Index = 0;
        int M_Obj_Index=0;
		//实例化M星系
		for(int i=0;i<LoadDataFromTXT.M_Pos.Length;)
		{
			Vector3 M_Pos=new Vector3(LoadDataFromTXT.M_Pos[i],LoadDataFromTXT.M_Pos[i+1],LoadDataFromTXT.M_Pos[i+2]);
			GameObject M=(GameObject)Instantiate(M_Sprite,M_Pos,Quaternion.identity);
			M.transform.forward=M_Pos;
			M.name=LoadDataFromTXT.M_Data[M_Obj_Index][0];
            M.transform.GetComponent<SpriteRenderer>().sprite = M_Image[M_Image_Index];
            if (M_Image_Index == 9)
            {
                M_Image_Index = 0;
            }
            else {
                M_Image_Index++;
            }
			i+=3;
            M_Obj[M_Obj_Index] = M;
            M_Obj_Index++;
		}
	}
}