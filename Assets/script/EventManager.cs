using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	public delegate void Select(int[] selectedIds);
	public static event Select OnSelect;
	
	public static void CallOnSelect(int[] selectedID){
		OnSelect(selectedID);
	}

	public delegate void GuiInput(int buttonId);
	public static event GuiInput OnGuiInput;

	public static void callOnGuiInput(int id){
		OnGuiInput (id);
	}
}
