using UnityEngine;
using System.Collections;
public class LevelData : MonoBehaviour{

	[SerializeField]
	private GameObject[] tiles;

	private static GameObject[] staticTiles;

	internal static GameObject[,] GroundTiles;
	internal static int width,height;
	internal static int[,] tileData;

	private static int w,h;

	private void Start(){
		staticTiles = tiles;
		LoadLevelData ();
	}

	public static void LoadLevelData ()
	{
		LoadTest ();
		Build (tileData);
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
		tileData = RandomTestData (20,20);
		width = tileData.GetLength(0);
		height = tileData.GetLength(1);
	}

	private static int[,] RandomTestData(int width,int height){
		int[,] data;
		data = new int[width,height];
		
		for (h = 0; h<height; h++) {
			for (w = 0; w<width; w++) {
				data [w, h] = (int)Random.Range(0,3);
			}
		}
		return data;
	}

	private static void Build (int[,] data) {
		Debug.Log ("width: "+LevelData.width+" height: "+LevelData.height);
		LevelData.tileData = new int[LevelData.width, LevelData.height];
		GroundTiles = new GameObject[LevelData.width,LevelData.height];
		int XisoDisplace = 0;
		for (h = 0; h<LevelData.height; h++) {
			if(h%2 == 0){ 
				XisoDisplace+=1;
			}
			for (w = 0; w<LevelData.width; w++) {
				Vector2 pos = IsoMath.tileToWorld(w,h);
				GameObject tile = (GameObject)GameObject.Instantiate (staticTiles[data[w,h]], new Vector3 (pos.x, pos.y,0), new Quaternion ());
				
				GroundTiles[w,h] = (GameObject)tile;
			}
		}
	}
}

