﻿using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour {
	public Tile[] tiles;


	private struct Pair {
		public Tile tile1;
		public Tile tile2;

		public Pair(Tile tile1, Tile tile2) {
			this.tile1 = tile1;
			this.tile2 = tile2;
		}
		
		public override bool Equals(object obj) {
			if(obj == null || GetType() != obj.GetType()) {
				return false;
			}

			Pair that = (Pair) obj;

			return that.tile1 == tile1 && that.tile2 == tile2;
		}
		
		public override int GetHashCode() {
			return tile1.GetHashCode() + 31 * tile2.GetHashCode();
		}
	}

	private List<Pair> pairs;


	private void Awake() {
		pairs = new List<Pair>();
	}


	public void shuffle(Content[] contentPrefabs) {
		if(tiles.Length % 2 == 0) {
			if(contentPrefabs.Length * 2 >= tiles.Length) {
				List<Content> shuffledContentPrefabs = new List<Content>(contentPrefabs);
				ListRandom.shuffle(shuffledContentPrefabs);

				List<Tile> shuffledTiles = new List<Tile>(tiles);
				ListRandom.shuffle(shuffledTiles);

				pairs.Clear();

				for(int index = 0; index < shuffledTiles.Count; index += 2) {
					Tile tile1 = shuffledTiles[index];
					Tile tile2 = shuffledTiles[index + 1];

					Content contentPrefab = shuffledContentPrefabs[0];
					shuffledContentPrefabs.RemoveAt(0);

					tile1.setContent(contentPrefab);
					tile2.setContent(contentPrefab);

					tile2.flip();

					Pair pair = new Pair(tile1, tile2);

					pairs.Add(pair);
				}
			}
			else {
				Debug.LogError("Not enough content for the number of tiles.");
			}
		}
		else {
			Debug.LogError("Even number of tiles required to form pairs.");
		}
	}
}