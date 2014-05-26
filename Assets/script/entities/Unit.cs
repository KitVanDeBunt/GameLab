using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
	PathFollower pathFollower;	
		
	public void FollowPath(VecInt[] path){
		pathFollower = new PathFollower (transform);
		pathFollower.SetPath (path);
	}

	void FixedUpdate ()
	{
		if (pathFollower != null) {
			pathFollower.loop ();
		}
	}
}
