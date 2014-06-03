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
	
	[SerializeField]
	private int buildingWidth;
	[SerializeField]
	private int buildingHeight;
	
	private SpriteRenderer spriterenderer;

	void Start () {
		health = maxHealth;
		spriterenderer = GetComponent<SpriteRenderer>();
		LevelData.buildingList.Add(this);
		LevelData.calculateEnegy();
		transform.position = LevelData.addSizeToPosition(transform.position, getBuildingWidth(), getBuildingHeight());
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
	
	public void onEnergyStateChange(bool state) {
	}

	public int getBuyAmount() {
		return buyAmount;
	}
	
	public int getSellAmount() {
		return sellAmount;
	}
	
	public int getBuildingWidth() {
		return buildingWidth;
	}
	
	public int getBuildingHeight() {
		return buildingHeight;
	}
}
