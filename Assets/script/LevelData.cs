using UnityEngine;
using System.Collections;
public class LevelData : MonoBehaviour{

	[SerializeField]
	private GameObject[] tiles;
	[SerializeField]
	private GameObject[] objects;
	
	private static GameObject[] staticTiles;
	private static GameObject[] staticObjects;

	private static IGenerator generator;

	public static GameObject[,] LoadedGroundTiles;
	public static GameObject[,] GroundVehicles;
	public static int width,height;
	public static int[,] tileData;
	public static int[,] objectData;

	public static int size;

	private static int w,h;

	private void Start(){
		staticTiles = tiles;
		staticObjects = objects;

		generator = new GeneratorTest();

		LoadLevelData ();
	}

	public static void LoadLevelData () {
		size = 50;

		generator.Generate(size);

		width = tileData.GetLength(0);
		height = tileData.GetLength(1);
		objectData = RandomTestData (size,size,new int[]{0,0,0,0,0,0,1});

		BuildTiles (tileData);
		BuildObjects(objectData);
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
		for (h = 0; h < height; h++) {
			for (w = 0; w < width; w++) {
				int objectID = data[w,h];
				if(objectID != 0){
					objectID -= 1;
					Vector2 pos = IsoMath.tileToWorld(w,h);
					GameObject tile = (GameObject)GameObject.Instantiate (staticObjects[objectID], new Vector3 (pos.x, pos.y, pos.x * pos.y / 40f + 5f), new Quaternion ());
					
					GroundVehicles[w,h] = (GameObject)tile;
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
				
				LoadedGroundTiles[w,h] = (GameObject)tile;
			}
		}
	}
}

