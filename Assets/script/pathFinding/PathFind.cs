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

	}

	public static Vector2[] FindPath (int[] A,int[] B, GameObject[,] collisionArray){
		int width = collisionArray.GetLength(0);
		int height = collisionArray.GetLength(1);

		List<Node> open;
		List<Node> closed;

		while (false) {
		}
		print ("Start: "+A);

		if (false) {
			return new Vector2[] {new Vector2(9,5)};
		} else {
			return null;
		}
	}


}
