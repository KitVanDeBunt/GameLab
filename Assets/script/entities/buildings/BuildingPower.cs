using UnityEngine;
using System.Collections;

public class BuildingPower : Building {
	public static int width = 2;
	public static int height = 3;
	
	void Start () {
		buyAmount = 1000;
		sellAmount = 400;
		energy = 500;
		health = 100;
		
		health = maxHealth;
		size.x = width;
		size.y = height;
		spriterenderer = GetComponent<SpriteRenderer>();
        onEnergyStateChange(EnergyManager.ENERGY);
		LevelData.mapObjects.Add(this);
        EnergyManager.calculateEnegy();
		transform.position = IsoMath.addSizeToPosition(transform.position, getBuildingWidth(), getBuildingHeight(),LevelData.size);	
	}
	
	void Update () {
	}
}