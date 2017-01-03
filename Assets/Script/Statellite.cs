using UnityEngine;
using System.Collections;
/*
卫星脚本
 */
public class Statellite : MonoBehaviour {

    public GameObject MainStatellite;//宿主星
    public float AngularVelocity;//绕宿主星公转角速度
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(MainStatellite.transform.position, MainStatellite.transform.up, AngularVelocity * Time.deltaTime);
	}
}
