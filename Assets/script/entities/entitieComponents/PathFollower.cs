using UnityEngine;
using System.Collections;

public class PathFollower
{
	private Vector3 pos;
	private VecInt[] currentPath;
	private int pathProgress;
	private Transform trans;
	private float lerp;
	private float loopInt = 0;
	private VecInt oldPos;
	
	private CheckNewPosition rotater;

	internal PathFollower(Transform _trans){
		trans = _trans;
		rotater = trans.gameObject.GetComponent<CheckNewPosition>();
		Init ();
	}

	internal void Init(){
		pos = trans.position;
	}
	
	internal void loop(){
		//transform.position = Vector3.Lerp(
		//trans.Translate(0.05f,0.05f,0);
		if (currentPath != null) {
			if(pathProgress < currentPath.Length){
				if(loopInt % 50 == 0){
					Vector2 newPos = IsoMath.tileToWorld( currentPath[pathProgress].x,currentPath[pathProgress].y);
					if(pathProgress>0){
						rotater.CheckNewPos(oldPos,currentPath[pathProgress],true);
					}
					//trans.position = new Vector3(newPos.x,newPos.y,zpos);
					trans.position = new Vector3(newPos.x, newPos.y, newPos.x * newPos.y / 40f + 5f);
					oldPos = currentPath[pathProgress];
					pathProgress+= 1;
				}
			}
			loopInt += 1;
		}
	}
	
	internal void SetPath(VecInt[] path){
		currentPath = path;
		pathProgress = 0;
	}
}

