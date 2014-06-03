using UnityEngine;
using System.Collections;

public class BuildingTurretSmall : MonoBehaviour, IBuilding, ITurret {
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
	private int buildingX;
	[SerializeField]
	private int buildingY;
	[SerializeField]
	private int buildingHeight;
	
	private SpriteRenderer spriterenderer;
	
	void Start () {
		health = maxHealth;
		spriterenderer = GetComponent<SpriteRenderer>();
		LevelData.buildingList.Add(this);
		LevelData.calculateEnegy();
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
	
	public void onEnergyStateChange(bool state) {
		if(state) { spriterenderer.color = Color.white; } else { spriterenderer.color = Color.gray; }
	}

	public int getBuyAmount() {
		return buyAmount;
	}
	
	public int getSellAmount() {
		return sellAmount;
	}
	
	public int getBuildingX() {
		return 0;
	}
	
	public int getBuildingY() {
		return 0;
	}
	
	public int getBuildingHeight() {
		return 0;
	}
}
