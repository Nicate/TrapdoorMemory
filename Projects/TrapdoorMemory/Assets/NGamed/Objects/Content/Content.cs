using UnityEngine;

public class Content : MonoBehaviour {
	public GameObject sprite;


	private void LateUpdate() {
		sprite.transform.rotation = Camera.main.transform.rotation;
	}
}