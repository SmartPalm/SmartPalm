using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimationControllerScript : MonoBehaviour {

    public Dictionary<string, PlayableDirector> PlayableDic = new Dictionary<string, PlayableDirector>();

    private PlayableDirector focusAnimation;
    private bool playFocusReverse;
    private bool focusIsPlaying;
    private bool focusWasPlayed;
    private double rewindTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        controlStateOfFocus();

        if(playFocusReverse)
        {
            reverse(focusAnimation);
        }

    }

    private void controlStateOfFocus()
    {
        if (focusAnimation.time == 0) // checks if the playable is still playing
        {
            focusIsPlaying = false;
            focusWasPlayed = false;
        }
        else if (focusAnimation.time >= focusAnimation.duration)
        {
            focusWasPlayed = true;
            focusIsPlaying = false;
        }
        else
        {
            focusWasPlayed = false;
            focusIsPlaying = true;
            //Debug.Log("playing");
        }

        if(playFocusReverse)
        {
            reverse(focusAnimation);
        }
    }

    private void reverse(PlayableDirector ani)
    {
        ani.Stop();
        ani.time = ani.playableAsset.duration - 0.01;
        ani.Evaluate();
    }

    private void reversing(PlayableDirector t)
    {
        //Debug.Log("Revinding: " + t.name);
        rewindTimer = t.time - Time.deltaTime;
        if (rewindTimer < 0)
            rewindTimer = 0;

        t.time = rewindTimer;
        t.Evaluate();

        if (rewindTimer == 0)
        {
            t.Stop();
            playFocusReverse = false;
            //Debug.Log("focus set to null");

        }
    }

    public void setFocusAnimation(string newAni)
    {
        if(PlayableDic[newAni] != null)
        {
            focusAnimation = PlayableDic[newAni];
        } else
        {
            Debug.Log("------ ANIMATION-ERROR: " + newAni + " does not exist as in the dictionary --------------");
        }
    }

    public void playFocusAnimation()
    {
        focusAnimation.Play();
    }

    public void playFocusAnimationReversed()
    {
        playFocusReverse = true;
        reversing(focusAnimation);
    }
}
