using UnityEngine;

public class EnergyManager
{
    private static int energyLevel;
    public static bool ENERGY = true;

    public static void calculateEnegy()
    {
        energyLevel = 0;

        int buildingLength = LevelData.buildingList.Count;
        for (int i = 0; i < buildingLength; i++)
        {
            energyLevel += LevelData.buildingList[i].getEnergyUsage();
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
        for (int i = LevelData.buildingList.Count - 1; i > -1; i--)
        {
            LevelData.buildingList[i].onEnergyStateChange(ENERGY);
        }
    }
}