using UnityEngine;
using System.Collections;

public class XuanZhuan : MonoBehaviour {

    public float AngularVelocity;//公转角速度
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, new Vector3(0,1,0), AngularVelocity * Time.deltaTime);
	}
}
