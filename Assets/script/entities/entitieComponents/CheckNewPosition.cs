using UnityEngine;
using System.Collections;

public class CheckNewPosition : MonoBehaviour {
	private AnimFramesManager frameManager;
	private void Start(){
		frameManager = gameObject.GetComponent<AnimFramesManager>();
	}
	public int oldRotationStateNumber = 10;
	public int newRotationStateNumber = 1;
	// Update is called once per frame
	public bool CheckNewPos (VecInt oldPos, VecInt newPos) {
		//////for with 8 states
		/*
		if(oldPos.x < newPos.x){
			if(oldPos.y > newPos.y){
				newRotationStateNumber = 8;
			}else if(oldPos.y < newPos.y){
				newRotationStateNumber = 6;
			}else{
				//newRotationStateNumber = 2;
			}
		}else if(oldPos.x > newPos.x){
			if(oldPos.y > newPos.y){
				newRotationStateNumber = ;
			}else if(oldPos.y < newPos.y){
				newRotationStateNumber = 5;
			}else{
				//newRotationStateNumber = 6;
			}
		}else{
			if(oldPos.y > newPos.y){
				//newRotationStateNumber = 8;
			}else {
				//newRotationStateNumber = 4;
			}
		}
		*/
		//////for with 4 states
		if(oldPos.x < newPos.x)
		{
			newRotationStateNumber = 4;

		}else if(oldPos.x > newPos.x)
		{
			newRotationStateNumber = 8;
		}
		if(oldPos.y > newPos.y)
		{
			newRotationStateNumber = 6;
		}else if (oldPos.y < newPos.y)
		{
			newRotationStateNumber = 2;
		}
		frameManager.GetComponent<AnimFramesManager>().UpdateRotationAngle(newRotationStateNumber);
		bool turning = false;
		if(newRotationStateNumber != oldRotationStateNumber){turning = true;}
		oldRotationStateNumber = newRotationStateNumber;
		return turning;
	}
}

