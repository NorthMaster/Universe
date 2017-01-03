using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

    public Transform diqiu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(diqiu);
	}
}
