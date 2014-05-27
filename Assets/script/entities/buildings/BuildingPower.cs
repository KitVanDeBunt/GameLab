using UnityEngine;
using System.Collections;

public class BuildingPower : MonoBehaviour, IBuilding {
	private int maxHealth = 100;
	private int health = 100;

	void Start () {
		
	}
	
	void Update () {
		
	}
	
	public int getHealth() {
		return health;
	}
	
	public int getEnergyUsage() {
		return 50;
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
