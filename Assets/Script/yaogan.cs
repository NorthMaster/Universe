using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class yaogan : MonoBehaviour
{
	
	private float[] axisInput = new float[2];
	public Camera CameraL;
	
	void Start()
	{
		for (int i = 0; i < axisInput.Length; i++)
			axisInput[i] = 0.0f;
	}
	
	void Update()
	{
		axisInput[0] = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
		axisInput[1] = Input.GetAxisRaw("Vertical") * Time.deltaTime;
		
		GetComponent<CharacterController>().SimpleMove(CameraL.transform.forward * axisInput[1] * 6.0f);
		GetComponent<CharacterController>().SimpleMove(CameraL.transform.right * axisInput[0] * 6.0f);
		
		if (Input.GetKey(KeyCode.Joystick1Button10))
		{
			//对应摇杆上的“Start”键
		}
	}
}

