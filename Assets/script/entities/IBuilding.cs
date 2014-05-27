using UnityEngine;
using System.Collections;

public interface IBuilding
{
	bool damage(int amount);
	int getHealth();
	int getEnergyUsage();
}