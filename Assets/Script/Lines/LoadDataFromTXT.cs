using UnityEngine;
using System.IO;
using System;
using System.Collections;
/*
该脚本是各种数据处理的总类，包括经纬度转换成空间坐标、星座中心点计算、数据字符串拆分并转换到锯齿数组
*/
public class LoadDataFromTXT : MonoBehaviour 
{
	public static int starsigns=86;//星座个数
	public static int M_Num=19;//M星系个数
	public static string[] DataFromTXT=new string[1792];//存放TXT每一行的字符串数组【将数据文件中每一行数据以数组的形式存起来】

	public static string[] StarSign_Name=new string[starsigns];//星座名称
	public static float[][] Star_Pos=new float[starsigns][];//星坐标数组*******重要数组（点坐标,绝对正确）******
	public static int[][] Star_Path=new int[starsigns][];//星与星连接顺序
	public static float[][] Star_NW=new float[starsigns][];//星经纬度
	public static string[][] Star_Level_Name=new string[starsigns][];//每颗星座每颗星的等级和名称
	public static float[][] StarName_Pos=new float[starsigns][];//每个星座每颗星的名称的位置坐标（XY）
	public static float[] StarSignName_Pos=new float[starsigns*3];//星座名称文本的位置

	public static string[] M_Row=new string[M_Num];//存放M星系TXT文件中的每一行数据
	public static string[][] M_Data=new string[M_Num][];//二维数组存放每个M星系数据
	public static float[] M_Pos=new float[M_Num*3];//存放星系坐标值

	public static float R=20.0f;//控制球体半径
    public static float M_R = 40.0f;//深空天体（M星云）半径
	public static float PI=3.14f;//圆周率

	void Awake()
	{
		//Read ();//调试用
	}

	//将TXT文件中导出的数据进行计算
	public static void Read(){
		//注释部分适用于在Windows平台读取数据
//		//把txt文件放在StreamingAssetsPath文件夹中（没有的话手动建一个）
//		var fileAddress = System.IO.Path.Combine(Application.streamingAssetsPath, "shuju.txt");
//		string s = "";
//		StreamReader r = new StreamReader(fileAddress);
//		s = r.ReadLine ();
//		int i = 0;
//		while(s != null)
//		{
//			DataFromTXT[i]=s;
//			s = r.ReadLine(); 
//			i++;
//		}
		LoadData_NW (DataFromTXT);//在TXT文件中加载经纬度
		TransNWToVec ();//将经纬度转换成坐标
		LoadData_Path (DataFromTXT);//加载连线到锯齿数组中
		M_Trans ();//关于M星系数据的一些转换方法
//		for (int i=0; i<Star_Pos.Length; i++) 
//		{
//			for(int j=0;j<Star_Pos[i].Length;)
//			{
//				Debug.Log("X:"+Star_Pos[i][j]+"Y:"+Star_Pos[i][j+1]+"Z:"+Star_Pos[i][j+1]);
//				j+=3;
//			}
//		}
	}
	static void TransNWToVec()//将经纬度转换成空间坐标
	{
		int Star_Index = 0;//每个星座的每颗星的坐标索引
		int StarSign_POS_Index = 0;
		float Average_X=0;
		float Average_Y=0;
		float Average_Z=0;
		float SUM_X=0;
		float SUM_Y=0;
		float SUM_Z=0;

		for(int i=0;i<Star_NW.Length;i++)
		{
			Star_Pos[i]=new float[Star_NW[i].Length/2*3];//实例化锯齿数组
			for(int j=0;j<Star_NW[i].Length;)
			{
				double n=(double)Star_NW[i][j]/180*PI;//转换经纬度
				double w=(double)Star_NW[i][j+1]/180*PI;

				float x=(float)(R*Math.Cos(w)*Math.Cos(n));//计算获得XYZ
				float y=(float)(R*Math.Cos(w)*Math.Sin(n));
				float z=(float)(R*Math.Sin(w));

				Star_Pos[i][Star_Index]=-x;//将每个点的XYZ存入锯齿数组
				Star_Pos[i][Star_Index+1]=y;
				Star_Pos[i][Star_Index+2]=z;

				j+=2;
				Star_Index+=3;
			}
			Star_Index=0;
		}
		//*****************计算星座名称文本位置*********************
		for (int i=0; i<Star_Pos.Length; i++) {	

			SUM_X=0;
			SUM_Y=0;
			SUM_Z=0;
			for(int j=0;j<Star_Pos[i].Length;)
			{
				//每个星座所有星X坐标和Y坐标总和,计算XY坐标平均值
				SUM_X+=Star_Pos[i][j];
				SUM_Y+=Star_Pos[i][j+1];
				SUM_Z+=Star_Pos[i][j+2];
				j+=3;
			}
			
			Average_X=SUM_X/(Star_Pos[i].Length/3);
			Average_Y=SUM_Y/(Star_Pos[i].Length/3);
			Average_Z=SUM_Z/(Star_Pos[i].Length/3);

			StarSignName_Pos[StarSign_POS_Index]=Average_X;
			StarSignName_Pos[StarSign_POS_Index+1]=Average_Y;
			StarSignName_Pos[StarSign_POS_Index+2]=Average_Z;

			StarSign_POS_Index+=3;
		}
	}
	static void LoadData_NW(String[] DataFromTXT)//在TXT文件中加载经纬度到锯齿数组
	{
		int XingZuoIndex = 0;//星座索引
		int NW=0;//每个星座经纬度索引
		int Level_Name = 0;//星等级和名称索引
		for(int i=0;i<DataFromTXT.Length;)//循环存储数据文件中每一行数据的数组【TXT数据文件中有多少行数组就有多大】
		{
			string[] strs=DataFromTXT[i].Split('#');//判断注释行
			if(strs[0]=="")
			{
				StarSign_Name[XingZuoIndex]=DataFromTXT[i+2];//获得星座名称
				//Debug.Log("***"+StarSign_Name[XingZuoIndex]);//打印星座名称
				int StarNUM=int.Parse(DataFromTXT [i+1]);//该星座的星数
				string[] str_EachRow;//用于存放每颗星经纬度（存放分割行字符串的经纬度）
				string[] str_dot;
				Star_NW[XingZuoIndex]=new float[StarNUM*2];//构建存放每个星座星经纬度的锯齿数组
				Star_Level_Name[XingZuoIndex]=new string[StarNUM*2] ;
				for(int j=0;j<StarNUM;j++)//循环该星座的经纬度部分,将经纬度存放入锯齿数组
				{
					//把每行按空格分割成数组
					str_EachRow=DataFromTXT[i+3+j].Split(' ');
					//获得每个星座的每颗星的等级和名称
					for(int m=2;m<4;m++)
					{
						Star_Level_Name[XingZuoIndex][Level_Name]=str_EachRow[m];
						//Debug.Log("**"+Star_Level_Name[XingZuoIndex][Level_Name]);//打印星等级和名称
						Level_Name++;
					}
					//循环拆分的数组依次放入锯齿数组
					for(int n=0;n<2;n++)
					{
						str_dot=str_EachRow[n].Split('.');//将TXT文件中的经纬度格式转换（"度.分"——>"度+（分/60）"）
						if(str_dot.Length==2)
						{
							str_EachRow[n]=(float.Parse(str_dot[1])/60+float.Parse(str_dot[0])).ToString();
						}
						Star_NW[XingZuoIndex][NW]=float.Parse(str_EachRow[n]);

						NW++;
					}
				}
				XingZuoIndex++;
				NW=0;
				Level_Name=0;
			}
			//设置外层循环
			if(strs[0]=="")
			{
				i+=int.Parse(DataFromTXT[i+1])+5+int.Parse(DataFromTXT[i+3+int.Parse(DataFromTXT[i+1])]);
			}else{
				i++;
			}
		}
	}
	static void LoadData_Path(String[] DataFromTXT)//从TXT文件中加载星连线到锯齿数组中
	{
		int XingZuo_Index = 0;//星座索引
		int Path_Index = 0;//每一星座中连线信息索引
		for(int i=0;i<DataFromTXT.Length;)//循环TXT导出的行数组
		{
			string[] strs=DataFromTXT[i].Split('#');//判断注释行
			if(strs[0]=="")
			{
				int StarPathNUM=int.Parse(DataFromTXT[int.Parse(DataFromTXT [i+1])+3+i]);//获得该星座线段数
				string[] strPath_EachRow;//声明该星座每一根（即TXT中每一行）连线临时存放数组
				Star_Path[XingZuo_Index]=new int[StarPathNUM*2];//构建存放星座连线的锯齿形数组
				for(int j=0;j<StarPathNUM;j++)//循环该星座中连线部分，将连线信息存入数组
				{
					//按空格拆分成数组
					strPath_EachRow= DataFromTXT[int.Parse(DataFromTXT [i+1])+4+i+j].Split(' ');
					//循环拆分的数组依次存放入锯齿数组
					for(int n=0;n<2;n++)
					{
						Star_Path[XingZuo_Index][Path_Index]=int.Parse(strPath_EachRow[n]);
						Path_Index++;
					}
				}
				XingZuo_Index++;
				Path_Index=0;
			}
			//判断外层总循环起点
			if(strs[0]=="")
			{
				i+=int.Parse(DataFromTXT[i+1])+5+int.Parse(DataFromTXT[i+3+int.Parse(DataFromTXT[i+1])]);
			}else{
				i++;
			}
		}
	}
	static void M_Trans()//将M星系数据拆分
	{
		string[] M_N;//存放拆分之后的经度
		string[] M_W;//存放拆分之后的纬度
		int M_Index = 0;//M星云索引
		for (int i=0; i<M_Num; i++) 
		{//实例化锯齿数组存放每一M星系（TXT文件中一行）拆分的数据
			M_Data[i]=new string[7];
		}
		for (int i=0; i<M_Row.Length; i++) 
		{
			M_Data[i]=M_Row[i].Split(' ');//按空格拆分每一行
		}
		for(int i=0;i<M_Row.Length;i++)
		{
			M_N=M_Data[i][5].Split('.');//按"."拆分经纬度
			M_W=M_Data[i][6].Split('.');
			float N=float.Parse(M_N[0])+float.Parse(M_N[1])/100;
			float W=float.Parse(M_W[0])+float.Parse(M_W[1])/100;
			double n=(double)N/180*PI;//转换经纬度
			double w=(double)W/180*PI;
            float x = (float)(M_R * Math.Cos(w) * Math.Cos(n));//计算获得XYZ
            float y = (float)(M_R * Math.Cos(w) * Math.Sin(n));
            float z = (float)(M_R * Math.Sin(w));
			M_Pos[M_Index]=x;//将三维坐标存放入float数组中
			M_Pos[M_Index+1]=y;
			M_Pos[M_Index+2]=z;
			M_Index+=3;
		}
	}
}