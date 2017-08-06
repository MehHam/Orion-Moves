/* -----------------------
    UDP-Receive (send to)
    -----------------------
    // [url]http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC[/url]
   
   
    // > receive
    // 127.0.0.1 : 8051
   
    // send
    // nc -u 127.0.0.1 8051
 */
 
using UnityEngine;
using System.Collections;
 
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;
 
public class UDPReceive : MonoBehaviour {
   
    // receiving Thread
    Thread receiveThread;
 
    // udpclient object
    UdpClient client;
	//public InputField inputIP;
	//public string IPstring;
    // public
    // public string IP = "127.0.0.1"; default local
    public int port=2390; // define > init

    // infos
    public static string lastReceivedUDPPacket="";
	public string allReceivedUDPPackets=""; // clean up this from time to time!
	public bool switcher= false;


   void OnDisable()
	{
		if ( receiveThread!= null)
		receiveThread.Abort();

		client.Close();
	} 
   
    // start from shell
   /* private static void Main()
    {
       UDPReceive receiveObj=new UDPReceive();
       receiveObj.init();
 
        string text="";
        do
        {
             text = Console.ReadLine();
        }
        while(!text.Equals("exit"));
    }
*/
// start from unity3d
    public void Start()
    {
		port=2390;
        init();
    }
	public void Update(){

		Debug.Log(port);
		//ReceiveData();
		Debug.Log(lastReceivedUDPPacket);

	}
    // OnGUI
    void OnGUI()
    {
      /* Rect rectObj=new Rect(900,10,200,400);
            GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.UpperRight;
        GUI.Box(rectObj,"# UDPReceive\n127.0.0.1 "+port+" #\n"
                    + "shell> nc -u 127.0.0.1 : "+port+" \n"
                    + "\nLast Packet: \n"+ lastReceivedUDPPacket
                    + "\n\nAll Messages: \n"+allReceivedUDPPackets
                ,style);*/
    }
       
    // init
    public void init()
    {
        //
        print("UDPSend.init()");
       
        // define port
       
		//inputIP.text="127.0.0.1";
        // status
        print("Sending to 127.0.0.1 : "+port);
        print("Test-Sending to this Port: nc -u 127.0.0.1  "+port+"");
 
        receiveThread = new Thread(
        new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
 
    }
 
    // receive thread
    private void ReceiveData()
    {
 
        client = new UdpClient(port);
        while (true)
        {
 
            try
            {
               
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Any/*IPAddress.Parse("192.168.0.15")*/, 0);
                byte[] data = client.Receive(ref anyIP);
 
                
                string text = Encoding.UTF8.GetString(data);
 
                              
                
				lastReceivedUDPPacket= text ;
//                // ....
               allReceivedUDPPackets=allReceivedUDPPackets+text;
               
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }
   
    // getLatestUDPPacket
    // cleans up the rest
    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets="";
        return lastReceivedUDPPacket;
    }

	public void switchReceiver()
	{ 
		init();
		Debug.Log("Receiving");
		switcher=!switcher;

	}

}