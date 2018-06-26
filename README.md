# 网络版巡逻兵
--------------
## 游戏
基于之前巡逻兵的游戏改成这一次的双人网络版巡逻兵。创建服务器的为玩家1，后面加入的为玩家2。玩家2担当巡逻兵，在30s内追赶到玩家1则获得游戏胜利，否则玩家1胜利。

## 演示
+ 游戏场景
![Player1](https://raw.githubusercontent.com/MapleLai/Homework10/master/Screenshot/Player1.jpg)
![Player2](https://raw.githubusercontent.com/MapleLai/Homework10/master/Screenshot/Player2.png)
+ 由于录屏时操作失误。演示视频只录了一个窗口，但也能看出是个网络游戏，希望TA能谅解  
[演示视频](https://pan.baidu.com/s/1JTS6024eN8ku4T5_fM85hg)

## 介绍
+ Player  
在Player预制上添加NetworkIdentity和NetworkTransform组件，使它能够在网络中被实例化和同步位置。
![Player](https://raw.githubusercontent.com/MapleLai/Homework10/master/Screenshot/Player.jpg)

+ NetworkManager  
利用空对象创建一个NetworkManager管理网络状态，添加NetworkManager和NetworkManagerHUD组件。把刚才的Player拖进玩家预制插槽里。  
![View](https://raw.githubusercontent.com/MapleLai/Homework10/master/Screenshot/View.png)
![NetworkManager](https://raw.githubusercontent.com/MapleLai/Homework10/master/Screenshot/NetworkManager.jpg)

+ 游戏脚本  
本以为把一个游戏弄成网络游戏并不难，但实际做起来时才发现问题多多，比如怎么在网络中同步变量，又或者是脚本到底是在哪里运行的，要做好一个网络游戏还是有一定难度的。因此，为了减少出现的错误，这次简化了脚本，简单分成场景类和玩家类。  

+ 场景类  
负责管理两个玩家，还有记录游戏状态，当分出胜负时出现提示信息。有个bug就是提示的信息在两个窗口并不同步，但我还没找到比较好的解决办法。
<pre>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour {

	private static FirstSceneController _instance;
	public GameObject[] players;
	public int playersCount;
	public int gameOver;
	public float time;

	public static FirstSceneController getInstance() {
		return _instance;
	}
		
	void Awake(){
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		gameOver = -1000;
		time = 0;
		players = new GameObject[2];
		playersCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (time > 0)
			time -= Time.deltaTime;
		if (time < 0)
			gameOver = 1;
	}

	public void addPlayer(){
		playersCount++;
		if (playersCount == 2)
			time = 30f;
	}

	public void setGameOver(int go){
		gameOver = go;
	}

	void OnGUI(){
		if (FirstSceneController.getInstance().gameOver == -1) {
			GUIStyle style = new GUIStyle();
			style.normal.background = null;
			style.normal.textColor = new Color(0, 0, 0);
			style.fontSize = 40;
			GUI.TextField (new Rect (200, 150, 300, 50), "Player2 Wins!", style);
		}
		if (FirstSceneController.getInstance().gameOver == 1) {
			GUIStyle style = new GUIStyle();
			style.normal.background = null;
			style.normal.textColor = new Color(0, 0, 0);
			style.fontSize = 40;
			GUI.TextField (new Rect (200, 150, 300, 50), "Player1 Wins!", style);
		}
	}

}
</pre>

+ 玩家类  
负责玩家的独立移动，当发生碰撞时把结果告诉场景类
<pre>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UserInterface : NetworkBehaviour {
	// Use this for initialization

	void Start () {
		if(isLocalPlayer)
			CmdAddPlayer();
	}

	void Update () {
		if (!isLocalPlayer)
			return;

		float translationX = Input.GetAxis ("Horizontal") * 6f;
		float translationZ = Input.GetAxis ("Vertical") * 6f;
		translationX *= Time.deltaTime;
		translationZ *= Time.deltaTime;
		this.transform.Translate (translationX, 0, 0);
		this.transform.Translate (0, 0, translationZ);
	}

	[Command]
	void CmdAddPlayer(){
		FirstSceneController.getInstance().addPlayer();
	}

	void OnCollisionStay(Collision other){
		if (other.gameObject.tag == "Player")
			FirstSceneController.getInstance().setGameOver(-1);
	}

}
</pre>

+ 总结  
这次因为在课上按教程操作时比较顺利，以为这次的作业挺简单的，但实际做起来后才发现有很多问题是课上操作时没发现的。这就导致了预留完成作业的时间不够，遇到的很多bug都还没有解决，诸如变量的不同步导致提示信息不一致。这是需要反省的，毕竟是第一次接触网络化，不应该这么掉以轻心。感觉这才是游戏设计的真正入门，但我却没有做好，如果之后还想在这方面取得进步，一定要好好研究。
