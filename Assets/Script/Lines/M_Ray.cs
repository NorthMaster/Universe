using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class M_Ray : MonoBehaviour {
	public Text M_Text;//显示M星系详细信息
	private string M_Name;
	private string M_Level;
	private string M_Light;
	private string M_ChiJing;
	private string M_ChiWei;
	private string M_HuangJing;
	private string M_HuangWei;
	private string[] M_Temp=new string[4];//M星系拆分临时变量数组

	public Button BToMStar;//切换到M星系列表场景按钮

	//鼠标需用的拾取变量
//	private RaycastHit _rayhit;
//	private Ray _ray;
//	private float _fDistance = 20f;

	void Start () {
	
	}

	void Update () {
		//注释部分为鼠标拾取代码
//		//检测鼠标左键的拾取
//		if (Input.GetMouseButtonDown(0)) {
//			//鼠标的屏幕坐标空间位置转射线
//			_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//			//射线检测，相关检测信息保存到RaycastHit 结构中
//			if (Physics.Raycast(_ray, out _rayhit, _fDistance)) {
//				//打印射线碰撞到的对象的名称
//				Debug.Log(""+_rayhit.collider.gameObject.name);
//			}
//		}

		if (Input.touchCount == 1) //3D拾取M星系群
		{
			Touch t = Input.GetTouch (0);
			if (t.phase == TouchPhase.Began) 
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
				RaycastHit hitInfo;
				if (Physics.Raycast (ray, out hitInfo)) {
					GameObject gameObj = hitInfo.collider.gameObject;

					for(int i=0;i<LoadDataFromTXT.M_Num;i++)
					{
						if(gameObj.transform.name==LoadDataFromTXT.M_Data[i][0])//判断拾取的是哪一个M星系
						{
							//拆分组装文本显示字符串
							M_Name=LoadDataFromTXT.M_Data[i][0];
							M_Level=LoadDataFromTXT.M_Data[i][1];
							M_Light=LoadDataFromTXT.M_Data[i][2];
							M_Temp=LoadDataFromTXT.M_Data[i][3].Split('.');
							M_ChiJing=M_Temp[0]+"h"+M_Temp[1]+"m"+M_Temp[2]+"."+M_Temp[3]+"s";
							M_Temp=LoadDataFromTXT.M_Data[i][4].Split('.');
							M_ChiWei=M_Temp[0]+"°"+M_Temp[1]+"′"+M_Temp[2]+"."+M_Temp[3]+"″";
							M_Temp=LoadDataFromTXT.M_Data[i][5].Split('.');
							M_HuangJing=M_Temp[0]+"°"+M_Temp[1]+"′"+M_Temp[2]+"."+M_Temp[3]+"″";
							M_Temp=LoadDataFromTXT.M_Data[i][6].Split('.');
							M_HuangWei=M_Temp[0]+"°"+M_Temp[1]+"′"+M_Temp[2]+"."+M_Temp[3]+"″";
							M_Text.text="名称："+M_Name+"\n星等："+M_Level+"\n表面亮度："+M_Light+"\n赤经："+M_ChiJing+"\n赤纬："+M_ChiWei+"\n黄经："+M_HuangJing+"\n黄纬："+M_HuangWei;
							BToMStar.gameObject.SetActive(true);
							StartCoroutine(M_Text_XSTime());//启动协程使文字显示一段时间后消失
						}
					}
				}
			}
		}
	}
	IEnumerator M_Text_XSTime()//文字显示时间协程
	{
		yield return new WaitForSeconds(5.0f);
		M_Text.text = "";
		BToMStar.gameObject.SetActive(false);
	}
}