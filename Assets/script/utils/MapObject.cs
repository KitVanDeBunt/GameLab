using UnityEngine;

public class MapObject{
	public GameObject gameObject;
	public VecInt pos;
	public MapObject(GameObject _gameObject,VecInt _pos){
		gameObject = _gameObject;
		pos = _pos;
	}
}
