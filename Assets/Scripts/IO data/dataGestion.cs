using UnityEngine;
using System.Collections;
using System.IO;
using DigitalRuby.AnimatedLineRenderer;

public class dataGestion : MonoBehaviour {

	protected FileInfo theSourceFile = null;
	protected StreamReader reader = null;


//	protected TextWriter writer = null;
	//protected FileStream filestr=null;
	//protected FileStream reader=null;
	//protected FileStream writer=null;

	protected string text = "";
	ArrayList positions =new ArrayList{};

	void Start(){

		//filestr=new FileStream("gesture1.txt",FileMode.OpenOrCreate,FileAccess.ReadWrite);
	}


	public void loadData(){
		theSourceFile = new FileInfo("gesture1.txt");
		reader = theSourceFile.OpenText();

		
		while(text!=null){
			if(text != null){

				text=reader.ReadLine();
				positions.Add(Utils.stringToVector3(text));
				//animLine.Enqueue(Utils.stringToVector3(text));

				}else{
				reader.Close();
				}
		}
	}


	public void saveData(){

		/*StreamWriter writer =new StreamWriter("gesture2.txt");

		for(int i=0;i<DemoScriptDraw.linerendererFile.Count-1;i++){
		
			writer.WriteLine(DemoScriptDraw.linerendererFile[i].ToString());
		}
		writer.Close();*/

	}







}
