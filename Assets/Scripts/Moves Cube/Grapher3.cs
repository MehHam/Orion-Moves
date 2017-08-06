using UnityEngine;
using System.Collections.Generic;

public class Grapher3 : MonoBehaviour {

	public enum FunctionOption {
		
		Ripple,
		Custom
	}

	private delegate float FunctionDelegate (Vector3 p, float t);
	private static FunctionDelegate[] functionDelegates = {
		
		Ripple,
		Custom
	};

	public FunctionOption function;
	public bool absolute;
	public float coeffCube=1f;
	public bool isRandom;

	public float sizeCube=1.0f; // max range of acc or gyro
	public float threshold ;
	
	[Range(2,7)]
	public int resolution = 2;

	private int currentResolution;
	private int currReso;
	public ParticleSystem.Particle[] points;
	private Vector4[] indexesVectors ;
	private Vector4[] colorsVectors;
	private int[] counters;
	public static Vector3 AccelerationMangnitude;
	public static Vector3 gyroVector;

	public string[] stringArray;
	public string info = "";
	private Vector3 maximumAccVector;
	public bool isStatic;
	public bool isResolutionSwitched;

	//private float increment ;


	void Start(){
		
		sizeCube=sizeCube*coeffCube;
		isRandom = false;
		maximumAccVector=2*Vector3.one;
		isStatic=true;
		isResolutionSwitched = true;
	}

	void Update () {

		//Array is given by [0] = Acc [1] = value of Vector2 acceleration [2] gyro [3] Vector3 of gyro
		resolutionSwitcher();

		if(isStatic){
			if (currentResolution != resolution || points == null)
			{
				CreatePoints(isStatic);
			}
				//randomizedCalculus(isRandom, UDPReceive.lastReceivedUDPPacket.Split('\t'));
		}else{
			
			if (currentResolution != resolution || points == null)
			{
				CreatePoints(isStatic);
			}

				randomizedCalculus(isRandom, UDPReceive.lastReceivedUDPPacket.Split('\t'));
		}
			Debug.Log(UDPReceive.lastReceivedUDPPacket.Split('\t').Length);
	
	}

	public void resolutionSwitcher(){

		if(resolution==8){
			isResolutionSwitched= false;
		}
		if(resolution==2)
			isResolutionSwitched= true;

	}

	public void randomizedCalculus(bool isRandom, string[] arr){

		if(isRandom){
			Vector3 ve= Utils.randomVectorGenerator(1.2f*sizeCube,1.2f*sizeCube);
			Vector3 veAcc= Utils.randomVectorGenerator(1.2f*sizeCube,1.2f*sizeCube);
			//Vector3 ve= -1f*Vector3.zero;
			//Vector3 veAcc= 1*Vector3.one;// to check if the mapping is OK vector3 1 is color(255,255,255) Vector3 -1, is color (0,0,0) 

			float ii = Utils.VectorPostioninCube(sizeCube, currentResolution, ve, indexesVectors);

			particleSetter(ii,counters, veAcc);
			info= "In Random mode " ;

		} else{
			if(arr.Length==4){

			AccelerationMangnitude = Utils.stringToVector3(arr[1]);

			gyroVector=Utils.stringToVector3(arr[3]);
			Vector3 ve= gyroVector;
			Vector3 veAcc= AccelerationMangnitude;
			
			info= "receiving data: Acceleration = " + AccelerationMangnitude+ " ,Gyroscope = "+ gyroVector;

				float ii = Utils.VectorPostioninCube(sizeCube, currentResolution, ve, indexesVectors);
			

			particleSetter(ii,counters, veAcc);
			}



		}

	}

	public void particleSetter(float inde, int[] counters, Vector3 ve2){

		int index =(int)inde;

		float increment = sizeCube / (1f*currentResolution);

		int j = counters[index];
		counters[index] = j+1;

		int sum = Mathf.Max(counters);//Utils.ArraySum(counters); 

		if(sum>=1)
		{
			float c = 1.0f*counters[index]/sum;
		
			Color cic = new Color(colorsVectors[index].x,colorsVectors[index].y,colorsVectors[index].z,1.0f*counters[index]/sum) ;//points[index].color;

			points[index].color = cic;

			float sz = points[index].size;
			sz=ve2.magnitude*1.5f*increment/maximumAccVector.magnitude;//increment*(1.0f*counters[index]/sum);
			points[index].size = sz;

			//Debug.Log(index + "\t" + points[index].color + "\t" + sum  +"\t" + counters[index]);

		}

		GetComponent<ParticleSystem>().SetParticles(points, points.Length);

	}


	public void CreatePoints (bool isStatic) {

		threshold = 1.0f*sizeCube;
		currentResolution = resolution;
		currReso = 2*resolution+1;

		points = new ParticleSystem.Particle[(currReso) * (currReso) * (currReso)];

		indexesVectors= new Vector4[currReso*currReso*currReso];
		counters = new int[currReso*currReso*currReso]; 
		colorsVectors= new Vector4[currReso*currReso*currReso];

		for(int k=0; k< counters.Length; k++)
			counters[k]=0;

		float increment = sizeCube / (1f*currReso);

		int i = 0;
		int j=0;
		for (int x = 0; x < currReso; x++) {
			for (int y = 0; y <currReso; y++) {
				for (int z = 0; z < currReso; z++) {
					
					Vector3 p = new Vector3(x, y, z) * sizeCube / (1f*resolution) - 1f*threshold*Vector3.one;
					points[i].position = p;

					//indexesVectors[j]= new Vector4(p.x,p.y,p.z,i);
					indexesVectors[j]= new Vector4(x,y,z,i);


					if(isStatic){

						points[i].color =new Color((x*(1f*increment)), (y*(1f*increment)),(z*(1f*increment)), 1f);
						colorsVectors[j]=new Vector4((x*(1f*increment)), (y*(1f*increment)),(z*(1f*increment)),i);


					}
					else{
						points[i].color =new Color((x*(1f*increment)), (y*(1f*increment)),(z*(1f*increment)), 1f);
						colorsVectors[j]=new Vector4((x*(1f*increment)), (y*(1f*increment)),(z*(1f*increment)),i);
						points[i].color = Color.black;
						}

					points[i++].size = 2.0f*increment;
//					Debug.Log(points[i++].size);
					j++;

				}
			}
		}


		GetComponent<ParticleSystem>().SetParticles(points, points.Length);

	}


    private static float Ripple (Vector3 p, float t){
		p.x -= 0.5f;
		p.y -= 0.5f;
		p.z -= 0.5f;
		float squareRadius = p.x * p.x + p.y * p.y + p.z * p.z;
		return Mathf.Sin(4f * Mathf.PI * squareRadius - 2f * t);
	}

	private static float Custom (Vector3 p, float t){




		return 1f;
	}



}