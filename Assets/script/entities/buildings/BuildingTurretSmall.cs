using UnityEngine;
using System.Collections;

public class BuildingTurretSmall : MonoBehaviour, IBuilding, ITurret {
	private int maxHealth = 120;
	private int health = 120;
	
	void Start () {
		
	}
	
	void Update () {
		if(LevelData.ENERGY) {

		}
	}
	
	public int getHealth() {
		return health;
	}
	
	public int getEnergyUsage() {
		return 5;
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
