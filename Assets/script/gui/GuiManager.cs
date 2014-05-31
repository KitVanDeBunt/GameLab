using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GuiManager
{
	[HideInInspector]
	public int currentActive = 0;

	[SerializeField]
	public GuiData[] guiData;
	
	[HideInInspector]
	[SerializeField]
	private List<GameObject> buttonGameObjects;
	
	[SerializeField]
	private int guiint;
	[SerializeField]
	private Camera guiCam;
	
	float screenHeight;
	float screenWidth;
	Vector3 TopRight;
	Vector3 BottomLeft;
	
	
	
	public void init(){
		screenHeight = 2f * guiCam.orthographicSize;
		screenWidth = screenHeight * guiCam.aspect;
		BuildGui();
	}

	public void BuildGui(){
		guiint++;
		ClearGui();
		
		buttonGameObjects = new List<GameObject>();
		//Debug.Log ("[GUI] "+guiData.Length);
		//Debug.Log ("[GUI]buttons: "+guiData[currentActive].buttons.Length);

		
		TopRight = new Vector3( screenWidth/2, screenHeight/2,10)+guiCam.transform.position;
		BottomLeft = new Vector3( -screenWidth/2, -screenHeight/2,10)+guiCam.transform.position;

		for (int i = 0; i < guiData[currentActive].buttons.Length; i++) {
			string name = "guibutton "+i.ToString()+" "+guiint;
			GameObject button = new GameObject(name);
			button.layer = LayerMask.NameToLayer("Gui");
			button.transform.parent = guiCam.transform;
			button.transform.position = new Vector3(guiData[currentActive].buttons[i].x/100.0f+BottomLeft.x
			                                        ,guiData[currentActive].buttons[i].y/100.0f+BottomLeft.y,0);
			button.AddComponent<SpriteRenderer>().sprite = guiData[currentActive].buttons[i].sprite;
			button.tag = "Button";

			buttonGameObjects.Add(button);
		}

		for (int i = 0; i < buttonGameObjects.Count; i++) {
			//Debug.Log("[GUI]::::::::"+buttonGameObjects[i].GetComponent<SpriteRenderer>().sprite.bounds);
		}
	}
	
	private void ClearGui(){
		GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
		//for(int i = 0; i < buttonGameObjects.Count;i++){
		//	GameObject.DestroyImmediate(buttonGameObjects[i]);
		//	//Debug.Log("Destroy: "+i);
		//}
		for(int i = 0; i < buttons.Length;i++){
			GameObject.DestroyImmediate(buttons[i]);
			//Debug.Log("Destroy: "+i);
		}
	}
	
	public bool checkGuiInput(){
		bool overGui = false;
		Vector2 mousePos = IsoMath.getMouseWorldPosition();
		//Debug.Log (mousePos);
		for(int i = 0; i < buttonGameObjects.Count;i++){
			if(guiData[currentActive].buttons[i].isButton){
				Bounds buttonBuonds = buttonGameObjects[i].renderer.bounds;
				//Debug.Log("buttonBuonds: "+buttonBuonds);
				//Debug.Log("message: "+guiData[currentActive].buttons[i].sprite.textureRect);
				if((buttonBuonds.center.x+buttonBuonds.extents.x)>mousePos.x&&
				   (buttonBuonds.center.x-buttonBuonds.extents.x)<mousePos.x&&
				   (buttonBuonds.center.y+buttonBuonds.extents.y)>mousePos.y&&
				   (buttonBuonds.center.y-buttonBuonds.extents.y)<mousePos.y){
					overGui = true;
					if(Input.GetMouseButtonDown(0)){
						EventManager.callOnGuiInput(guiData[currentActive].buttons[i].message);
					}
				}else{
					
				}
			}
		}
		return overGui;
	}

	public void tick(){
		Vector3 TopRight = new Vector3( screenWidth/2, screenHeight/2,10)+guiCam.transform.position;
		Vector3 BottomLeft = new Vector3( -screenWidth/2, -screenHeight/2,10)+guiCam.transform.position;
		
		//Debug.Log("[GUI] tick "+buttonGameObjects.Count);
		for (int i = 0; i < buttonGameObjects.Count; i++) {
			buttonGameObjects[i].transform.position = new Vector3( guiData[currentActive].buttons[i].x/100.0f+BottomLeft.x
			                                                      ,guiData[currentActive].buttons[i].y/100.0f+BottomLeft.y,0);
		}
	}
}

