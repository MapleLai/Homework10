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