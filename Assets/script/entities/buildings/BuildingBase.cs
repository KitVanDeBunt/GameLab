using UnityEngine;
using System.Collections;

public class BuildingBase : Building {
	public static int width = 2;
	public static int height = 3;
	
	void Start () {
		buyAmount = 0;
		sellAmount = 500;
		energy = 0;
		health = 200;
		
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