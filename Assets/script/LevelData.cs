using UnityEngine;
using System.Collections;

public class MapObject{
	public GameObject gameObject;
	public VecInt pos;
	public MapObject(GameObject _gameObject,VecInt _pos){
		gameObject = _gameObject;
		pos = _pos;
	}
}

public class LevelData : MonoBehaviour{

	[SerializeField]
	private GameObject[] tiles;
	[SerializeField]
	private GameObject[] objects;
	[SerializeField]
	private GameObject[] buildings;
	
	private static GameObject[] staticTiles;
	private static GameObject[] staticObjects;
	private static GameObject[] staticBuildings;

	private static IGenerator generator;

	public static GameObject[,] LoadedGroundTiles;
	public static MapObject[,] GroundVehicles;
	public static int width,height;
	public static int[,] tileData;
	public static int[,] objectData;
	private static bool[,] collsionData;
	
	private static GameObject levelHolder;
	
	public static bool[,] CollsionData{
		get{
			return collsionData;
		}
	}

	public static int size;

	private static int w,h;

	private void Start(){
		levelHolder = new GameObject("LevelHolder");
		staticTiles = tiles;
		staticObjects = objects;
		staticBuildings = buildings;

		generator = new GeneratorTest();

		LoadLevelData ();
	}

	public static void LoadLevelData () {
		size = 50;

		generator.Generate(size);

		width = tileData.GetLength(0);
		height = tileData.GetLength(1);
		collsionData = new bool[size,size];
		objectData = RandomTestData (size,size,new int[]{0,0,0,0,0,0,1});

		BuildTiles (tileData);
		BuildObjects(objectData);
		BuildBuildings();
	}

	private static int[,] RandomTestData(int width,int height, int[] choice){
		int[,] data;
		data = new int[width,height];
		
		for (h = 0; h<height; h++) {
			for (w = 0; w<width; w++) {
				data [w, h] = (int)Random.Range(0,choice.Length);
				data [w, h] = choice[data [w, h]];
			}
		}
		return data;
	}
	
	private static void BuildBuildings(){
		for(int test1 = 0; test1 < 50; test1+=2){
			for(int test2 = 0; test2 < 50; test2+=2){
				int ran = (int)Random.Range(0,5);
				if(ran == 0){
					if(test1 % 4 == 0){
						constructBuilding(test1, test2, 0, 2);
					}else{
						constructBuilding(test1, test2, 1, 2);
					}
				}
			}
		}
	}

	private static bool constructBuilding(int x, int y, int id, int size) {
		for(int i = 0; i < size; i++) {
			for(int j = 0; j < size; j++) {
				if(collsionData[x + i, y + j]) {
					return false;
				}
			}
		}

		for(int k = 0; k < size; k++) {
			for(int l = 0; l < size; l++) {
				collsionData[x + k, y + l] = true;
			}
		}

		Vector2 pos = IsoMath.tileToWorld(x - 1 + (size / 2), y + (size / 2));
		GameObject building = (GameObject)GameObject.Instantiate (staticBuildings[id], new Vector3 (pos.x, pos.y, (pos.x - 1) * pos.y / 40f + 5f), new Quaternion());
		building.transform.parent = levelHolder.transform;
		return true;
	}
	
	private static void BuildObjects (int[,] data) {
		Debug.Log ("width: "+width+" height: "+height);
		GroundVehicles = new MapObject[width,height];
		for (h = 0; h < height; h++) {
			for (w = 0; w < width; w++) {
				int objectID = data[w,h];
				if(objectID != 0){
					objectID -= 1;
					Vector2 pos = IsoMath.tileToWorld(w,h);
					GameObject tile = (GameObject)GameObject.Instantiate (staticObjects[objectID], new Vector3 (pos.x, pos.y, pos.x * pos.y / 40f + 5f), new Quaternion ());
					tile.transform.parent = levelHolder.transform;
					GroundVehicles[w,h] = new MapObject(tile,new VecInt(w,h));
					collsionData[w,h] = true;
				}
			}
		}
	}

	private static void BuildTiles (int[,] data) {
		Debug.Log ("width: "+width+" height: "+height);
		LoadedGroundTiles = new GameObject[width,height];
		for (h = 0; h < height; h++) {
			for (w = 0; w < width; w++) {
				Vector2 pos = IsoMath.tileToWorld(w,h);
				GameObject tile = (GameObject)GameObject.Instantiate (staticTiles[data[w,h]], new Vector3 (pos.x, pos.y, pos.x * pos.y / 40f + 10f), new Quaternion ());
				tile.transform.parent = levelHolder.transform;
				LoadedGroundTiles[w,h] = (GameObject)tile;
			}
		}
	}
}

