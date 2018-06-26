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
