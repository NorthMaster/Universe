using UnityEngine;
using System.Collections;

public class Planets_N : MonoBehaviour {
	public TextAsset DataTXT_N;//地球黄经计算数据文件
	public static string[] Data_Row_N;//地球黄经计算数据文件按行存储在数组中 
	public static string[,] Data_Row_Split;//按行存储之后再把每一行按空格拆分
	public static float[] Data_List_SUM;
	public static float dt;//时间【计算得出】
	void Awake()
	{
		Data_Split_Save ();//数据动态存储和拆分
		Method_Time ();//计算儒略日时间
		Process_SUM ();//经纬度总体计算中间准备阶段
		Method_SUM(Data_List_SUM,dt);//计算经纬度
	}
	void Update()
	{
//		Method_Time ();//计算儒略日时间
//		Process_SUM ();//经纬度总体计算中间准备阶段
//		Method_SUM(Data_List_SUM,dt);//计算经纬度
	}
	void Process_SUM()
	{
		int Index_L = 0;
		for(int i=0;i<Data_Row_N.Length;i++)//统计数据列表中有几个数据项
		{
			if(Data_Row_Split[i,0]=="Ver=4")
			{
				Index_L+=1;
			}
		}
		Data_List_SUM=new float[Index_L];
		int[] Index_Item=new int[Index_L+1];
		int temp = 0;
		for(int i=0;i<Data_Row_N.Length;i++)//统计数据列表中有几个数据项
		{
			if(Data_Row_Split[i,0]=="Ver=4")
			{
				Index_Item[temp]=i;
				temp++;
			}
		}
		Index_Item [temp] = Data_Row_N.Length;//最后一行的下一行索引
		float SUM_Item = 0;
		for(int j=0;j<Index_L;j++)
		{
			SUM_Item=0;
			for(int k=Index_Item[j]+1;k<Index_Item[j+1];k++)
			{
				SUM_Item+=Method_Math(Data_Row_Split[k,0],Data_Row_Split[k,1],Data_Row_Split[k,2],dt);
			}
			Data_List_SUM[j]=SUM_Item;
		}
	}
	void Data_Split_Save()//数据动态存储和拆分
	{
		Data_Row_N = new string[DataTXT_N.text.Split('\n').Length];//分配存储空间
		Data_Row_N = DataTXT_N.text.Split ('\n');//将TXT文件按行存储在数组中
		Data_Row_Split=new string[DataTXT_N.text.Split('\n').Length,3];//按行存储之后再把每一行按空格拆分
		for(int i=0;i<DataTXT_N.text.Split('\n').Length;i++)
		{
			Data_Row_Split[i,0]=Data_Row_N[i].Split(' ')[0];
			Data_Row_Split[i,1]=Data_Row_N[i].Split(' ')[1];
			Data_Row_Split[i,2]=Data_Row_N[i].Split(' ')[Data_Row_N[i].Split(' ').Length-1];
		}
	}
	float Method_Math(string A,string B,string C,float dt)
	{
		//A*cos(B+C*τ)计算公式
		float[] ABC=new float[3];//将字符串转换成数据类型
		ABC [0] = float.Parse (A);
		ABC [1] = float.Parse (B);
		ABC [1] = float.Parse (C);
		float Result = ABC [0] * Mathf.Cos (ABC [1] + ABC [2] * dt);
		return Result;
	}
	void Method_SUM(float[] Data_List_SUM,float dt)//得到经纬度（弧度制）
	{
		float SUM = 0;
		for(int i=0;i<Data_List_SUM.Length;i++)
		{
			SUM+=Data_List_SUM[i]*Mathf.Pow(dt,i);//加完之后的这个值就是经纬度（弧度制）
		}
		int Index =int.Parse(transform.name.Split ('_') [1]);
		Constraints.N[Index] = SUM;
	}
	void Method_Time()//时间计算方法
	{
		/*可能有些读者对儒略日数不太了解，造成无法计算出τ。那可按如下方法计算τ：
		 * 对于某时刻，先算出该时刻距2000年1月1日12：00：00的偏离日数D，那么τ=D/365250
		 */
		float[] OriginalTime = {2000,01,01,12,00,00};
		string NowTime=System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
		string[] NowTime_Split = NowTime.Split ('-');
		float[] Temp=new float[6];
		for(int i=0;i<6;i++)
		{
			Temp[i]=float.Parse(NowTime_Split[i])-OriginalTime[i];
		}
		dt = (Temp [0] * 365 + Temp [1] * 30 + Temp [2] + Temp [3] / 24 + 
		      Temp [4] / (60 * 24) + Temp [5] / (60 * 60 * 24)) / 365250;
	}
}












