using UnityEngine;
using System.Collections;

public class Unit : MapObject
{
    [SerializeField]
	private PathFollower pathFollower;

    [SerializeField]
    private Selectable selectable;

    private void OnEnable()
    {
        selectable.init(this);
        selectable.OnEnable();
    }

    private void OnDisable()
    {
        selectable.OnDisable();
    }
	public void FollowPath(VecInt[] path){
        
		pathFollower.Init (this);
		pathFollower.SetPath (path);
	}

	void FixedUpdate ()
	{
		if (pathFollower != null) {
			pathFollower.loop ();
		}
	}
}

