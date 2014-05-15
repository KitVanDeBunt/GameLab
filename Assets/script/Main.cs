using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	private void FixedUpdate(){
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 mousePosition = new Vector3(mouseRay.origin.x,mouseRay.origin.y,0);
		Vector2 mousePos2D = new Vector2(mouseRay.origin.x,mouseRay.origin.y);
		Vector2 TilePos = IsoMath.worldToTile(mousePos2D.x,mousePos2D.y);
		print ("pos : "+mousePos2D+"\n");
		if (TilePos.x<LevelData.width&&TilePos.x>0&&
		    TilePos.y<LevelData.height&&TilePos.y>0) {
			print ("tile: " + TilePos + "\n");
			LevelData.GroundTiles [(int)Mathf.Floor (TilePos.x), (int)Mathf.Floor (TilePos.y)].GetComponent<SpriteRenderer> ().color = new Color32 (0, 255, 0, 255);
		}
	}
}
