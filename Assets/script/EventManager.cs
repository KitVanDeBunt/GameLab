using UnityEngine;
using System.Collections;

public enum Message{
	Default,
	Pause,
	Play,
	Test
}

public class EventManager {
	public delegate void Select(int[] selectedIds);
	public static event Select OnSelect;
	
	public static void CallOnSelect(int[] selectedID){
		OnSelect(selectedID);
	}

	public delegate void GuiInput(Message message);
	public static event GuiInput OnGuiInput;

	public static void callOnGuiInput(Message message){
		if(OnGuiInput != null){
			OnGuiInput (message);
		}
	}
}
