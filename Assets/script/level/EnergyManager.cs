using UnityEngine;

public class EnergyManager
{
    private static int energyLevel;
    public static bool ENERGY = true;

    public static void calculateEnegy()
    {
        energyLevel = 0;

        Building[] buildings = LevelData.GetAllBuildings();
        int buildingLength = buildings.Length;
        for (int i = 0; i < buildingLength; i++)
        {
            energyLevel += buildings[i].getEnergyUsage();
        }

        if (energyLevel > -1 && !ENERGY)
        {
            ENERGY = true;
            onEnergyStateChange();
        }
        else if (energyLevel < 0 && ENERGY)
        {
            ENERGY = false;
            onEnergyStateChange();
        }
    }

    public static void onEnergyStateChange()
    {
        Debug.Log(ENERGY);
        Building[] buildings = LevelData.GetAllBuildings();
        int buildingLength = buildings.Length;
        for (int i = buildingLength - 1; i > -1; i--)
        {
            buildings[i].onEnergyStateChange(ENERGY);
        }
    }
}