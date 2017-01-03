using UnityEngine;
using System.Collections;

public class ZiZhuan : MonoBehaviour {

    public float AngularVelocity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.position, transform.up, AngularVelocity * Time.deltaTime);
	}
}
