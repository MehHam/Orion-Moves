﻿
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
 
public class UDPSend : MonoBehaviour
{
    private static int localPort;
	public Gyroscope gyro;
    // prefs
	public InputField IPfield;  // define in init
    public int port;  // define in init
   
    // "connection" things
    IPEndPoint remoteEndPoint;
    UdpClient client;
   
	private bool switcher;
	public string IP;
	public Text textInfo;

    // gui
    string strMessage="";
   void OnDisable()
		{
			/*if ( receiveThread!= null)
			receiveThread.Abort();*/

			client.Close();
		}

		
       
    // call it from shell (as program)
    private static void Main()
    {
        UDPSend sendObj=new UDPSend();
        sendObj.init();
       
        // testing via console
        // sendObj.inputFromConsole();
       
        // as server sending endless
        sendObj.sendEndless(" endless infos \n");
       
    }
    // start from unity3d
    public void Start()
    {
		IPfield.text="192.168.0.0";

		gyro = Input.gyro;
		gyro.enabled= true;

		switcher=false;
		init();
    }
   
	private void Update()
	{
		if(switcher){
			
			sendString("Acc\t" + Input.acceleration.magnitude +"\t"+ "Gyro\t" + gyro.gravity.ToString());
			textInfo.text = "Sending Acceleration Magnitude" + Input.acceleration.ToString() +" and Gyroscope " + gyro.gravity.ToString() + " to target";
		}else{
			textInfo.text =" Waiting for instructions";

		}


	}

    // OnGUI
    void OnGUI()
    {
       /* Rect rectObj=new Rect(900,380,800,600);
            GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.UpperLeft;
		GUI.Box(rectObj,"# UDPSend-Data\n " + IP + port+" #\n"
                    + "shell> nc -lu 127.0.0.1  "+port+" \n"
                ,style);
       
        // ------------------------
        // send it
        // ------------------------
        strMessage=GUI.TextField(new Rect(950,500,400,200),strMessage);
        if (GUI.Button(new Rect(700,500,200,100),"send"))
        {
            sendString(strMessage+"\n");
        }     */ 
    }
   
    // init
    public void init()
    {
     
        print("UDPSend.init()");
       
        // define
		string IP=IPfield.text;
        port=8080;
       
        // ----------------------------
        // Send
        // ----------------------------
		remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
       
        // status
        print("Sending to "+ IP +" : "+port);
        print("Testing: nc -lu "+IP+" : "+port);
   
    }
 
    // inputFromConsole
    private void inputFromConsole()
    {
        try
        {
            string text;
            do
            {
                text = Console.ReadLine();
 
                // Den Text zum Remote-Client senden.
                if (text != "")
                {
 
                    
                    byte[] data = Encoding.UTF8.GetBytes(text);
 
                   
                    client.Send(data, data.Length, remoteEndPoint);
                }
            } while (text != "");
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
 
    }
 
    // sendData
    private void sendString(string message)
    {
		Debug.Log(message);
        try
        {
                //if (message != "")
                //{
 
                    
                    byte[] data = Encoding.UTF8.GetBytes(message);
 
                   
                    client.Send(data, data.Length, remoteEndPoint);
                //}
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }
   
   
    // endless test
    private void sendEndless(string testStr)
    {
        do
        {
            sendString(testStr);
           
           
        }
        while(true);
       
    }
	
	public void switchSender()
	{ 
		init();
		Debug.Log("Clicked");
		switcher=!switcher;

	}
   
}
  
