using UnityEngine;
using System.Collections;

public class BuildingTurretSmall : Building {
	public static int width = 2;
	public static int height = 3;
	
	void Start () {
		buyAmount = 800;
		sellAmount = 250;
		energy = 100;
		health = 100;
		
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