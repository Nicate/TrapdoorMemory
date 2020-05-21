using UnityEngine;

public class Tile : MonoBehaviour {
	public GameObject trapdoor;
	public GameObject container;


	internal bool flipped;


	public void setContent(Content contentPrefab) {
		foreach(Transform contentTransform in container.transform) {
			contentTransform.SetParent(null);
			Destroy(contentTransform.gameObject);
		}

		Content content = Instantiate(contentPrefab, container.transform, false);

		content.name = contentPrefab.name;
	}


	public void flip() {
		Quaternion rotation = trapdoor.transform.rotation;

		if(flipped) {
			rotation = Quaternion.identity;
		}
		else {
			rotation = Quaternion.AngleAxis(180.0f, Vector3.right);
		}

		trapdoor.transform.rotation = rotation;

		flipped = !flipped;
	}
}