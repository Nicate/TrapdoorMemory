using UnityEngine;

public class Game : MonoBehaviour {
	public Contents contents;
	public Tiles tiles;


	private void Awake() {
		
	}

	private void Start() {
		startGame();
	}

	private void Update() {
		
	}


	public void startGame() {
		tiles.shuffle(contents.contentPrefabs);
	}
}