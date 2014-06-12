using UnityEngine;

public class EnergyManager
{
    private static int energyLevel;
    private static bool pENERGY = true;

    public static bool ENERGY{
        get
        {
            return pENERGY;
        }
    }

    public static void calculateEnegy()
    {
        energyLevel = 0;

        Building[] buildings = LevelData.GetAllBuildings();
        int buildingLength = buildings.Length;
        for (int i = 0; i < buildingLength; i++)
        {
            energyLevel += buildings[i].getEnergyUsage();
        }

        if (energyLevel > -1 && ENERGY)
        {
            pENERGY = true;
            onEnergyStateChange();
        }
        else if (energyLevel < 0 && !ENERGY)
        {
            pENERGY = false;
            onEnergyStateChange();
        }
        Debug.Log("[EnergyManager]: on: " + ENERGY);
        Debug.Log("[EnergyManager]: level: " + energyLevel);
    }

    private static void onEnergyStateChange()
    {
        Debug.Log("[ change]: on: " + ENERGY);
        Debug.Log("[ change]: level: " + energyLevel);
        Building[] buildings = LevelData.GetAllBuildings();
        int buildingLength = buildings.Length;
        Debug.Log("[ change]: buildingLength: " + buildingLength);
        for (int i = buildingLength - 1; i > -1; i--)
        {
            buildings[i].onEnergyStateChange(ENERGY);
        }
    }
}