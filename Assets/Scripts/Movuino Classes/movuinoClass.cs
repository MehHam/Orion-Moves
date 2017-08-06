using UnityEngine;
using System.Collections;

public class movuinoClass :MonoBehaviour {

	public string name;
	public Vector3[] arrayAcc;
	public Vector3[] arraygyro;

	public movuinoClass(){} //constructor

	public movuinoClass(string name,Vector3[] arrAcc,Vector3[]arrGyro)//constructor parameters

	{
		this.name=name;
		this.arrayAcc=arrAcc;
		this.arraygyro=arrGyro;
	
	}

	public void setName(string name)
	{
		name =name;
	}

	public void setAccel(Vector3[] acc)
	{
		arrayAcc=acc;

	}
	public void setGyro(Vector3[] gyro)
	{
		arraygyro=gyro;
	}

	public static bool movuinoCompare(movuinoClass mov1, movuinoClass mov2){
		//returns true if name is same
		bool isSame = false;
		if(mov1.name==mov2.name)
			isSame=true;

		return isSame;
	}


}
	

