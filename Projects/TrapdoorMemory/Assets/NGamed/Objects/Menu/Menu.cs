using UnityEngine;

public class Menu : MonoBehaviour {
	public Game game;


	public void setEnabled(bool enabled) {
		gameObject.SetActive(enabled);
	}


	public void startGame(float difficulty) {
		game.opponent.difficulty = difficulty;

		game.startGame();
	}
}