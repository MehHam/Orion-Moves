using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

	[RequireComponent(typeof(Collider))]
	public class ActivateOptions : MonoBehaviour {

		private Vector3 startingPosition;

		public Material inactiveMaterial;
		public Material gazedAtMaterial;
		public Text txt;
		private GameObject go;

		public int port1;  
		public int port2;  

		void Start() {
			startingPosition = transform.localPosition;
			SetGazedAt(false);
			//go=GameObject.Find("TestCharacter");
			GameObject.Find("Player").transform.position=new Vector3(-40f,0f, 0f);

			port1=2390;
			port2=8080;
		}

	void Update(){
		
		//Debug.Log(transform.localPosition);


	}

		public void SetGazedAt(bool gazedAt) {

			if (inactiveMaterial != null && gazedAtMaterial != null) {
				GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
				Color c;
				c = GetComponent<MeshRenderer>().material.color; 
				c.a = gazedAt ? 1f:0f ;
				GetComponent<MeshRenderer>().material.color = c;
				return;
			}
			//GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
			
			

			
		}

		public void Reset() {
			transform.localPosition = startingPosition;
		}

		public void Recenter() {
			#if !UNITY_EDITOR
			GvrCardboardHelpers.Recenter();
			#else
			GvrEditorEmulator emulator = FindObjectOfType<GvrEditorEmulator>();
			if (emulator == null) {
				return;
			}
			emulator.Recenter();
			#endif  // !UNITY_EDITOR
		}

		public void TeleportRandomly() {
			Vector3 direction = Random.onUnitSphere;
			switcherCubes();
			
		}

		public void switcherCubes(){

			if(this.gameObject.name=="Resolution"){
				if(GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().isResolutionSwitched)
					GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().resolution++;
				else
					GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().resolution--;

				txt.text = " Resolution: " + GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().resolution;
			}

			if(this.gameObject.name=="OutIn"){
				

				if(GameObject.Find("Player").transform.position==Vector3.zero){
					GameObject.Find("Player").transform.position= new Vector3(-40f,30f, -40f);
					txt.text = "higher : Look Down " + GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().resolution;
				}else{	
					if(GameObject.Find("Player").transform.position==new Vector3(-40f,30f, -40f)){

						GameObject.Find("Player").transform.position=new Vector3(-40f,0f, 0f);
						txt.text = " Face " ;
					}else{
						if(GameObject.Find("Player").transform.position==new Vector3(-40f,0f, 0f)){

						GameObject.Find("Player").transform.position= Vector3.zero;//Vector3.Lerp(GameObject.Find("Player").transform.position,Vector3.zero, 2.0f*Time.deltaTime);
							txt.text = " Centered " ;
						}
					}
				}

			}

			if(this.gameObject.name=="RandomizedVectors"){
				bool boolrandom = GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().isRandom ; 

				GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().isRandom = !boolrandom;

				GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().points=null;
				GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().CreatePoints(GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().isStatic);

				txt.text = " On remote? \t"+ !GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().isRandom;
			}

			if(this.gameObject.name=="isStatic"){
				bool boolrandom = GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().isStatic ; 

				GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().isStatic = !boolrandom;
				GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().points=null;
				GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().CreatePoints(GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().isStatic);

				txt.text = " Is static vectors?\t"+ GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().isStatic;

			}
			
			if(this.gameObject.name=="Infos"){
				
				if(GameObject.Find("UDPListener").GetComponent<UDPReceive>().port ==port1)
				{
					GameObject.Find("UDPListener").GetComponent<UDPReceive>().port =port2;

				}else{
					GameObject.Find("UDPListener").GetComponent<UDPReceive>().port =port1;

				}
				GameObject.Find("UDPListener").GetComponent<UDPReceive>().init();

			txt.text = " Port: " + GameObject.Find("UDPListener").GetComponent<UDPReceive>().port;
			}
			
	}
 
		public void textUIChanger(bool isPointed){

			if(isPointed){

			transform.Find("FireBall").gameObject.SetActive(true);
			GameObject.Find("Canvas").transform.Find("Panel").gameObject.SetActive(true);
			GameObject.Find("options").GetComponent<rotate>().constant=0f;
			GetComponent<rotateOptions>().constant=0f;	

			if(this.gameObject.name=="Resolution"){

				txt.text = " Resolution: " + GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().resolution;

				}

			if(this.gameObject.name=="OutIn"){
					
				txt.text = " Change the Point of view";
				}

			if(this.gameObject.name=="RandomizedVectors"){
					
				txt.text = " On Remote? \t"+ !GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().isRandom;
				}

			if(this.gameObject.name=="isStatic"){

				txt.text = " Is static vectors?\t"+ GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().isStatic;
			}

			if(this.gameObject.name=="Infos"){
				//if(UDPReceive.lastReceivedUDPPacket!="")
					txt.text =  GameObject.Find("StarsMovesCube").GetComponent<Grapher3>().info;
			}

			}else{
				txt.text = " ";
				transform.Find("FireBall").gameObject.SetActive(false);
				
				GameObject.Find("Canvas").transform.Find("Panel").gameObject.SetActive(false);
				GameObject.Find("options").GetComponent<rotate>().constant=20f;
				GetComponent<rotateOptions>().constant=200f;	
			}

			
		}

		// maximize acceleration 
		// check movuino values, and split them
}