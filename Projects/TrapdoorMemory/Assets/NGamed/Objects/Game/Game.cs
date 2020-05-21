using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	public UserInterface userInterface;

	public Contents contents;
	public Tiles tiles;
	
	public float selectionRange = 1.0f;


	private Tile selectedTile;
	private List<Tile> activatedTiles;

	private int score;


	private void Awake() {
		selectedTile = null;
		activatedTiles = new List<Tile>();

		score = 0;
	}

	private void Start() {
		startGame();
	}

	private void Update() {
		updateGame();
	}


	private void startGame() {
		tiles.shuffle(contents.contentPrefabs);
	}

	private void updateGame() {
		Vector3 cursor = userInterface.getPointOnPlane(Input.mousePosition, 0.0f);

		if(selectedTile != null) {
			selectedTile.setSelected(false);
		}

		selectedTile = tiles.findNearestTile(cursor, selectionRange);

		if(selectedTile != null && !selectedTile.disabled) {
			selectedTile.setSelected(true);

			if(Input.GetMouseButtonDown(0)) {
				if(selectedTile.activated) {
					selectedTile.setActivated(false);
					activatedTiles.Remove(selectedTile);
				}
				else {
					selectedTile.setActivated(true);
					activatedTiles.Add(selectedTile);

					if(activatedTiles.Count >= 2) {
						bool match = tiles.isPair(activatedTiles[0], activatedTiles[1]);

						foreach(Tile activatedTile in activatedTiles.ToArray()) {
							activatedTile.setActivated(false);
							activatedTiles.Remove(activatedTile);
							
							if(match) {
								activatedTile.setDisabled(true);

								if(activatedTile.flipped) {
									activatedTile.flip();
								}

								score += 1;
							}
							else {
								activatedTile.flip();
							}
						}
					}
				}
			}
		}
	}
}