using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour {

    GameObject videoPlaceholder, touchArea, videoAudio;
    VideoPlayer player;

    public RenderTexture previewTexture;
    public RenderTexture fullVideoTexture;
    public RenderTexture landscapeTexture;

    // Use this for initialization
    void Start () {

        videoPlaceholder = GameObject.Find("VideoPlaceholder");
        videoPlaceholder.SetActive(false);

        touchArea = GameObject.Find("TouchAreaMenu");

        videoAudio = GameObject.Find("VideoAudioSource");

        foreach(GameObject preview in GameObject.FindGameObjectsWithTag("videoPreview"))
        {
            GameObject parent = preview.transform.parent.gameObject;
            VideoPlayer currentPlayer = preview.GetComponent<VideoPlayer>();
            currentPlayer.renderMode = VideoRenderMode.RenderTexture;
            currentPlayer.targetTexture = previewTexture;
            currentPlayer.frame = 100;
            currentPlayer.Prepare();
            currentPlayer.Play();
            currentPlayer.Pause();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void action(GameObject selectedSphere)
    {

        GameObject selectedVideo = selectedSphere.transform.GetChild(0).gameObject;
        player = selectedVideo.GetComponent<VideoPlayer>();    

        if(player.isPlaying)
        {
            player.Pause();
        }
        else if(player.frame > 10)
        {
            videoPlaceholder.SetActive(true);
            player.Play();
        }
        else
        {
            player.renderMode = VideoRenderMode.RenderTexture;
            player.targetTexture = fullVideoTexture;
            videoPlaceholder.SetActive(true);
            player.Play();

        }
        
    }

    public void callMethodForGameObject(GameObject obj)
    {
        action(obj);
    }

    public void stopVideos()
    {
        foreach(GameObject videoPlayer in GameObject.FindGameObjectsWithTag("videoPreview"))
        {
            videoPlaceholder.SetActive(false);
            videoPlayer.GetComponent<VideoPlayer>().Stop();
        }
    }

    public void displayVideoLandscape(bool landscape)
    {
        if (landscape == true)
        {

            if (player != null)
            {
                touchArea.SetActive(false);
                //GameObject.Find("MainImageTarget").SetActive(false);
                videoPlaceholder.SetActive(false);
                player.renderMode = VideoRenderMode.CameraNearPlane;
                //playingPlayer.targetTexture = fullVideoTexture;
                player.targetCamera = GameObject.Find("ARCamera").GetComponent<Camera>();
                //playingPlayer.aspectRatio = VideoAspectRatio.FitVertically;
                //fullscreenPlaceholder.SetActive(true);
                //fullscreenPlaceholder.GetComponent<Image>
                //playingPlayer.Play();
            }
        }
        else
        {
            touchArea.SetActive(true);
            //GameObject.Find("MainImageTarget").SetActive(true);
            player.renderMode = VideoRenderMode.RenderTexture;
            player.targetCamera = null;
            player.targetTexture = fullVideoTexture;
            //player.frame = 1;
            videoPlaceholder.SetActive(true);
            //player.Play();
        }
    }
}
