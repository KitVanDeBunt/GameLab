using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	[SerializeField]
	private Camera cam;
	[SerializeField]
	private GameObject multiSelectArea;
	
	private bool mouseDown = false;
	private Vector2 oldPos;
	private int[] selectedIDs;
	private int[] location;
	private bool multiselect;
	private Vector3 startMousePos;
	private VecInt startTilePos;

	private void Start(){
		GameInput.init(this);
		multiSelectArea.SetActive(false);
	}

	private void Update(){
		GameInput.updateInput();
	}
	
	public void UpdateSelect(){
		Vector2? TilePosN = IsoMath.getMouseTilePosition();
		if (multiselect) {
			Vector2 currentMousePos = IsoMath.getMouseWorldPosition();
			float areaWidth = currentMousePos.x-startMousePos.x;
			float areaHeight = currentMousePos.y-startMousePos.y;
			//Debug.Log("area("+areaWidth+","+areaHeight+")");
			multiSelectArea.transform.localScale = new Vector3(areaWidth,areaHeight,1);
			if (Input.GetMouseButtonUp (0)) {
				VecInt[] selectedArea = IsoMath.Area(startTilePos,areaWidth,areaHeight,new Rect?(new Rect(0,0,LevelData.width,LevelData.height)));
				List<int> tempSelectedIds = new List<int>(); 
				for(int i = 0;i < selectedArea.Length;i++){
					//Debug.Log("selected: ("+selectedArea[i].x+","+selectedArea[i].y+")");
					LevelData.LoadedGroundTiles[selectedArea[i].x,selectedArea[i].y].GetComponent<SpriteRenderer>().color = new Color(0,0,1,1);
					GameObject selected = LevelData.GroundVehicles [(int)Mathf.Floor (selectedArea[i].x), (int)Mathf.Floor (selectedArea[i].y)];
					if(selected != null){
						tempSelectedIds.Add(selected.GetInstanceID());
					}
				}
				selectedIDs = tempSelectedIds.ToArray();
				Debug.Log("selected: "+selectedIDs.Length);
				EventManager.CallOnSelect(selectedIDs);
				multiselect = false;
				multiSelectArea.SetActive(false);
			}
		}else{
			if(TilePosN!= null){
				Vector2 TilePos = (Vector2)TilePosN;
				//print ("tile: " + TilePos + "\n");
				//multi select
				if(Input.GetMouseButtonDown(0)&&(Input.GetKey(KeyCode.RightControl)||Input.GetKey(KeyCode.LeftControl))){
					multiselect = true;
					multiSelectArea.SetActive(true);
					startMousePos = IsoMath.getMouseWorldPosition3D();
					multiSelectArea.transform.position = startMousePos;
					multiSelectArea.transform.localScale = Vector3.zero;
					startTilePos = new VecInt((int)TilePos.x,(int)TilePos.y);
					Debug.Log("Multiselect: "+startTilePos);//+selectedIDs.Length);
				}else if(Input.GetMouseButtonDown(0)){
				//single select
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
