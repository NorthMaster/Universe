using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/*
该脚本使用预制体——线段（即圆柱体）按实例化好的预制体——点（即球）连接起来，连接数据信息存放
在LoadDataFromTXT脚本中的Star_Path锯齿数组中，特别需要注意的是“在每个星座画完之后需要将该星座
的所有点（即球）引用在列表中删除（即StarPOS_Vec_Array脚本中的ArrayList_Star列表）”[这一点特别重要]
*/
public class LineTool : MonoBehaviour {//画线
	public GameObject line;//线预制体（实质上是圆柱代替线）
	public Transform fart;
	void Start()
	{
		//遍历（星与星连线）锯齿二维数组
		for(int i=0;i<LoadDataFromTXT.Star_Path.Length;i++)
		{
			for(int j=0;j<LoadDataFromTXT.Star_Path[i].Length;)
			{
				//画线
				showLine(LoadDataFromTXT.Star_Path[i][j],LoadDataFromTXT.Star_Path[i][j+1]);
				j+=2;
			}
			//当一个星座的连线画完之后，将该星座的所有点从列表中删除
			StarPOS_Vec_Array.ArrayList_Star.RemoveRange(0,LoadDataFromTXT.Star_Pos[i].Length/3);
		}
	}
	void showLine(int i,int j)//画线方法
	{
		Vector3 star_a = (Vector3)StarPOS_Vec_Array.ArrayList_Star[i];//两个点的坐标，用于连线
		Vector3 star_b = (Vector3)StarPOS_Vec_Array.ArrayList_Star[j];

		Vector3 tempPos=(star_a+star_b)/2;//计算两个点的中点坐标，
		GameObject go=(GameObject)Instantiate(line,tempPos,Quaternion.identity) ;//在两个点的中点处实例化线条，因为对物体的缩放，是从中心向两边延伸

		go.transform.right=(go.transform.position-star_a).normalized;//改变线条的朝向
		float distance=Vector3.Distance(star_a,star_b);//计算两点的距离
		go.transform.localScale=new Vector3(distance,0.01f,0.01f);//延长线条,连接两点
		go.transform.parent = fart.transform;
	}
}