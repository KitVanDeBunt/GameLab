using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour {
	[SerializeField]
	private GameObject[] tiles;
	private int[,] tileData;
	private int w,h;

	void Start () {
		tileData = new int[,]{ 
			{ 1, 2, 0, 1, 2, 0, 1, 2, 1, 2},
			{ 1, 2, 0, 1, 2, 0, 1, 2, 1, 2},
			{ 1, 2, 0, 1, 2, 0, 1, 2, 1, 2},
			{ 1, 2, 0, 1, 2, 0, 1, 2, 1, 2},
			{ 1, 2, 0, 1, 2, 0, 1, 2, 1, 2},
			{ 1, 2, 0, 1, 2, 0, 1, 2, 1, 2},
			{ 1, 2, 0, 1, 2, 0, 1, 2, 1, 2},
			{ 1, 2, 0, 1, 2, 0, 1, 2, 1, 2},
			{ 1, 2, 0, 1, 2, 0, 1, 2, 1, 2},
			{ 1, 2, 0, 1, 2, 0, 1, 2, 1, 2}
		};;


		Build(tileData);
	}
	
	private void Build (int[,] data) {
		int width = data.GetLength(1);
		int height = data.GetLength(0);

		print ("w: "+width+" height: "+height);
		tileData = new int[width, height];

		int isoDisplace = 0;
		for (h = 0; h<height; h++) {
			if(h%2 == 0){ 
				isoDisplace+=1;
			}
			for (w = 0; w<width; w++) {
				if(h%2 == 0){ 
					GameObject.Instantiate (tiles[data[h,w]], new Vector3 (w*0.69f-(-0.69f*isoDisplace), h*0.205f, 0), new Quaternion ());
				}else{
					GameObject.Instantiate (tiles[data[h,w]], new Vector3 (w*0.69f+0.345f-(-0.69f*isoDisplace), h*0.205f, 0), new Quaternion ());
				}
			}
		}
	}
}
