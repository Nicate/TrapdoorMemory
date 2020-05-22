using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Opponent : MonoBehaviour {
	public float difficulty = 0.5f;

	public float delay = 1.0f;

	
	internal List<Tile> knownTiles;
	internal List<Tile> unknownTiles;


	private void Awake() {
		knownTiles = new List<Tile>();
		unknownTiles = new List<Tile>();
	}


	public void setTiles(Tile[] tiles) {
		foreach(Tile tile in tiles) {
			if(tile.flipped) {
				knownTiles.Add(tile);
			}
			else {
				unknownTiles.Add(tile);
			}
		}
	}


	public void addKnownTile(Tile tile) {
		if(!knownTiles.Contains(tile)) {
			knownTiles.Add(tile);
		}

		if(unknownTiles.Contains(tile)) {
			unknownTiles.Remove(tile);
		}
	}

	public void removeKnownTile(Tile tile) {
		if(knownTiles.Contains(tile)) {
			knownTiles.Remove(tile);
		}

		if(unknownTiles.Contains(tile)) {
			unknownTiles.Remove(tile);
		}
	}


	public Tile[] chooseTiles() {
		if(knownTiles.Count + unknownTiles.Count > 2) {
			List<Tile> tiles = new List<Tile>();

			tiles.AddRange(knownTiles);
			tiles.AddRange(unknownTiles);

			ListRandom.shuffle(tiles);

			return new Tile[] {
				tiles[0],
				tiles[1]
			};
		}
		else if(knownTiles.Count + unknownTiles.Count == 2) {
			List<Tile> tiles = new List<Tile>();

			tiles.AddRange(knownTiles);
			tiles.AddRange(unknownTiles);

			return tiles.ToArray();
		}
		else {
			return null;
		}
	}
}