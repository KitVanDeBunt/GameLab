using UnityEngine;
using System.Collections;

public class BuildingTurretSmall : MonoBehaviour, IBuilding, ITurret {
	[SerializeField]
	private int energy;
	[SerializeField]
	private int maxHealth;
	private int health;
	
	void Start () {
		health = maxHealth;
	}
	
	void Update () {
		if(LevelData.ENERGY) {

		}
	}
	
	public int getHealth() {
		return health;
	}
	
	public int getEnergyUsage() {
		return energy;
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
