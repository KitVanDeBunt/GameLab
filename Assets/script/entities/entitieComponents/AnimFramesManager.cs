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
	private Sprite Up, RightUp, Right, RightDown, Down, LeftDown, Left, LeftUp;

	private int currentRotation;
	void Start () {
		currentRotation = 1;
		sprite = gameObject.GetComponent<SpriteRenderer>();
		sprite.sprite = Up;
		//Debug.Log(rotationStates.Down);
	}
	public void UpdateRotationAngle(int nextRotationState)
	{
		StartCoroutine(SetNewState(nextRotationState));
	
	}

	IEnumerator SetNewState(int state)
	{
		bool turn = false;
		if(state < currentRotation)
		{
			turn = true;
			currentRotation += 1;
		}else if (state > currentRotation){
			currentRotation -= 1;
			turn = true;
		}else {
			turn = false;
		}

		if(currentRotation == 0){currentRotation += 8;}
		else if(currentRotation == 9){currentRotation -= 8;}

		if(turn)
		{
			if(currentRotation == 1){sprite.sprite = Up;}
			else if(currentRotation == 2){sprite.sprite = RightUp;}
			else if(currentRotation == 3){sprite.sprite = Right;}
			else if(currentRotation == 4){sprite.sprite = RightDown;}
			else if(currentRotation == 5){sprite.sprite = Down;}
			else if(currentRotation == 6){sprite.sprite = LeftDown;}
			else if(currentRotation == 7){sprite.sprite = Left;}
			else if(currentRotation == 8){sprite.sprite = LeftUp;}
		}else{
			yield return new WaitForSeconds(0.5f);
			StartCoroutine(SetNewState(state));
		}
	}

}




