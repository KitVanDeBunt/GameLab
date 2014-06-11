using UnityEngine;
using System.Collections;

public class Building : MapObject {
	public int buyAmount;
	public int sellAmount;
	public int energy;
	public int maxHealth;
	public int health;

	//public int buildingWidth;
	//public int buildingHeight;
	
	public SpriteRenderer spriterenderer;
	
	public bool damage(int amount) {
		health -= amount;
		if(health < 0)
		{
			return true;
		}
		return false;
	}
	
	public void onEnergyStateChange(bool state) {
		if(state && energy > 0) { spriterenderer.color = Color.white; } else { spriterenderer.color = Color.gray; }
	}

	//======================================
	
	public int getHealth() {
		return health;
	}
	
	public int getEnergyUsage() {
		return energy;
	}
	
	public int getBuyAmount() {
		return buyAmount;
	}
	
	public int getSellAmount() {
		return sellAmount;
	}
	
	public int getBuildingWidth() {
		return size.x;
	}
	
	public int getBuildingHeight() {
		return size.y;
	}
}