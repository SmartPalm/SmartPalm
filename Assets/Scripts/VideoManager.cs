using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour {

    private GameObject videoPlaceHolder; 

	// Use this for initialization
	void Start () {
        videoPlaceHolder = GameObject.Find("VideoPlaceholder");
        videoPlaceHolder.SetActive(false);
        foreach (GameObject foo in GameObject.FindGameObjectsWithTag("videoPreview"))
        {
            VideoPlayer player = foo.GetComponent<VideoPlayer>();
            player.frame = 250;
            player.Pause();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Action(GameObject selectedSphere)
    {
        videoPlaceHolder.SetActive(true);
        GameObject selectedVideo = selectedSphere.transform.GetChild(1).gameObject;
        VideoPlayer player = selectedVideo.GetComponent<VideoPlayer>();
        Debug.Log("Bis hier hin und " + player);
        player.Play();
    }

    public void callMethodForGameObject(GameObject obj)
    {
        Action(obj);
    }
}
