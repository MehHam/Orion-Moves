using UnityEngine;
//using System;
using System.Collections;

public class histogramBuilder : MonoBehaviour 
{
	private int rangeLim=20;
	public GameObject bin;
	public Material materialX;
	public Material materialY;
	public Material materialZ;
	public GameObject planeX;
	public GameObject planeY;
	public GameObject planeZ;
	public GameObject avatar;
	public float sizeHisto=10.0f;




				/// <summary>
			/// The main entry point for the application.
			/// </summary>
	/// 
	/// 
	/// 
	void Start()
	{
		Vector3[] rendo=randomGenerator(100,rangeLim);


		//histogramFunction(rangeLim,rendo,1);
		histogramGraphFromArray(histogramFunction(rangeLim,rendo,chooseAxis()),chooseAxis());

		}

	void Update()
	{

	}
	/// 
	public Vector2[] histogramFunction(int limMax, Vector3[] positionsToHisto, int coordinateXYZ)
	{

		Vector2[] histo =new Vector2[2*limMax+1];
		int arrondInt=0;

		for(int i=0;i<histo.Length;i++ )
		{
			histo[i]=new Vector2(i-limMax,0);
		}

		foreach (Vector3 vecteur in positionsToHisto)
		{

			switch(coordinateXYZ)
			{
			case 1:
			arrondInt=(int)vecteur.x;
			//histo[arrondInt+limMax] = new Vector2(arrondInt,(int)histo[arrondInt+limMax].y +1);
			break;
			
			case 2:
			arrondInt=(int)vecteur.y;
			//histo[arrondInt+limMax] = new Vector2(arrondInt,(int)histo[arrondInt+limMax].y +1);
			break;
			
			case 3:
			arrondInt=(int)vecteur.z;
			//histo[arrondInt+limMax] = new Vector2(arrondInt,(int)histo[arrondInt+limMax].y +1);
			break;

			}

			histo[arrondInt+limMax] = new Vector2(arrondInt,(int)histo[arrondInt+limMax].y +1);
		}


		return histo;
	}

	public Vector3[] randomGenerator(int size, float range)
	{
		Vector3[] vectRand=new Vector3[size];
		for (int i=0;i<size;i++)
		{
			vectRand[i]=new Vector3(Random.Range(-range,range),Random.Range(-range,range),Random.Range(-range,range));
			//Debug.Log (vectRand[i]);

		}
	return vectRand;
	}

	public void histogramGraphFromArray(Vector2[] tab, int coordinateXYZ)
	{
		GameObject go;
		float scale=0f;
		Color couleur;
		Quaternion rot;
		for (int i=0;i<tab.Length;i++)
		{
			switch(coordinateXYZ)
			{
			case 1:
				planeX.transform.localScale=new Vector3(sizeHisto, 1,sizeHisto*Screen.height/Screen.width);
				go=(GameObject)Instantiate(bin, new Vector3 (tab[i].x,planeX.transform.position.y,planeX.transform.position.z), Quaternion.identity);

				go.transform.parent=planeX.transform;//GameObject.Find("HistogramX").transform;
				go.name=i+"";

				scale = (1f*go.transform.parent.transform.lossyScale.z/*Mathf.Max(arrayConverterToY(tab)))*/*tab[i].y/Mathf.Max(arrayConverterToY(tab)));
				go.transform.localScale=new Vector3(0.2f*go.transform.parent.transform.localScale.x/tab.Length,scale,0.8f);
				go.transform.localPosition=new Vector3((0.5f*go.transform.parent.transform.localScale.x/tab.Length)*tab[i].x,0.8f,-0.5f*go.transform.localScale.y+go.transform.parent.transform.lossyScale.z/2);

				go.GetComponent<Renderer>().material= materialX;
	
			break;

			case 2:
				planeY.transform.localScale=new Vector3(sizeHisto, 1.0f,sizeHisto*Screen.height/Screen.width);
				go=(GameObject)Instantiate(bin, new Vector3 (tab[i].x,planeY.transform.position.y,planeY.transform.position.z), Quaternion.identity);
				//go=(GameObject)Instantiate(bin, planeX.transform.position, Quaternion.identity);
				go.transform.parent=planeY.transform;//GameObject.Find("HistogramX").transform;
				go.name=i+"";
				scale = (1f*go.transform.parent.transform.lossyScale.z/*Mathf.Max(arrayConverterToY(tab)))*/*tab[i].y/Mathf.Max(arrayConverterToY(tab)));
				go.transform.localScale=new Vector3(0.08f,scale,0.2f*go.transform.parent.transform.localScale.x/tab.Length);
				go.transform.localPosition=new Vector3((0.5f*go.transform.parent.transform.localScale.x/tab.Length)*tab[i].x,0.08f,-0.5f*go.transform.localScale.y+go.transform.parent.transform.lossyScale.z/2);

				go.GetComponent<Renderer>().material= materialY;

			
			break;

			case 3:
				planeZ.transform.localScale=new Vector3(sizeHisto,1.0f,sizeHisto*Screen.height/Screen.width);
				go=(GameObject)Instantiate(bin, new Vector3 (tab[i].x,planeZ.transform.position.y,planeZ.transform.position.z), Quaternion.identity);
				//go=(GameObject)Instantiate(bin, planeX.transform.position, Quaternion.identity);
				go.transform.parent=planeZ.transform;//GameObject.Find("HistogramX").transform;
				go.name=i+"";
				scale = (1f*go.transform.parent.transform.lossyScale.z/*Mathf.Max(arrayConverterToY(tab)))*/*tab[i].y/Mathf.Max(arrayConverterToY(tab)));
				go.transform.localScale=new Vector3(0.2f*go.transform.parent.transform.localScale.x/tab.Length,0.08f,scale);
				go.transform.localPosition=new Vector3((0.5f*go.transform.parent.transform.localScale.x/tab.Length)*tab[i].x,0.08f,0.5f*(go.transform.localScale.z-go.transform.parent.transform.lossyScale.z));
				go.GetComponent<Renderer>().material= materialZ;

				break;

			}

		}

	}

	private float[] arrayConverterToY(Vector2[] tab) // adapt for max X and max Z
	{
		float[] arr=new float[tab.Length];
		for(int i=0;i<tab.Length;i++)
		{
			arr[i]=tab[i].y;
		}
		return arr;
	}


	private int chooseAxis()
	{
		int intAxis=0;
		switch(gameObject.name)
		{
		case "PlaneX":
			intAxis=1;
			break;
		case "PlaneY":
			intAxis=2;
			break;
		case "PlaneZ":
			intAxis=3;
			break;
		}

		return intAxis;
	}

}
	

