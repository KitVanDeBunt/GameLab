using UnityEngine;
using System.Collections;
public class LevelData : MonoBehaviour{

	[SerializeField]
	private GameObject[] tiles;
	[SerializeField]
	private GameObject[] objects;
	
	private static GameObject[] staticTiles;
	private static GameObject[] staticObjects;

	internal static GameObject[,] LoadedGroundTiles;
	internal static GameObject[,] GroundVehicles;
	internal static int width,height;
	internal static int[,] tileData;
	internal static int[,] objectData;

	private static int w,h;

	private void Start(){
		staticTiles = tiles;
		staticObjects = objects;
		LoadLevelData ();
	}

	public static void LoadLevelData ()
	{
		LoadTest ();
		BuildTiles (tileData);
		BuildObjects(objectData);
	}

	private static void LoadTest ()
	{
		tileData = new int[4,10]{ 
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 2, 0, 1, 2, 2, 2, 2, 2, 1},
			
			{ 1, 0, 0, 0, 0, 1, 1, 1, 1, 1},
			{ 1, 2, 2, 1, 2, 2, 2, 2, 2, 1}
		};
		//Debug.Log (tileData[0,7]);
		tileData = RandomTestData (20,20,new int[]{0,1,2});
		width = tileData.GetLength(0);
		height = tileData.GetLength(1);
		
		objectData = RandomTestData (20,20,new int[]{0,0,0,0,0,0,1});
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
	
	private static void BuildObjects (int[,] data) {
		Debug.Log ("width: "+width+" height: "+height);
		GroundVehicles = new GameObject[width,height];
		for (h = 0; h<height; h++) {
			for (w = 0; w<width; w++) {
				int objectID = data[w,h];
				if(objectID != 0){
					objectID -= 1;
					Vector2 pos = IsoMath.tileToWorld(w,h);
					GameObject tile = (GameObject)GameObject.Instantiate (staticObjects[objectID], new Vector3 (pos.x, pos.y,0), new Quaternion ());
					
					GroundVehicles[w,h] = (GameObject)tile;
				}
			}
		}
	}

	private static void BuildTiles (int[,] data) {
		Debug.Log ("width: "+width+" height: "+height);
		LoadedGroundTiles = new GameObject[width,height];
		for (h = 0; h<height; h++) {
			for (w = 0; w<width; w++) {
				Vector2 pos = IsoMath.tileToWorld(w,h);
				GameObject tile = (GameObject)GameObject.Instantiate (staticTiles[data[w,h]], new Vector3 (pos.x, pos.y,0), new Quaternion ());
				
				LoadedGroundTiles[w,h] = (GameObject)tile;
			}
		}
	}
}

