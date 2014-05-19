using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	[SerializeField]
	private Camera cam;
	
	private bool mouseDown = false;
	private Vector2 oldPos;
	
	private void Update(){
		if(Input.GetKey(KeyCode.R)){
			UpdateMove ();
		}else{
			UpdateSelect();
		}
	}
	
	private void UpdateSelect(){
		Vector2? TilePosN = getTilePosition();
		if(TilePosN!= null){
			Vector2 TilePos = (Vector2)TilePosN;
			//print ("tile: " + TilePos + "\n");
			if(Input.GetMouseButtonDown(0)){
				GameObject selected = LevelData.GroundVehicles [(int)Mathf.Floor (TilePos.x), (int)Mathf.Floor (TilePos.y)];
				if(selected != null){
					int selectedID = selected.GetInstanceID();
					EventManager.CallOnSelect(selectedID);
				}
			}
		}
	}
	
	
	private void UpdateMove(){
		/*if(Input.touchCount > 1 && Input.touchCount < 3){
			if(Input.TouchPhase ==TouchPhase.Began)
			   }else{
				
			}*/
		Vector2? currentPos = null;
		#if UNITY_PSM || UNITY_ANDROID
		
		#else
		//Debug.Log(mouseDown);
		if(Input.GetMouseButtonUp(0)){
			mouseDown = false;
		}
		if(Input.GetMouseButtonDown(0)||mouseDown){
			currentPos = Input.mousePosition;
			//Debug.Log("click world Pos:"+Input.mousePosition);
			mouseDown = true;
		}
		if(Input.GetMouseButtonDown(0)){
			oldPos = (Vector2)currentPos;
		}
		
		#endif
		if(mouseDown){
			if(currentPos != null && oldPos != null){
				Vector2 deltaPos = (Vector2)currentPos - oldPos;
				deltaPos *= -0.02f;
				//Debug.Log("delta: "+deltaPos);
				Camera.main.transform.Translate(new Vector3(deltaPos.x,deltaPos.y,0));
				oldPos = (Vector2)currentPos;
			}
		}
	}
	
	private Vector2 getMouseWorldPosition(){
		Vector2 returnValue;
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 mousePosition = new Vector3(mouseRay.origin.x,mouseRay.origin.y,0);
		Vector2 mousePos2D = new Vector2(mouseRay.origin.x,mouseRay.origin.y);
		return mousePos2D;
	}

	private Vector2? getTilePosition(){
		Vector2 mousePos2D = getMouseWorldPosition();
		Vector2 TilePos = IsoMath.worldToTile(mousePos2D.x,mousePos2D.y);
		//print ("world pos : "+mousePos2D+"\n");
		if (TilePos.x < LevelData.width && TilePos.x > 0 &&
			TilePos.y < LevelData.height && TilePos.y > 0) {
			//print ("tile pos: " + TilePos + "\n");
			//LevelData.GroundVehicles [(int)Mathf.Floor (TilePos.x), (int)Mathf.Floor (TilePos.y)].GetComponent<SpriteRenderer> ().color = new Color32 (0, 255, 0, 255);
			return TilePos;
		} else {
			return null;
		}
	}
}
