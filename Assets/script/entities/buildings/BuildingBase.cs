using UnityEngine;
using System.Collections;

public class BuildingBase : MonoBehaviour, IBuilding {
	private int maxHealth = 200;
	private int health = 200;

	void Start () {
	
	}

	void Update () {
	
	}

	public int getHealth() {
		return health;
	}

	public int getEnergyUsage() {
		return 0;
	}

	public bool damage(int amount) {
		health -= amount;
		if(health < 0)
		{
			return true;
		}
		return false;
	}
}
