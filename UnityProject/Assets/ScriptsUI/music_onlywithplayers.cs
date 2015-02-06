using UnityEngine;
using System.Collections;

public class music_onlywithplayers : MonoBehaviour {

    float lastcheck = 0f;
    public float musicvolume = 0.1f;

    AudioSource music;
	// Use this for initialization
	void Start () {
        music = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - lastcheck > 0.1f)
        {
            lastcheck = Time.time;
            GameObject[] players=GameObject.FindGameObjectsWithTag("Octopus");
            if (players.Length > 0)
            {
                music.volume = musicvolume;
                if (!music.isPlaying)
                {
                    music.Play();
                }
                music.loop = true;
            }
            else
            {
                music.volume = Mathf.Lerp(music.volume, 0f, 0.1f);
                if (music.volume < 0.0010f)
                {
                    music.Stop();
                }
                music.loop = false;
            }
        }
	}
}
