using UnityEngine;
using System.Collections;

[System.Serializable]
public class GuiData
{
	[SerializeField]
	private int id;
	[SerializeField]
	private string name;
	public GuiButton[] buttons;
		//public GuiData(){

	//}
}

[System.Serializable]
public class HitBox{
	[SerializeField]
	internal int x,y;
}

[System.Serializable]
public class GuiButton:HitBox
{
	//[SerializeField]
	//internal Sprite sprite;
	[SerializeField]
	internal bool isButton = true;
	[SerializeField]
	internal string message;
	[SerializeField]
	internal Transform parent;
	[SerializeField]
	internal GameObject gameObject;
}

