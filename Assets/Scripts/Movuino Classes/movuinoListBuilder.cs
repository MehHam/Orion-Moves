using UnityEngine;
using System.Collections;

public class movuinoListBuilder : MonoBehaviour {
	public int nmbOfMovuino=1;
	movuinoClass movui1= new movuinoClass();
	movuinoClass movuiTempo;
	public static movuinoClass[] movuinoArray;


	// Use this for initialization
		
	// Update is called once per frame
	void Update () {


		if(movui1.name==null){
			movuinoArray=new movuinoClass[nmbOfMovuino];
			movui1=movuinoElement(stringSeperator(UDPReceive.lastReceivedUDPPacket));
			movuinoArray[0]=movui1;
		}else{
			movuiTempo = movuinoElement(stringSeperator(UDPReceive.lastReceivedUDPPacket));
			int indexSeek=comparisonIndexSeeker(movuinoArray,movuiTempo);
			if(indexSeek!=-1)
			{
				movui1=movuinoArray[indexSeek];
				movui1.arrayAcc=dynamicAccelArray(movui1.arrayAcc,movuiTempo.arrayAcc[movuiTempo.arrayAcc.Length-1]);
				movuinoArray[indexSeek]=movui1;
			}else{
				//movuiTempo= dynamicMovuinoArray(movuinoArray,movuiTempo);
			}

		}
		//Debug.Log(movuinoArray[0].arrayAcc[movuinoArray[0].arrayAcc.Length-1]);

	}
	public int comparisonIndexSeeker(movuinoClass[] arr, movuinoClass seekedMovuino)
	{
		int index=-1;

		for(int i=0;i<arr.Length;i++)
		{
			if(movuinoClass.movuinoCompare(arr[i],seekedMovuino))
				index=i;
			else
				index=-1;

		}

		return index; 
	}


	public Vector3[] dynamicAccelArray(Vector3[] arr, Vector3 accel) //modifies the array of accelerations dynamically 
	{
		ArrayList arrList= new ArrayList{};
		
		for(int i=0;i<arr.Length;i++){
			arrList.Add(arr[i]);
		}
		
		arrList.Add(accel);

		arr= new Vector3[arrList.Count];

		for(int i=0;i<arr.Length;i++){
			arr[i]=(Vector3)arrList[i];
		}
		
		return arr;
	}


	public movuinoClass[] dynamicMovuinoArray(movuinoClass[] arr, movuinoClass mov) //modifies the array of movuinos dynamically 
	{
		ArrayList arrList= new ArrayList{};

		for(int i=0;i<arr.Length;i++){
			arrList.Add(arr[i]);
		}

		arrList.Add(mov);

		arr=(movuinoClass[])arrList.ToArray();

		return arr;
	}

	public movuinoClass movuinoElement(string[] arr) //instantiate a movuinoClass element from the tring 
	{
		ArrayList accelList=new ArrayList{};
		ArrayList gyroList=new ArrayList{};
		string name=""+arr[2]+arr[3]+arr[4]+arr[5];
		accelList.Add(new Vector3(int.Parse(arr[9]), int.Parse(arr[10]), int.Parse(arr[11])));

		movuinoClass mov1=new movuinoClass(name,arrayListConverter(accelList),arrayListConverter(gyroList));

		return mov1;
	}

	public ArrayList vectorArrayConverter(Vector3[] arrVect) //converts Vector3[] to arrayList
	{
		ArrayList arr=new ArrayList{};

		for (int i=0;i<arrVect.Length;i++){
		
			arr[i]=(Vector3)arrVect[i];
		
		}
		return arr;
	}

	public Vector3[] arrayListConverter(ArrayList arrList)//converts arraylist to vector
	{

		Vector3[] arr=new Vector3[arrList.Count];

		for (int i=0;i<arrList.Count;i++){

			arr[i]=(Vector3)arrList[i];

		}
		return arr;
	}


	public string[] stringSeperator(string texto) // transforms the Movuino UDP message into a string[]
	{
		string[] sArray=null;
		if(texto==null || texto == " "){
			
		}else{
		sArray=texto.Split(' ');
			/*for(int i=0; i<sArray.Length;i++){

				//Debug.Log(sArray[i]);

			}*/

		}

		return sArray;
	}
		// transforms the Movuino UDP message into a movuinoClass element

	

}
