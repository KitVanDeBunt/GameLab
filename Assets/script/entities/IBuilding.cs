using UnityEngine;
using System.Collections;

public interface IBuilding
{
	bool damage(int amount);
	int getHealth();
	int getEnergyUsage();
	void onEnergyStateChange(bool state);

	int getBuyAmount();
	int getSellAmount();

	int getBuildingWidth();
	int getBuildingHeight();
}