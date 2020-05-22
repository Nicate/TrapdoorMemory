using Boo.Lang;
using UnityEditor;
using UnityEngine;

public class RenameTiles {
	[MenuItem("Trapdoor Memory/List Tiles")]
	public static void rename() {
		GameObject[] gameObjects = Selection.gameObjects;

		foreach(GameObject gameObject in Selection.gameObjects) {
			Tiles tiles = gameObject.GetComponent<Tiles>();

			if(gameObject.activeInHierarchy && tiles != null) {
				int count = 1;

				List<Tile> childTiles = new List<Tile>();

				foreach(Transform childTransform in gameObject.transform) {
					GameObject childGameObject = childTransform.gameObject;

					Tile tile = childGameObject.GetComponent<Tile>();

					if(tile != null) {
						childGameObject.name = "Tile" + count;

						count += 1;

						childTiles.Add(tile);
					}
				}

				tiles.tiles = childTiles.ToArray();

				EditorUtility.SetDirty(tiles);
			}
		}

		AssetDatabase.SaveAssets();
	}
}