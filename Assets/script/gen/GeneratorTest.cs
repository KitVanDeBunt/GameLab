using UnityEngine;
using System.Collections;

public class GeneratorTest : IGenerator {
	public void Generate(int size) {
		GenerateSurface(size);
	}

	public void GenerateSurface(int size) {
		int[,] tiles;
		tiles = new int[size,size];
		
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				int t = (int) (Mathf.PerlinNoise((float)i / 7f, (float)j / 7f) * 6) - 1;
				Debug.Log(Mathf.PerlinNoise((float)i / 7f, (float)j / 7f) * 3);
				t = t < 0 ? 0 : t > 2 ? 2 : t;
				tiles [i, j] = t;
			}
		}

		LevelData.tileData = tiles;
	}
}
