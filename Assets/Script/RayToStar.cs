using UnityEngine;
using System.Collections;
/*
 * 该脚本实现从场景中拾取某一个天体进入该天体详细信息界面
 */
public class RayToStar : MonoBehaviour {
	private bool isTouch=false;//设置全局时间缩放标志位
	// Use this for initialization
	void Start () {
	
	}	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
			if(t.phase==TouchPhase.Ended)//触屏结束后事件
			{
				isTouch=!isTouch;//标志位置反
			}
            if (t.phase == TouchPhase.Began)//触屏开始触发事件
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    GameObject gameObj = hitInfo.collider.gameObject;
                    if (gameObj.name == "Sun")
                    {
                        Constraints.NameStar = "Sun";
                    }
                    else if (gameObj.name == "shuixing1")
                    {
                        Constraints.NameStar = "shuixing1";
                    }
                    else if (gameObj.name == "jinxing2")
                    {
                        Constraints.NameStar = "jinxing2";
                    }
                    else if (gameObj.name == "diqiu3")
                    {
                        Constraints.NameStar = "diqiu3";
                    }
                    else if (gameObj.name == "huoxing4")
                    {
                        Constraints.NameStar = "huoxing4";
                    }
                    else if (gameObj.name == "muxing5")
                    {
                        Constraints.NameStar = "muxing5";
                    }
                    else if (gameObj.name == "tuxing6")
                    {
                        Constraints.NameStar = "tuxing6";
                    }
                    else if (gameObj.name == "tianwangxing7")
                    {
                        Constraints.NameStar = "tianwangxing7";
                    }
                    else if (gameObj.name == "haiwangxing8")
                    {
                        Constraints.NameStar = "haiwangxing8";
                    }
                    Application.LoadLevelAsync("XingUI");
                }
            }
        }
		if (isTouch) {//设置全局的时间
			Time.timeScale = Constraints.timeScale;
		} else {
			Time.timeScale=1.0f;
		}
	}
}
