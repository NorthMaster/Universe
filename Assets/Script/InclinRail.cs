using UnityEngine;
using System.Collections;
/*
倾斜公转轨道
 */
public class InclinRail : MonoBehaviour {

    public float AngularVelocity;//公转角速度
    public float Rail_Angle;//轨道倾角
    private float Angle_x;//公转轴的xy,z=0
    private float Angle_y;
	// Use this for initialization
	void Start () {
        Angle_x = Mathf.Sin(90.0f+Rail_Angle);
        Angle_y = Mathf.Cos(90.0f+Rail_Angle);
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Vector3.zero, new Vector3(Angle_x, Angle_y, 0), AngularVelocity * Time.deltaTime);
	}
}
