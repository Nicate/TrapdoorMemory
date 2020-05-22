using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	public Menu menu;
	public HUD hud;
	public GameOver gameOver;

	public Contents contents;
	public Tiles tiles;

	public Music music;

	public Opponent opponent;
	
	public float selectionRange = 1.0f;


	private Tile selectedTile;
	private List<Tile> activatedTiles;
	
	private bool playing;
	private bool turn;
	private Coroutine opponentTurnCoroutine;


	private void Awake() {
		selectedTile = null;
		activatedTiles = new List<Tile>();

		playing = false;
		turn = true;
		opponentTurnCoroutine = null;
	}

	private void Start() {
		menu.setEnabled(true);
		gameOver.setEnabled(false);

		music.playMenuMusic();
	}

	private void Update() {
		if(playing) {
			updateGame();
		}
	}


	public void startGame() {
		menu.setEnabled(false);
		gameOver.setEnabled(false);

		hud.resetScores();
		gameOver.resetScores();

		tiles.shuffle(contents.contentPrefabs);

		opponent.setTiles(tiles.tiles);
		
		music.stopMenuMusic();
		music.playGameMusic();

		playing = true;
		turn = true;
	}

	private void updateGame() {
		if(selectedTile != null) {
			selectedTile.setSelected(false);
		}

		if(turn) {
			Vector3 cursor = hud.getPointOnPlane(Input.mousePosition, 0.0f);

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

						if(activatedTiles.Count == 2) {
							bool match = tiles.isPair(activatedTiles[0], activatedTiles[1]);

							bool left = true;

							foreach(Tile activatedTile in activatedTiles.ToArray()) {
								activatedTile.setActivated(false);
								activatedTiles.Remove(activatedTile);
								
								if(match) {
									activatedTile.setDisabled(true);

									if(activatedTile.flipped) {
										activatedTile.flip();
									}

									hud.incrementScore1();
									gameOver.incrementScore1();

									opponent.removeKnownTile(activatedTile);
								}
								else {
									activatedTile.flip();

									if(!activatedTile.flipped && activatedTile.content is SoundContent) {
										SoundContent soundContent = activatedTile.content as SoundContent;

										soundContent.play(left, !left);

										left = false;
									}

									if(activatedTile.flipped) {
										opponent.addKnownTile(activatedTile);
									}
								}
							}

							turn = false;

							if(tiles.getAvailableTiles().Length > 0) {
								opponentTurnCoroutine = StartCoroutine(handleOpponentTurn());
							}
							else {
								StartCoroutine(handleStopGame());
							}
						}
						else if(selectedTile.content is SoundContent) {
							SoundContent soundContent = selectedTile.content as SoundContent;

							soundContent.play(true, true);
						}
					}
				}
			}
		}
	}

	private IEnumerator handleOpponentTurn() {
		yield return new WaitForSeconds(opponent.delay);
		
		Tile[] choice = opponent.chooseTiles();

		bool match = tiles.isPair(choice[0], choice[1]);

		bool left = true;

		foreach(Tile tile in choice) {
			if(match) {
				tile.setDisabled(true);

				if(!tile.flipped) {
					tile.flip();
				}

				hud.incrementScore2();
				gameOver.incrementScore2();

				opponent.removeKnownTile(tile);
			}
			else {
				tile.flip();

				if(!tile.flipped && tile.content is SoundContent) {
					SoundContent soundContent = tile.content as SoundContent;

					soundContent.play(left, !left);

					left = false;
				}

				if(tile.flipped) {
					opponent.addKnownTile(tile);
				}
			}
		}

		if(tiles.getAvailableTiles().Length > 0) {
			turn = true;
		}
		else {
			StartCoroutine(handleStopGame());
		}

		opponentTurnCoroutine = null;
	}

	private IEnumerator handleStopGame() {
		yield return new WaitForSeconds(opponent.delay);

		stopGame();
	}

	public void stopGame() {
		gameOver.setEnabled(true);

		music.stopGameMusic();

		playing = false;
	}
}