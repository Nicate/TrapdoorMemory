﻿using UnityEngine;

public class Tile : MonoBehaviour {
	public GameObject trapdoor;
	public GameObject container;

	public MeshRenderer selectableMeshRenderer;
	public Color disabledColor;
	public Color selectionColor;
	public Color activeColor;


	internal Content content;
	
	internal bool selected;
	internal bool activated;
	internal bool disabled;
	internal bool flipped;


	private Material material;
	private Color originalColor;

	private Animator animator;


	private void Awake() {
		material = selectableMeshRenderer.material;
		originalColor = material.GetColor("_BaseColor");

		animator = trapdoor.GetComponent<Animator>();
	}

	private void LateUpdate() {
		container.transform.rotation = Camera.main.transform.rotation;
	}


	public void setContent(Content contentPrefab) {
		foreach(Transform contentTransform in container.transform) {
			contentTransform.SetParent(null);
			Destroy(contentTransform.gameObject);
		}

		content = Instantiate(contentPrefab, container.transform, false);

		content.name = contentPrefab.name;
	}


	public void setSelected(bool selected) {
		this.selected = selected;

		updateColor();
	}

	public void setActivated(bool activated) {
		this.activated = activated;

		updateColor();
	}

	public void setDisabled(bool disabled) {
		this.disabled = disabled;

		updateColor();
	}


	private void updateColor() {
		if(disabled) {
			container.SetActive(false);
		}
		else {
			container.SetActive(true);
		}

		if(disabled) {
			material.SetColor("_BaseColor", disabledColor);
		}
		else if(selected) {
			material.SetColor("_BaseColor", selectionColor);
		}
		else if(activated) {
			material.SetColor("_BaseColor", activeColor);
		}
		else {
			material.SetColor("_BaseColor", originalColor);
		}
	}


	public void flip() {
		if(flipped) {
			animator.SetTrigger("FlipOff");
		}
		else {
			animator.SetTrigger("FlipOn");
		}

		flipped = !flipped;
	}
}