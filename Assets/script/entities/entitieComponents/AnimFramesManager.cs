using UnityEngine;
using System.Collections;
/*
public enum RotationStates
{
	Up,
	RightUp,
	Right,
	RightDown,
	Down,
	LeftDown,
	Left,
	LeftUp
};*/
public class AnimFramesManager : MonoBehaviour {
	private SpriteRenderer sprite;
	//public static RotationStates spriteState;
	[SerializeField]
	public Sprite Up, RightUp, Right, RightDown, Down, LeftDown, Left, LeftUp;

	private int currentRotation;
	void Start () {
		currentRotation = 2;
		sprite = gameObject.GetComponent<SpriteRenderer>();
		sprite.sprite = RightUp;
		//Debug.Log(rotationStates.Down);
	}
	public void UpdateRotationAngle(int nextRotationState)
	{
        int state = nextRotationState;
		/*StartCoroutine(SetNewState(nextRotationState));
	}

	IEnumerator SetNewState(int state)
	{*/
		bool turn = false;
		//this is for when you start, when you need to turn twice
		
        /*
		if(currentRotation < 4)
		{
			if(state > 5)
			{
				state -= 8;
			}
		}else if (currentRotation > 5){
			if(state < 4){
				state += 8;
			}
		}*/
		
        /*if(state < currentRotation)
		{
			turn = true;
		}else if (state > currentRotation){
			turn = true;
		}else {
			turn = true;
		}*/

		if(currentRotation == 0){currentRotation += 8;}
		else if(currentRotation == 9){currentRotation -= 8;}
		/*if(turn)
		{*/
		if(state == 1){sprite.sprite = Up;}
		else if(state == 2){sprite.sprite = RightUp;}
		else if(state == 3){sprite.sprite = Right;}
		else if(state == 4){sprite.sprite = RightDown;}
		else if(state == 5){sprite.sprite = Down;}
		else if(state == 6){sprite.sprite = LeftDown;}
		else if(state == 7){sprite.sprite = Left;}
		else if(state == 8){sprite.sprite = LeftUp;}
		currentRotation = state;
		/*}else{
			yield return new WaitForSeconds(0.2f);
			StartCoroutine(SetNewState(state));
		}*/
	}

}




