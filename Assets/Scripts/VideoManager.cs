using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		    foreach(GameObject foo in GameObject.FindGameObjectsWithTag("videoPreview"))
        {
            VideoPlayer player = foo.GetComponent<VideoPlayer>();
            player.frame = 250;
            player.Stop();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Action(GameObject selectedSphere)
    {
        GameObject selectedVideo = selectedSphere.transform.GetChild(0).transform.GetChild(0).gameObject;
        VideoPlayer player = selectedVideo.GetComponent<VideoPlayer>();
        player.Play();
    }
}
