using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

	public float constant = 20f; 
	// Use this for initialization
	void Start () {
		spawnCircular();
	}

	// Update is called once per frame
	void Update () {

		transform.RotateAround(transform.position, Vector3.up, constant * Time.deltaTime);

	}

	void spawnCircular(){

		GameObject go = GameObject.Find("options");

		Vector3 center = go.transform.position;
		Transform[] tr = GetComponentsInChildren<Transform>();  
		int ch = go.transform.childCount;

		float radius = 1f;

		for (int i = 0; i <= ch; i++){



			Vector3 pos = radius * new Vector3(Mathf.Cos(2*i*Mathf.PI/(ch)), Mathf.Sin(2*i*Mathf.PI/(ch)), 0f);

			if(tr[i].gameObject.tag == "optionTag"){
				tr[i].gameObject.transform.localPosition = pos ;

				Debug.Log(i + "\t"+ tr[i].name + "\t"+ pos);

			}
		}

	}


}




/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {
	
	public float constant = 20f; 
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
		transform.RotateAround(transform.position, Vector3.up, constant * Time.deltaTime);

	
	}
}*/
