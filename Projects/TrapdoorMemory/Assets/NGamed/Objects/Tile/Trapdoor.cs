using UnityEngine;

public class Trapdoor : MonoBehaviour {
	private Animator animator;


	private void Awake() {
		animator = GetComponent<Animator>();
	}


	public void handleEndOfFlipAnimation1() {
		animator.SetTrigger("ExitFlipOn");
	}

	public void handleEndOfFlipAnimation2() {
		animator.SetTrigger("ExitFlipOff");
	}
}