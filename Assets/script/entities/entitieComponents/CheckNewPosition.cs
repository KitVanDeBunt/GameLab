using UnityEngine;
using System.Collections;

public class CheckNewPosition : MonoBehaviour {
	private AnimFramesManager frameManager;
	private void Start(){
		frameManager = gameObject.GetComponent<AnimFramesManager>();
	}
	// Update is called once per frame
	public bool CheckNewPos (VecInt oldPos, VecInt newPos, bool turning = false) {
		int newRotationStateNumber;
		if(oldPos.x < newPos.x){
			if(oldPos.y > newPos.y){
				newRotationStateNumber = 2;
			}else if(oldPos.y < newPos.y){
				newRotationStateNumber = 4;
			}else{
				newRotationStateNumber = 3;
			}
		}else if(oldPos.x > newPos.x){
			if(oldPos.y > newPos.y){
				newRotationStateNumber = 8;
			}else if(oldPos.y < newPos.y){
				newRotationStateNumber = 6;
			}else{
				newRotationStateNumber = 7;
			}
		}else{
			if(oldPos.y > newPos.y){
				newRotationStateNumber = 1;
			}else {
				newRotationStateNumber = 5;
			}
		}
		frameManager.GetComponent<AnimFramesManager>().UpdateRotationAngle(newRotationStateNumber);
		turning = true;
		return turning;
	}
}






//motor gezaai
//goat zila
//zang motor
//nazi groet
//c n g motor genaai
//a  c e  i l   n zang motor
//a  c    l m n o    nazi groet
//  c e    m n n o  r  goat zilla
//a a c e g i l m n n o o r t z
//control magazine

