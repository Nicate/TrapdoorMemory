using UnityEngine;

public class Music : MonoBehaviour {
	public AudioSource menuMusicAudioSource;
	public AudioSource gameMusicAudioSource;


	public void playMenuMusic() {
		menuMusicAudioSource.Play();
	}
	
	public void stopMenuMusic() {
		menuMusicAudioSource.Stop();
	}


	public void playGameMusic() {
		gameMusicAudioSource.Play();
	}
	
	public void stopGameMusic() {
		gameMusicAudioSource.Stop();
	}
}