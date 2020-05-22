using System.Globalization;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour {
	public Game game;

	public TextMeshProUGUI score1Text;
	public TextMeshProUGUI score2Text;

	public GameObject youWin;
	public GameObject aTie;
	public GameObject youLose;


	private int score1;
	private int score2;


	private void Awake() {
		resetScores();
	}

	private void Start() {
		updateScores();
	}


	public void resetScores() {
		score1 = 0;
		score2 = 0;
	}

	public void incrementScore1() {
		score1 += 1;

		updateScores();
	}
	
	public void incrementScore2() {
		score2 += 1;

		updateScores();
	}

	private void updateScores() {
		score1Text.text = score1.ToString(CultureInfo.InvariantCulture);
		score2Text.text = score2.ToString(CultureInfo.InvariantCulture);

		if(score1 > score2) {
			youWin.SetActive(true);
			aTie.SetActive(false);
			youLose.SetActive(false);
		}
		else if(score1 == score2) {
			youWin.SetActive(false);
			aTie.SetActive(true);
			youLose.SetActive(false);
		}
		else {
			youWin.SetActive(false);
			aTie.SetActive(false);
			youLose.SetActive(true);
		}
	}


	public void setEnabled(bool enabled) {
		gameObject.SetActive(enabled);
	}


	public void playAgain() {
		game.startGame();
	}

	public void menu() {
		setEnabled(false);
		game.menu.setEnabled(true);
	}

	public void quit() {
		Application.Quit();
	}
}