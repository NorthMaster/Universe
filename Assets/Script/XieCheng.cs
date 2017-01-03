using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class XieCheng : MonoBehaviour
{
	public Image ImageBG;
	public Sprite[] Index_MainMenu;
	public Image[] Index_Click;
	public Image[] ok;//太阳系三种模式（普通、VR、AR）
	public Image moshishezhi;
	public Image shezhi;
	public int Index=5;
    public Text GPS_info;//GPS信息文本Text引用
	public Toggle[] toggle;
	public Image[] scro;
    public GameObject scroll;
	public Toggle[] Toggle_MS;//上帝视角、漫游模式
	public GameObject Toggle_GM;//Toggle_MS的父对象
	public AudioClip Sound;//声音资源
	public Slider[] slider_YG_Time;//设置摇杆参数和时间因子缩放的两个滑杆引用

	public void ExitOnClick()
	{
    	scroll.SetActive(false);
	}
	public void toggle_LY_YES()//控制VR眼镜Alignment Maker开启或者关闭
	{
		Constraints.VR_Alignment = "开启";
	}
	public void toggle_LY_NO()
	{
		Constraints.VR_Alignment = "关闭";
	}
	/*此处是协程启用GPS和关闭GPS的代码【此功能已删去】*/
  public void toggle_GPS_YES() {
    if (toggle[4].isOn == true) {
      toggle[4].isOn = true;
      toggle[5].isOn = false;
      // 这里需要启动一个协同程序  
      StartCoroutine(StartGPS());
    }
    else {
      toggle[4].isOn = false;
      toggle[5].isOn = true;
      StopGPS();
      GPS_info.text = "未连接";
    }
  }
  public void toggle_GPS_NO() {
    if (toggle[5].isOn == true) {
      toggle[5].isOn = true;
      toggle[4].isOn = false;
      StopGPS();
      GPS_info.text = "未连接";
    }
    else {
      toggle[5].isOn = false;
      toggle[4].isOn = true;
      // 这里需要启动一个协同程序  
      StartCoroutine(StartGPS());
    }
  }
	public void toggle_MS_GOD()//VR/AR选择上帝视角或者漫游
	{
		if (Toggle_MS[0].isOn == true) {
			Toggle_MS[0].isOn = true;
			Toggle_MS[1].isOn = false;
			Constraints.GOD_MANYOU="GOD";
		} else {
			Toggle_MS[0].isOn=false;
			Toggle_MS[1].isOn=true;
			Constraints.GOD_MANYOU="MANYOU";
		}
	}
	public void toggle_MS_MANYOU()
	{
		if (Toggle_MS[1].isOn == true) {
			Toggle_MS[1].isOn = true;
			Toggle_MS[0].isOn = false;
			Constraints.GOD_MANYOU="MANYOU";
		} else {
			Toggle_MS[1].isOn=false;
			Toggle_MS[0].isOn=true;

			Constraints.GOD_MANYOU="GOD";
		}
	}
	public void toggle_YX_YES()//音效开启事件监听
	{
		Constraints.YinXiao = "open";
	}
	public void toggle_YX_NO()//音效关闭事件监听
	{
		Constraints.YinXiao = "close";
	}
	public void Slider_Time()//时间缩放比事件监听
	{
		Constraints.timeScale = slider_YG_Time [1].value;
	}
	public void Slider_YG()//摇杆灵敏度设置
	{
		Constraints.timeScale = slider_YG_Time [0].value * 10;
	}
	public void Click_a()//星座按钮事件监听【转换场景】
	{
		Index = 0;
		StartCoroutine(Click());
		if(Constraints.YinXiao=="open")//添加音效
		{
			AudioSource.PlayClipAtPoint(Sound,new Vector3(0,0,0));//设置播放片段的位置，离摄像机越近越清晰
		}
		Application.LoadLevelAsync ("XingZuo");
	}
	public void Click_b()//选择太阳系观测模式
	{
		Index = 1;
		StartCoroutine(Click());
    	scroll.SetActive(false);
		moshishezhi.gameObject.SetActive (true);
		if(Constraints.YinXiao=="open")//添加音效
		{
			AudioSource.PlayClipAtPoint(Sound,new Vector3(0,0,0));//设置播放片段的位置，离摄像机越近越清晰
		}
		//********默认设置*****************
		if(Constraints.MS_Selected=="PuTong")
		{
			Toggle_GM.gameObject.SetActive(true);
			if(Constraints.GOD_MANYOU=="MANYOU")
			{
				ok [0].gameObject.SetActive (true);
				ok [1].gameObject.SetActive (false);
				ok [2].gameObject.SetActive (false);
				Toggle_MS[1].isOn=true;
			}else if(Constraints.GOD_MANYOU=="GOD")
			{
				ok [0].gameObject.SetActive (true);
				ok [1].gameObject.SetActive (false);
				ok [2].gameObject.SetActive (false);
				Toggle_MS[0].isOn=true;
			}
		}else if(Constraints.MS_Selected=="AR")
		{
			Toggle_GM.gameObject.SetActive(false);
			ok [0].gameObject.SetActive (false);
			ok [1].gameObject.SetActive (true);
			ok [2].gameObject.SetActive (false);
		}else if(Constraints.MS_Selected=="VR")
		{
			Toggle_GM.gameObject.SetActive(true);
			if(Constraints.GOD_MANYOU=="MANYOU")
			{
				ok [0].gameObject.SetActive (false);
				ok [1].gameObject.SetActive (false);
				ok [2].gameObject.SetActive (true);
				Toggle_MS[1].isOn=true;
			}else if(Constraints.GOD_MANYOU=="GOD")
			{
				ok [0].gameObject.SetActive (false);
				ok [1].gameObject.SetActive (false);
				ok [2].gameObject.SetActive (true);
				Toggle_MS[0].isOn=true;
			}
		}
	}
	public void Click_c()//AR/VR操作说明按钮监听
	{
		Index = 2;
		StartCoroutine(Click());
    	scroll.SetActive(true);
		if(Constraints.YinXiao=="open")//添加音效
		{
			AudioSource.PlayClipAtPoint(Sound,new Vector3(0,0,0));//设置播放片段的位置，离摄像机越近越清晰
		}
	}
	public void Click_d()//“设置”按钮监听
	{
		Index = 3;
		StartCoroutine(Click());
		moshishezhi.gameObject.SetActive (false);
    	scroll.SetActive(false);
	    shezhi.gameObject.SetActive (true);
		if(Constraints.VR_Alignment=="开启")
		{
			toggle[0].isOn=true;
		}else if(Constraints.VR_Alignment=="关闭")
		{
			toggle[1].isOn=true;
		}
		if(Constraints.YinXiao=="open")
		{
			toggle[2].isOn=true;
		}else if(Constraints.YinXiao=="close")
		{
			toggle[3].isOn=true;
		}
		if(Constraints.YinXiao=="open")//添加音效
		{
			AudioSource.PlayClipAtPoint(Sound,new Vector3(0,0,0));//设置播放片段的位置，离摄像机越近越清晰
		}
	}
	public void Click_e()//太阳系选择面板中【开始】按钮监听
	{
		Index = 4;
		if (Constraints.MS_Selected == "PuTong") {
			Application.LoadLevelAsync ("SkyboxBlueNebula_Scene");
		} else if (Constraints.MS_Selected == "AR") {
			Application.LoadLevelAsync ("AR");
		} else if (Constraints.MS_Selected == "VR") {
			Application.LoadLevelAsync("SkyboxBlueNebula_Scene");
		}
		if(Toggle_MS[0].isOn==true)//上帝视角
		{
			Constraints.GOD_MANYOU="GOD";
		}else if(Toggle_MS[1].isOn==true)//漫游
		{
			Constraints.GOD_MANYOU="MANYOU";
		}
		if(Constraints.YinXiao=="open")//添加音效
		{
			AudioSource.PlayClipAtPoint(Sound,new Vector3(0,0,0));//设置播放片段的位置，离摄像机越近越清晰
		}
	}

	//模式选择事件监听
	public void MSClick_PT()
	{
		ok [0].gameObject.SetActive (true);
		ok [1].gameObject.SetActive (false);
		ok [2].gameObject.SetActive (false);
		Constraints.MS_Selected = "PuTong";
		Toggle_GM.gameObject.SetActive (true);
	}
	public void MSClick_AR()
	{
		ok [0].gameObject.SetActive (false);
		ok [1].gameObject.SetActive (true);
		ok [2].gameObject.SetActive (false);
		Constraints.MS_Selected = "AR";
		Toggle_GM.gameObject.SetActive (false);
	}
	public void MSClick_VR()
	{
		ok [0].gameObject.SetActive (false);
		ok [1].gameObject.SetActive (false);
		ok [2].gameObject.SetActive (true);
		Constraints.MS_Selected = "VR";
		Toggle_GM.gameObject.SetActive (false);
	}
	public void SheiZhiBToM()
	{
		shezhi.gameObject.SetActive (false);
	}
    void Start()
    {

    }
	void Awake()
	{
		if(Constraints.BackTemp=="")
		{
			StartCoroutine (SpeedDown ());
		}else if(Constraints.BackTemp=="back")
		{
			moshishezhi.gameObject.SetActive (true);
			//********默认设置*****************
			if(Constraints.MS_Selected=="PuTong")
			{
				Toggle_GM.gameObject.SetActive(true);
				if(Constraints.GOD_MANYOU=="MANYOU")
				{
					ok [0].gameObject.SetActive (true);
					ok [1].gameObject.SetActive (false);
					ok [2].gameObject.SetActive (false);
					Toggle_MS[1].isOn=true;
				}else if(Constraints.GOD_MANYOU=="GOD")
				{
					ok [0].gameObject.SetActive (true);
					ok [1].gameObject.SetActive (false);
					ok [2].gameObject.SetActive (false);
					Toggle_MS[0].isOn=true;
				}
			}else if(Constraints.MS_Selected=="AR")
			{
				Toggle_GM.gameObject.SetActive(false);
				ok [0].gameObject.SetActive (false);
				ok [1].gameObject.SetActive (true);
				ok [2].gameObject.SetActive (false);
			}else if(Constraints.MS_Selected=="VR")
			{
				Toggle_GM.gameObject.SetActive(true);
				if(Constraints.GOD_MANYOU=="MANYOU")
				{
					ok [0].gameObject.SetActive (false);
					ok [1].gameObject.SetActive (false);
					ok [2].gameObject.SetActive (true);
					Toggle_MS[1].isOn=true;
				}else if(Constraints.GOD_MANYOU=="GOD")
				{
					ok [0].gameObject.SetActive (false);
					ok [1].gameObject.SetActive (false);
					ok [2].gameObject.SetActive (true);
					Toggle_MS[0].isOn=true;
				}
			}
		}
	}

    void Update()
    {    
		if(Input.GetKey("escape"))
		{
			Application.Quit();
		}
    }

    IEnumerator SpeedDown()
    {
		ImageBG.sprite = Index_MainMenu [0];
        yield return new WaitForSeconds(0.1f);
		ImageBG.sprite = Index_MainMenu [1];
		yield return new WaitForSeconds(0.1f);
		ImageBG.sprite = Index_MainMenu [2];
    }
	IEnumerator Click()
	{
		Index_Click [Index].gameObject.SetActive(true);
		yield return new WaitForSeconds(0.1f);
		Index_Click [Index].gameObject.SetActive (false);
	}

  void StopGPS() {
    Input.location.Stop();
  }
  IEnumerator StartGPS() {
    // Input.location 用于访问设备的位置属性（手持设备）, 静态的LocationService位置  
    // LocationService.isEnabledByUser 用户设置里的定位服务是否启用  
    if (!Input.location.isEnabledByUser) {
      GPS_info.text = "isEnabledByUser value is:" + Input.location.isEnabledByUser.ToString() + " Please turn on the GPS";
     // return false;
    }
    // LocationService.Start() 启动位置服务的更新,最后一个位置坐标会被使用  
    Input.location.Start(10.0f, 10.0f);

    int maxWait = 20;
    while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
      // 暂停协同程序的执行(1秒)  
      yield return new WaitForSeconds(1);
      maxWait--;
    }

    if (maxWait < 1) {
      GPS_info.text = "Init GPS service time out";
      //return false;
    }

    if (Input.location.status == LocationServiceStatus.Failed) {
      GPS_info.text = "Unable to determine device location";
      //return false;
    }
    else {
      GPS_info.text = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude + " Time:" + Input.location.lastData.timestamp;
        yield return new WaitForSeconds(100);
    }
  }
}