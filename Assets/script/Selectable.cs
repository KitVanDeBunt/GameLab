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
	
	private void Select(int id){
		if(gameObject.GetInstanceID() == id){
			selectIcon.SetActive(true);
		}else{
			selectIcon.SetActive(false);
		}
	}
}
