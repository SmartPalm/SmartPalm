using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

    private string playingAudio;

	// Use this for initialization
	void Start () {
        playingAudio = "nothing";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void callMethodForGameObject(GameObject obj)
    {
        Debug.Log("Call to play audio. Called obj is: " + obj + " and playingAudio is: " + playingAudio);
        if (obj.name == playingAudio || playingAudio == "nothing")
        {
            if (playingAudio == "nothing")
            {
                obj.GetComponent<AudioSource>().Play();
                playingAudio = obj.name;
            }
            else
            {
                obj.GetComponent<AudioSource>().Pause();
                playingAudio = "nothing";
            }
        } else
        {
            GameObject.Find(playingAudio).GetComponent<AudioSource>().Pause();
            obj.GetComponent<AudioSource>().Play();
            playingAudio = obj.name;
        }   
    }
}
