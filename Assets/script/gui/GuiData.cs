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
	internal int width,height,x,y;
}

[System.Serializable]
public class GuiButton:HitBox
{
	[SerializeField]
	private bool drawHitBox;
	[SerializeField]
	internal Sprite sprite;
}

