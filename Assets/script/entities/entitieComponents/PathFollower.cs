using UnityEngine;
using System.Collections;

public class PathFollower
{
	private Vector3 pos;
	private VecInt[] currentPath;
	private int pathProgress;
	private Transform trans;
	private float lerp;
	private float zpos;
	private float loopInt = 0;

	internal PathFollower(Transform _trans){
		trans = _trans;
		zpos = trans.position.z;
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
					Vector2 next = IsoMath.tileToWorld( currentPath[pathProgress].x,currentPath[pathProgress].y);
					pathProgress+= 1;
					trans.position = new Vector3(next.x,next.y,zpos);
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

