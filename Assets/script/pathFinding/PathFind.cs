using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct Node
{
	public float G; // movement from start
	public float H; // estimated Cost til end
	public float F; // cost
	public bool back;
}

public class PathFind : MonoBehaviour {
	private void Start(){
		FindPath (new int[]{0,0}, new int[]{10,10}, LevelData.GroundVehicles);
	}

	public static Vector2[] FindPath (int[] A,int[] B, GameObject[,] collisionArray){
		int width = collisionArray.GetLength(0);
		int height = collisionArray.GetLength(1);

		List<Node> open;
		List<Node> closed;

		while (false) {
		}


		if (false) {
			return new Vector2[] {new Vector2(9,5)};
		} else {
			return null;
		}
	}


}
