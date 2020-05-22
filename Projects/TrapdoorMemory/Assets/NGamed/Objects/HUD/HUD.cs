using System.Globalization;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour {
	public Camera viewer;

	public TextMeshProUGUI score1Text;
	public TextMeshProUGUI score2Text;


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
	}


	public Vector3 getPointOnPlane(Vector3 point, float height) {
		Ray ray = viewer.ScreenPointToRay(point);
		Plane plane = new Plane(Vector3.up, -height);

		float distance;

		if(plane.Raycast(ray, out distance)) {
			return ray.GetPoint(distance);
		}
		else {
			return Vector3.zero;
		}
	}
}