using UnityEngine;
using System.Collections;

public class TouchScale : MonoBehaviour {

	private Touch oldTouch1;  //上次触摸点1(手指1)  
	private Touch oldTouch2;  //上次触摸点2(手指2) 
	
	void Update () {

		if(Input.touchCount!=2)
		{
			return;
		}

		//多点触摸, 放大缩小  
		Touch newTouch1 = Input.GetTouch(0);
		Touch newTouch2 = Input.GetTouch(1);
		
		//第2点刚开始接触屏幕, 只记录，不做处理  
		if (newTouch2.phase == TouchPhase.Began)
		{
			oldTouch2 = newTouch2;
			oldTouch1 = newTouch1;
			return;
		}
		
		//计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
		float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
		float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
		
		//两个距离之差，为正表示放大手势，为负表示缩小手势  
		float offset = newDistance - oldDistance;

		float scaleFactor = offset / 10f;//设置缩放比
		//在主摄像机field of view(即Right、Left、Top、Bottom)的值在（20.0f~100.0f）范围内才能缩放
		if(Camera.main.GetComponent<Camera>().fieldOfView-scaleFactor<5.0f)
		{
			Camera.main.GetComponent<Camera>().fieldOfView=5.0f;
		}else if(Camera.main.GetComponent<Camera>().fieldOfView-scaleFactor>65.0f)
		{
			Camera.main.GetComponent<Camera>().fieldOfView=65.0f;
		}else{
			Camera.main.GetComponent<Camera>().fieldOfView-=scaleFactor;
		}
		//记住最新的触摸点，下次使用  
		oldTouch1 = newTouch1;
		oldTouch2 = newTouch2;
	}
}
