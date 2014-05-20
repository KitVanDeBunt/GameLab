using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour {
	private static Main main;

	public static void init(Main m) {
		main = m;
	}

	public static void updateInput() {
		if(Input.GetKey(KeyCode.R)){
			main.UpdateMove ();
		}else{
			main.UpdateSelect();
		}
	}
}
