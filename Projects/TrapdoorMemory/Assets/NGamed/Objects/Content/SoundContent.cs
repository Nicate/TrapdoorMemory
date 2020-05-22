using UnityEngine;

public class SoundContent : Content {
	public void play(bool left, bool right) {
		AudioSource audioSource = GetComponent<AudioSource>();

		if(left && right) {
			audioSource.panStereo = 0.0f;

			audioSource.Play();
		}
		else if(left) {
			audioSource.panStereo = -1.0f;

			audioSource.Play();
		}
		else if(right) {
			audioSource.panStereo = 1.0f;

			// Create a sort of unison if you match sounds.
			audioSource.PlayDelayed(0.1f);
		}
	}
}