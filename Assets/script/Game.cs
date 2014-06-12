using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum InputState{
	normal,
	multiselect,
	buildingPlace
}
public class Game : MonoBehaviour {

	[SerializeField]
	private Camera cam;
	[SerializeField]
	private GameObject multiSelectArea;

	[SerializeField]
	private GameObject preBuildImage;

	private bool mouseDown = false;
	private Vector2 oldPos;
	private int[] selectedIDs;
	private List<MapObject> selected = new List<MapObject>();
	private int[] location;
	//private bool multiselect;
	private Vector3 startMousePos;
	private VecInt startTilePos;
	private bool releasingPreBuildImage;
	private InputState state;

	private static Game _instance;

	public static Game instance{
		get{
			return _instance;
		}
	}
	private void Awake(){
		_instance = this;
		state = InputState.normal;
	}
	void OnEnable(){
		EventManager.OnGuiInput += GuiInput;
	}
	void OnDisable(){
		EventManager.OnGuiInput -= GuiInput;
	}
	private void GuiInput(string message){
		Debug.Log("[Game]: Event Message: "+message);
		if (message == "Button1") {
			Debug.Log("clicked the first time");
			fullPreBuildImage(LevelData.staticBuildings[0]);
			Debug.Log(preBuildImage);
			StartCoroutine(PreBuildImageFollowCursor());
			releasingPreBuildImage = true;
		}
	}
	public bool fullPreBuildImage(GameObject buildingSprite){
		preBuildImage.GetComponent<SpriteRenderer>().sprite = buildingSprite.GetComponent<SpriteRenderer>().sprite;
		Color color = preBuildImage.renderer.material.color;
		color.a = 0.5f;
		preBuildImage.renderer.material.color = color;
		return true;
	}
	private IEnumerator PreBuildImageFollowCursor(){
		Vector2 currentMousePos = IsoMath.getMouseWorldPosition();
		if(Input.GetMouseButtonDown(0) && releasingPreBuildImage)
		{
			Vector2 hello = IsoMath.worldToTile(currentMousePos.x,currentMousePos.y);
			VecInt hello2 = new VecInt((int)hello.x,(int)hello.y);
			LevelData.constructBuilding(hello2.x,hello2.y,0,2);
			Color color = preBuildImage.renderer.material.color;
			color.a = 0;
			preBuildImage.renderer.material.color = color;
			//Debug.Log(releasingPreBuildImage);
			releasingPreBuildImage = false;
			//Debug.Log(releasingPreBuildImage);
		}else{
			preBuildImage.transform.position = new Vector3(currentMousePos.x-0.50f, currentMousePos.y+0.25f, 0);
			yield return new WaitForEndOfFrame();
			StartCoroutine(PreBuildImageFollowCursor());
		}

		yield return 2;
	}
	public void UpdateSelect(){
		Vector2? TilePosN = IsoMath.getMouseTilePosition();
		if (state == InputState.multiselect) {
			Vector2 currentMousePos = IsoMath.getMouseWorldPosition();
			float areaWidth = currentMousePos.x-startMousePos.x;
			float areaHeight = currentMousePos.y-startMousePos.y;
			multiSelectArea.transform.localScale = new Vector3(areaWidth,areaHeight,1);
			if (Input.GetMouseButtonUp (0)) {
				VecInt[] selectedArea = IsoMath.Area(startTilePos,areaWidth,areaHeight,new Rect?(new Rect(0,0,LevelData.width,LevelData.height)));
				List<int> tempSelectedIds = new List<int>(); 
				selected.Clear();
				for(int i = 0;i < selectedArea.Length;i++){
					//Debug.Log("selected: ("+selectedArea[i].x+","+selectedArea[i].y+")");
					//LevelData.LoadedGroundTiles[selectedArea[i].x,selectedArea[i].y].GetComponent<SpriteRenderer>().color = new Color(0,0,1,1);
                    MapObject Tempselected = LevelData.GetMapObjects((int)selectedArea[i].x, (int)selectedArea[i].y);//LevelData.mapObjects [(int)selectedArea[i].x, (int)selectedArea[i].y];
					if(Tempselected != null){
						tempSelectedIds.Add(Tempselected.gameObject.GetInstanceID());
						selected.Add(Tempselected);
					}
				}
				selectedIDs = tempSelectedIds.ToArray();
				EventManager.CallOnSelect(selectedIDs);
				state = InputState.normal;
				multiSelectArea.SetActive(false);
			}
		}else{
			if(TilePosN!= null){
				Vector2 TilePos = (Vector2)TilePosN;
				//print ("tile: " + TilePos + "\n");
				//multi select
				if(Input.GetMouseButtonDown(0)&&(Input.GetKey(KeyCode.RightControl)||Input.GetKey(KeyCode.LeftControl))){
					StartMultiSelect();
					startTilePos = new VecInt((int)TilePos.x,(int)TilePos.y);
					Debug.Log("[Main] Multiselect: "+startTilePos);//+selectedIDs.Length);
				}else if(Input.GetMouseButtonDown(0)){
				//single select
					selected.Clear();
					//MapObject Tempselected = LevelData.mapObjects [(int)TilePos.x, (int)TilePos.y];
                    MapObject Tempselected = LevelData.GetMapObjects((int)TilePos.x, (int)TilePos.y);
                    if(Tempselected != null){
                        selected.Add(Tempselected);
						selectedIDs = new int[]{selected[0].gameObject.GetInstanceID()};
						Debug.Log("[Main] selected: "+selectedIDs.Length);
					}else if(releasingPreBuildImage){
						StartCoroutine(PreBuildImageFollowCursor());
					}else{
						selectedIDs = new int[]{};
						Debug.Log("[Main] not selected: "+selectedIDs.Length);
					}
					EventManager.CallOnSelect(selectedIDs);
				}else if(Input.GetMouseButtonDown(1)){
					if(selectedIDs.Length > 0){
						FindNewPath();
					}
				}
			}
		}
	}
	private void FindNewPath(){
		Vector2 TilePos = (Vector2)IsoMath.getMouseTilePosition();
		print ("[Main] find path");
		for (int i = 0;i<selected.Count;i++){
			VecInt[] newPath = PathFind.FindPath (
				new VecInt(selected[i].pos.x,selected[i].pos.y)
				, new VecInt((int)TilePos.x,(int)TilePos.y)
				, LevelData.CollsionData
                , true);
			if(newPath != null){
				selected[i].gameObject.GetComponent<Unit>().FollowPath(newPath);
			}
		}
	}
	private void StartMultiSelect(){
		state = InputState.multiselect;
		multiSelectArea.SetActive(true);
		startMousePos = IsoMath.getMouseWorldPosition3D();
		multiSelectArea.transform.position = startMousePos;
		multiSelectArea.transform.localScale = Vector3.zero;
	}
	public void UpdateMove(){
		Vector2? currentPos = null;
		#if UNITY_PSM || UNITY_ANDROID

		if(Input.GetAxis("RightStickXAxis")>0 || Input.GetAxis("RightStickYAxis")>0 )
		{
			if(currentPos != null && oldPos != null){
				Vector2 deltaPos = (Vector2)new Vector2(Input.GetAxis("RightStickXAxis")*2, Input.GetAxis("RigthStickYAxis")*2);
				Camera.main.transform.Translate(new Vector3(deltaPos.x,deltaPos.y,0));
				oldPos = (Vector2)currentPos;
			}
		}
		#else
		//Debug.Log(mouseDown);
		if(Input.GetMouseButtonUp(0)){
			mouseDown = false;
		}
		if(Input.GetMouseButtonDown(0)||mouseDown){
			currentPos = Input.mousePosition;
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
				Camera.main.transform.Translate(new Vector3(deltaPos.x,deltaPos.y,0));
				oldPos = (Vector2)currentPos;
			}
		}
	}
}
