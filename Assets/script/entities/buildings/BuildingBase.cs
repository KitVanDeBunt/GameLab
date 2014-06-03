using UnityEngine;
using System.Collections;

public class BuildingBase : MonoBehaviour, IBuilding {
	[SerializeField]
	private int buyAmount;
	[SerializeField]
	private int sellAmount;
	[SerializeField]
	private int energy;
	[SerializeField]
	private int maxHealth;
	private int health;

	void Start () {
		health = maxHealth;
	}

	void Update () {
	
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
	
	public int getBuyAmount() {
		return buyAmount;
	}
	
	public int getSellAmount() {
		return sellAmount;
	}
}
