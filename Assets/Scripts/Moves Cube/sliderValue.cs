using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderValue : MonoBehaviour {

	public Slider sldr;
	public static Vector3 datas;
	public Gyroscope gyro;
	// Use this for initialization

	void Start (){
		gyro=Input.gyro;
		gyro.enabled = true ;

	}
	
	// Update is called once per frame
	void Update () {

		changerSldr(sldr);

		
	}

	void changerSldr(Slider sldr){
		Transform t = transform.Find("UDPListener");

		switch ((int)sldr.value){
		case (0):
			t.gameObject.SetActive(false);
			datas=new Vector3(Input.acceleration.x * gyro.gravity.x , Input.acceleration.y * gyro.gravity.y ,Input.acceleration.z * gyro.gravity.z );
			break;
		case (1):
			t.gameObject.SetActive(true) ;

			break;
		}


	}
}
