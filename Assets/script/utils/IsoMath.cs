using UnityEngine;
using System.Collections;

public class IsoMath : MonoBehaviour {

	private static float tileW = 0.90f;
	private static float tileH = 0.50f;

	public static Vector2 tileToWorld(int tx, int ty){
		return new Vector2((ty * tileW/2) + (tx * tileW/2), -((tx * tileH/2) - (ty * tileH/2)));
	}
	
	public static Vector2 worldToTile(float px,float py){
		//float displacementX = 0;
		//float displacementY = 0;
		//px -= displacementX;
		//py -= displacementY;
		
		float tx = 1 / tileW * px - 1 / tileH * py + 0.5f;
		float ty = 1 / tileW * px + 1 / tileH * py - 0.5f +1;
		return new Vector2(tx, ty);
	}

	
	public static Vector2 getMouseWorldPosition(){
		Vector2 returnValue;
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 mousePosition = new Vector3(mouseRay.origin.x,mouseRay.origin.y,0);
		Vector2 mousePos2D = new Vector2(mouseRay.origin.x,mouseRay.origin.y);
		return mousePos2D;
	}
	
	public static Vector2? getMouseTilePosition(){
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

