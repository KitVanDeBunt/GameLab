using UnityEngine;
using System.Collections;

public class GameInput {
	public static void updateInput() {
		Game game = Game.instance;
		Main main = Main.instance;
		//main.gui
		if(Input.GetKey(KeyCode.R)){
			game.UpdateMove ();
		}else if(Input.GetKey(KeyCode.JoystickButton5)){
			game.UpdateMove();
		}else{
			game.UpdateSelect();
		}
	}
}
