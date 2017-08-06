using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateOptions : MonoBehaviour {
	public float constant = 400f; 
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
		transform.RotateAround(transform.position, Vector3.one, constant * Time.deltaTime);

	
	}
}
