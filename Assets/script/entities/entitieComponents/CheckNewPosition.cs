using UnityEngine;
using System.Collections;

public class CheckNewPosition : MonoBehaviour {
    private AnimFramesManager frameManager;
	private void Start(){
		frameManager = gameObject.GetComponent<AnimFramesManager>();
	}
	private int oldRotationStateNumber = 10;
    private int newRotationStateNumber = 1;
	// Update is called once per frame
	public bool CheckNewPos (VecInt oldPos, VecInt newPos,float delay) {
		//////for with 8 states

		if(oldPos.x < newPos.x){
			if(oldPos.y > newPos.y){
				newRotationStateNumber = 5;
			}else if(oldPos.y < newPos.y){
				newRotationStateNumber = 3;
			}else{
				newRotationStateNumber = 4;
			}
		}else if(oldPos.x > newPos.x){
			if(oldPos.y > newPos.y){
				newRotationStateNumber = 7;
			}else if(oldPos.y < newPos.y){
				newRotationStateNumber = 1;
			}else{
				newRotationStateNumber = 8;
			}
		}else{
			if(oldPos.y > newPos.y){
				newRotationStateNumber = 6;
			}else {
				newRotationStateNumber = 2;
			}
		}
		/*
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
		}*/
		frameManager.GetComponent<AnimFramesManager>().UpdateRotationAngle(newRotationStateNumber);
		bool turning = false;
		//if you need to turn and wait with moving, you need to enable this if statement
		if(newRotationStateNumber != oldRotationStateNumber){turning = true;}
		
        StartCoroutine(Rotate(delay));
		return turning;
	}

    IEnumerator Rotate(float delay)
    {
        yield return new WaitForSeconds(delay);
        oldRotationStateNumber = newRotationStateNumber;
    }
}

