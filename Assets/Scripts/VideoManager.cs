using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour {

    GameObject videoPlaceholder, fullscreenPlaceholder;
    VideoPlayer player;

    public RenderTexture previewTexture;
    public RenderTexture fullVideoTexture;
    public RenderTexture landscapeTexture;

    // Use this for initialization
    void Start () {

        videoPlaceholder = GameObject.Find("VideoPlaceholder");
        videoPlaceholder.SetActive(false);

        fullscreenPlaceholder = GameObject.Find("VideoLandscapePlaceholder");
        fullscreenPlaceholder.SetActive(false);

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
        else if(player.frame >= 1)
        {
            videoPlaceholder.SetActive(true);
            player.Play();
        }
        else
        {
            displayVideoLandscape(false);
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
            videoPlayer.GetComponent<VideoPlayer>().Stop();
        }
    }

    public void displayVideoLandscape(bool landscape = true)
    {
        if(landscape)
        {
            VideoPlayer playingPlayer = null;

            foreach (GameObject videoPlayer in GameObject.FindGameObjectsWithTag("videoPreview"))
            {
                VideoPlayer player = videoPlayer.GetComponent<VideoPlayer>();
                if (player.isPlaying)
                {
                    playingPlayer = player;
                    break;
                }
            }

            if (playingPlayer != null)
            {
                playingPlayer.renderMode = VideoRenderMode.CameraFarPlane;
                //playingPlayer.targetTexture = fullVideoTexture;
                playingPlayer.aspectRatio = VideoAspectRatio.FitVertically;
                //fullscreenPlaceholder.SetActive(true);
                //fullscreenPlaceholder.GetComponent<Image>
                //playingPlayer.Play();
            }
        }
        else
        {
            player.renderMode = VideoRenderMode.RenderTexture;
            player.targetTexture = fullVideoTexture;
            player.frame = 1;
            videoPlaceholder.SetActive(true);
            player.Play();
        }
    }
}
