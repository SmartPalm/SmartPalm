using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour {

    GameObject videoPlaceholder;

    public RenderTexture previewTexture;
    public RenderTexture fullVideoTexture;

    // Use this for initialization
    void Start () {

        videoPlaceholder = GameObject.Find("VideoPlaceholder");
        videoPlaceholder.SetActive(false);

		foreach(GameObject preview in GameObject.FindGameObjectsWithTag("videoPreview"))
        {
            GameObject parent = preview.transform.parent.gameObject;
            VideoPlayer player = preview.GetComponent<VideoPlayer>();
            player.renderMode = VideoRenderMode.RenderTexture;
            player.targetTexture = previewTexture;
            player.frame = 100;
            player.Prepare();
            player.Play();
            player.Pause();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Action(GameObject selectedSphere)
    {

        GameObject selectedVideo = selectedSphere.transform.GetChild(0).gameObject;
        VideoPlayer player = selectedVideo.GetComponent<VideoPlayer>();    

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
            player.renderMode = VideoRenderMode.RenderTexture;
            player.targetTexture = fullVideoTexture;
            player.frame = 1;
            videoPlaceholder.SetActive(true);
            player.Play();
        }
        
    }

    public void callMethodForGameObject(GameObject obj)
    {
        Action(obj);
    }
}
