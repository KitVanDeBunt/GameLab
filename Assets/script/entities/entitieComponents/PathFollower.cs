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
	private bool turnUnit;
	
	private CheckNewPosition rotater;
	
	private VecInt startPos;
	private VecInt endPos;

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
				
				if(loopInt % 25 == 0){
					if(pathProgress>0){
						turnUnit = rotater.CheckNewPos(oldPos,currentPath[pathProgress]);
					}
				}
				if(loopInt % 50 == 0){
					Vector2 newPos = IsoMath.tileToWorld( currentPath[pathProgress].x,currentPath[pathProgress].y);
					if(pathProgress>0){
						turnUnit = rotater.CheckNewPos(oldPos,currentPath[pathProgress]);
					}
					//trans.position = new Vector3(newPos.x,newPos.y,zpos);
					if(!turnUnit)
					{
						if(checkNextPosFree(currentPath[pathProgress],oldPos)){
							trans.position = new Vector3(newPos.x, newPos.y, newPos.x * newPos.y / 40f + 5f);
						}
					}
					oldPos = currentPath[pathProgress];
					pathProgress+= 1;
				}
			}
			loopInt += 1;
		}
	}
	
	private bool checkNextPosFree(VecInt next,VecInt current){
		//check collision array
		if (LevelData.CollsionData [next.x, next.y]) {
			//set new path
			Debug.Log("newPath!!!!!!!!!!!!!!");
			return false;
		} else {
			//update collision array if free
			//Debug.Log(current.print);
			LevelData.GroundVehicles [next.x, next.y] = LevelData.GroundVehicles [current.x, current.y];
			LevelData.GroundVehicles [current.x, current.y] = null;
			LevelData.objectData [current.x, current.y] = 0;
			LevelData.objectData [next.x, next.y] = 1;
			return true;
		}

	}
	
	internal void SetPath(VecInt[] path){
		startPos = path[0];
		oldPos = startPos;
		endPos = path[path.Length-1];
		currentPath = path;
		pathProgress = 0;
	}
}

