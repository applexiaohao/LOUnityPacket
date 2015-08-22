using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PacketScript : MonoBehaviour {


	private UIPanel Window{ set; get;}
	private LONGUIPacketController Packet{ set; get;}
	// Use this for initialization
	void Start () {
		
		//创建程序窗口
		this.Window = NGUITools.CreateUI(false);

		//创建背包滚动视图
		this.Packet = new LONGUIPacketController(this.Window.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
