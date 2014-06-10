using UnityEngine;
using System.Collections;

public class BuildingRefinery : Building {
	public static int width = 2;
	public static int height = 3;

	void Start () {
		buyAmount = 2000;
		sellAmount = 800;
		energy = 200;
		health = 140;

		health = maxHealth;
		buildingWidth = width;
		buildingHeight = height;
		spriterenderer = GetComponent<SpriteRenderer>();
        onEnergyStateChange(EnergyManager.ENERGY);
		LevelData.buildingList.Add(this);
        EnergyManager.calculateEnegy();
        transform.position = IsoMath.addSizeToPosition(transform.position, getBuildingWidth(), getBuildingHeight(), LevelData.size);	
	}
	
	void Update () {
	}
}
