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
		buildingWidth = width;
		buildingHeight = height;
		spriterenderer = GetComponent<SpriteRenderer>();
		onEnergyStateChange(LevelData.ENERGY);
		LevelData.buildingList.Add(this);
		LevelData.calculateEnegy();
		transform.position = LevelData.addSizeToPosition(transform.position, getBuildingWidth(), getBuildingHeight());	
	}
	
	void Update () {
	}
}