using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {

	[SerializeField]
	private GameObject selectIcon;

	void OnEnable()
	{
		EventManager.OnSelect += Select;
	}
	
	
	void OnDisable()
	{
		EventManager.OnSelect -= Select;
	}
	
	private void Select(int[] id){
		if (id.Length > 0) {
			for (int i = 0; i <id.Length; i++) {
				if (gameObject.GetInstanceID () == id [i]) {
					selectIcon.SetActive (true);
					break;
				} else {
					selectIcon.SetActive (false);
				}
			}
		}else{
			selectIcon.SetActive (false);
		}
	}
}
