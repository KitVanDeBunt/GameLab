using UnityEngine;
using System.Collections;

public class IsoMAth : MonoBehaviour {

	private static float tileW = 0.69f;
	private static float tileH = 0.41f;

	public static Vector2 tileToWorld(int tx, int ty){
		return new Vector2((ty * tileW/2) + (tx * tileW/2), -((tx * tileH/2) - (ty * tileH/2)));
	}
	
	public static Vector2 worldToTile(float px,float py){
		//float displacementX = 0;
		//float displacementY = 0;
		//px -= displacementX;
		//py -= displacementY;
		
		float tx = 1 / tileW * px - 1 / tileH * py + 0.5f + 1;
		float ty = 1 / tileW * px + 1 / tileH * py - 0.5f + 1;
		return new Vector2(tx, ty);
	}

}

