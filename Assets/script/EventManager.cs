using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {
	public delegate void Select(int selectedIds);
	public static event Select OnSelect;
	
	public static void CallOnSelect(int selectedID){
		OnSelect(selectedID);
	}
}
