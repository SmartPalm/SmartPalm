using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimationControllerScript : MonoBehaviour {

    public Dictionary<string, PlayableDirector> PlayableDic = new Dictionary<string, PlayableDirector>();

    private PlayableDirector focusAnimation;
    private bool playFocusReverse = false;
    private bool focusIsPlaying = false;
    private bool focusWasPlayed = false;
    private double rewindTimer;

	// Use this for initialization
	void Start () {
        fillDictionary();
	}
	
	// Update is called once per frame
	void Update () {

        controlStateOfFocus();

        if(playFocusReverse)
        {
            reversing(focusAnimation);
        }

    }

    private void fillDictionary()
    {
        PlayableDic.Add("toilet", GameObject.Find("TestAnimationTarget").GetComponent<PlayableDirector>());
        PlayableDic.Add("menu", GameObject.Find("menu").GetComponent<PlayableDirector>());
        PlayableDic.Add("BubbleExplorerFor3DModels", GameObject.Find("3DModelFolder").GetComponent<PlayableDirector>());
        PlayableDic.Add("BubbleExplorerForBluetooth", GameObject.Find("Bluetooth").GetComponent<PlayableDirector>());
        PlayableDic.Add("armbones", GameObject.Find("armbones").GetComponent<PlayableDirector>());
        PlayableDic.Add("DevFolder", GameObject.Find("DevFolder").GetComponent<PlayableDirector>());
    }

    private void controlStateOfFocus()
    {
        if (focusAnimation != null)
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

            if (playFocusReverse)
            {
                reversing(focusAnimation);
                //Debug.Log("Started Reversing");
            }
        }   
    }

    private void reverse(PlayableDirector ani)
    {
        //Debug.Log("called reverse function");
        ani.Stop();
        ani.time = ani.playableAsset.duration - 0.01;
        ani.Evaluate();
    }

    private void reversing(PlayableDirector t)
    {
        //Debug.Log("Revinding: " + t.name);
        rewindTimer = t.time - Time.deltaTime;
        //Debug.Log(rewindTimer);

        if (rewindTimer < 0)
            rewindTimer = 0;

        t.time = rewindTimer;
        t.Evaluate();

        if (rewindTimer == 0)
        {
            t.Stop();
            playFocusReverse = false;
            Debug.Log("Finished revinding");
        }
    }

    public void setFocusAnimation(string newAni)
    {
        if (PlayableDic[newAni] != null)
        {
            if (!focusIsPlaying)
            {
                focusAnimation = PlayableDic[newAni];
            }
            
        } else
        {
            Debug.Log("------ ANIMATION-ERROR: " + newAni + " does not exist in the dictionary --------------");
        }
    }

    public void playFocusAnimation()
    {
        focusAnimation.Play();
    }

    public void playFocusAnimationReversed()
    {
        if (!focusIsPlaying)
        {
            playFocusReverse = true;
            reverse(focusAnimation);
        }
    }

}
