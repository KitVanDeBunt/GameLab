using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GuiManager
{
	[SerializeField]
	private int currentActive = 0;

	[SerializeField]
	private GuiData[] guiData;

	private List<GameObject> buttonGameObjects;
	public void init(Camera _cam){
		BuildGui(_cam);
	}

	public void BuildGui(Camera _cam){
		buttonGameObjects = new List<GameObject>();
		//Debug.Log ("[GUI] "+guiData.Length);
		//Debug.Log ("[GUI]buttons: "+guiData[currentActive].buttons.Length);

		float screenHeight = 2f * _cam.orthographicSize;
		float screenWidth = screenHeight * _cam.aspect;
		Vector3 TopRight = new Vector3( screenWidth/2, screenHeight/2,10)+_cam.transform.position;
		Vector3 BottomLeft = new Vector3( -screenWidth/2, -screenHeight/2,10)+_cam.transform.position;

		for (int i = 0; i < guiData[currentActive].buttons.Length; i++) {
			string name = "button "+i.ToString();
			GameObject button = new GameObject(name);
			button.layer = LayerMask.NameToLayer("Gui");
			button.transform.parent = _cam.transform;
			button.transform.position = BottomLeft;
			button.transform.Translate(guiData[currentActive].buttons[i].x,guiData[currentActive].buttons[i].y,0);
			button.AddComponent<SpriteRenderer>().sprite = guiData[currentActive].buttons[i].sprite;

			buttonGameObjects.Add(button);
		}
	}

	public void tick(){

	}
}

