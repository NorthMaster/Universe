using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
 * M星系列表以及点击列表查看详细信息脚本，此脚本是活的，若添加M星系在MContent.txt中添加文本信息，
 * 在该脚本中将图片拖拉至引用，修改M_All、M_Cont的存储空间大小即可
 */
public class M_Select : MonoBehaviour {
	private GameObject[] M_All=new GameObject[19];//列表
	private string[] M_Cont=new string[19];//存放Txt每一行数据
	public Text M_Text;//M选中星系信息
	public Image M_Image;//M选中星系图片

	public TextAsset txt_MContent;//关于M星系的信息
	public Sprite[] M_Sprite;//每个星系的精灵图片数组
	public GameObject[] M_Label;//给列表中的M星团名称赋值
	public AudioClip Sound;//声音资源

	public GameObject M_Prefab;//M星系左侧列表中每一项的预制体【为了写活】
	public Transform M_Fart;//列表每一项的父级
	public ToggleGroup M_Group;//Toggle组
	void Start () {
	
	}
	void Awake()
	{
		M_Cont = txt_MContent.text.Split ('\n');
		//实例化M星系列表
		for(int i=0;i<M_Cont.Length;i++)
		{ 
			GameObject M_Name=(GameObject)Instantiate(M_Prefab,new Vector3(0,0,0),Quaternion.identity);
			M_Name.transform.parent=M_Fart;
			M_Name.transform.GetComponentInChildren<Text>().text=M_Cont[i].Split('*')[0];
			M_Name.transform.GetComponentInParent<Toggle>().group=M_Group;
			M_Name.transform.name=M_Cont[i].Split('*')[0];
			M_All[i]=M_Name;
		}
		M_All [0].GetComponent<Toggle> ().isOn = true;
	}
	void Update () {
		CheakSelect_M ();
	}
	public void BackToXingZuo()
	{
		if(Constraints.YinXiao=="open")//添加音效
		{
			AudioSource.PlayClipAtPoint(Sound,new Vector3(0,0,0));//设置播放片段的位置，离摄像机越近越清晰
		}
		Application.LoadLevelAsync ("XingZuo");
	}
	void CheakSelect_M()//判断选中M星云
	{
		string[] M_Temp;
		for(int i=0;i<M_All.Length;i++)
		{
			if(M_All[i].GetComponent<Toggle>().isOn==true)
			{
				M_Temp=M_Cont[i].Split('*');
				M_Text.text="名称："+M_Temp[0]+"\n"+"简介："+M_Temp[1];
				M_Image.sprite=M_Sprite[i];
			}
		}
	}

}
