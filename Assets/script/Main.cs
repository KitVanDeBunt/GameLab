using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	[SerializeField]
	private Camera cam;
	
	private bool mouseDown = false;
	private Vector2 oldPos;
	private int[] selectedIDs;
	private int[] location;
	private bool multiselect;

	private void Start(){
		GameInput.init(this);
	}

	private void Update(){
		GameInput.updateInput();
	}
	
	public void UpdateSelect(){
		Vector2? TilePosN = IsoMath.getMouseTilePosition();
		if(TilePosN!= null){
			Vector2 TilePos = (Vector2)TilePosN;
			//print ("tile: " + TilePos + "\n");
			if(Input.GetMouseButtonDown(0)&&Input.GetKeyDown(KeyCode.RightControl)){
				multiselect = true;
			}else if(Input.GetMouseButtonDown(0)){
				GameObject selected = LevelData.GroundVehicles [(int)Mathf.Floor (TilePos.x), (int)Mathf.Floor (TilePos.y)];
				if(selected != null){
					selectedIDs = new int[]{selected.GetInstanceID()};
					Debug.Log("selected: "+selectedIDs.Length);
					EventManager.CallOnSelect(selectedIDs);
				}else{
					selectedIDs = new int[]{};
					Debug.Log("not selected: "+selectedIDs.Length);
					EventManager.CallOnSelect(selectedIDs);
				}
			}else if(Input.GetMouseButtonDown(1)){
				print ("mouse1");
				if(selectedIDs.Length > 0){
					PathFind.FindPath (
					new int[]{(int)Mathf.Floor (TilePos.x),(int)Mathf.Floor (TilePos.y)}
					, new int[]{10,10}
					, LevelData.GroundVehicles);
				}
			}
		}
	}
	
	
	public void UpdateMove(){
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

}
