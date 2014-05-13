using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour {
	
	[SerializeField]
	private GameObject[] tiles;
	private int[,] tileData;
	private GameObject[,] GroundTiles;
	private int w,h;
	
	void Start () {
		tileData = new int[4,10]{ 
			{ 1, 0, 0, 2, 0, 0, 0, 0, 1, 1},
			{ 1, 2, 0, 1, 2, 2, 2, 2, 2, 2},

			{ 1, 0, 0, 0, 0, 1, 1, 1, 1, 1},
			{ 1, 2, 0, 1, 2, 2, 2, 2, 2, 2}
		};
		Debug.Log (tileData[0,7]);
		tileData = RandomTestData (10,10);

		Build(tileData);
	}
	
	private void Build (int[,] data) {
		int width = data.GetLength(0);
		int height = data.GetLength(1);

		print ("width: "+width+" height: "+height);
		tileData = new int[width, height];
		GroundTiles = new GameObject[width,height];
		int XisoDisplace = 0;
		for (h = 0; h<height; h++) {
			if(h%2 == 0){ 
				XisoDisplace+=1;
			}
			for (w = 0; w<width; w++) {
				Vector2 pos = IsoMAth.tileToWorld(w,h);
				GameObject tile = (GameObject)GameObject.Instantiate (tiles[data[w,h]], new Vector3 (pos.x, pos.y,0), new Quaternion ());

				GroundTiles[w,h] = (GameObject)tile;
			}
		}
	}

	private void FixedUpdate(){
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 mousePosition = new Vector3(mouseRay.origin.x,mouseRay.origin.y,0);
		Vector2 mousePos2D = new Vector2(mouseRay.origin.x,mouseRay.origin.y);
		Vector2 TilePos = IsoMAth.worldToTile(mousePos2D.x,mousePos2D.y);
		print ("pos : "+mousePos2D+"\n");
		if (TilePos.x<10&&TilePos.x>0&&
		    TilePos.y<10&&TilePos.y>0) {
			print ("tile: " + TilePos + "\n");
			GroundTiles [(int)Mathf.Floor (TilePos.x), (int)Mathf.Floor (TilePos.y)].GetComponent<SpriteRenderer> ().color = new Color32 (0, 255, 0, 255);
		}
	}
	
	private int[,] RandomTestData(int width,int height){
		int[,] data;
		data = new int[width,height];
		
		for (h = 0; h<height; h++) {
			for (w = 0; w<width; w++) {
				data [w, h] = (int)Random.Range(0,3);
			}
		}
		return data;
	}
}
