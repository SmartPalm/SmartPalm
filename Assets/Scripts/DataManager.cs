using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

    [HideInInspector]
    public GameObject currentDisplayedObject;
    private GameObject lastDisplayedObject;
    private bool canHaveAction;

	// Use this for initialization
	void Start () {

        EventManager.StartListening(TouchController.EVENT_SCROLL_HORIZONTAL, onScrollHorizontal);

        EventManager.StartListening(TouchController.EVENT_SCROLL_VERTICAL, onScrollVertical);

        EventManager.StartListening(TouchController.EVENT_DOUBLE_TAP, onDoubleTap);

        EventManager.StartListening(TouchController.EVENT_LAYER_UP, onLayerUp);

        EventManager.StartListening(TouchController.EVENT_LAYER_DOWN, onLayerDown);

        EventManager.StartListening(TouchController.EVENT_DOUBLE_TAP_TWO, onDoubleTapThree);
        currentDisplayedObject = GameObject.Find("menu");
        canHaveAction = true;
    }
	
	// Update is called once per frame
	void Update () {
	}

    void onLayerUp( float changed )
    {
        
    }

    void onLayerDown( float changed )
    {
        if (currentDisplayedObject.GetComponent<DirectoryPathScript>().isNewDirectory)
        {
            currentDisplayedObject.GetComponent<FolderManager>().backToMenu();
        }
        else
        {
            lastDisplayedObject.GetComponent<FolderManager>().backToMenu();
        }
    }

    void onDoubleTap( float changed )
    {
        if (currentDisplayedObject.GetComponent<DirectoryPathScript>().isNewDirectory)
        {
            currentDisplayedObject.GetComponent<FolderManager>().makeSelection();
        }
       
    }


    private void onScrollHorizontal(float changed)
    {
        currentDisplayedObject.transform.Rotate(0, -changed, 0, Space.World);

    }

    private void onScrollVertical(float changed )
    {
        currentDisplayedObject.transform.Rotate(changed, 0, 0, Space.World);

    }

    private void onDoubleTapThree(float value)
    {
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().changePlayingState();
    }

    public void setGameObject(string objectName)
    {
        lastDisplayedObject = currentDisplayedObject;
        currentDisplayedObject = GameObject.Find(objectName);
    }

    public void simulateDoubleTap()
    {
        if (currentDisplayedObject.GetComponent<DirectoryPathScript>().isNewDirectory)
        {
            currentDisplayedObject.GetComponent<FolderManager>().makeSelection();
        }
    }

    public void simulateLayerUp()
    {
        if (currentDisplayedObject.GetComponent<DirectoryPathScript>().isNewDirectory)
        {
            currentDisplayedObject.GetComponent<FolderManager>().backToMenu();
        }
        else
        {
            lastDisplayedObject.GetComponent<FolderManager>().backToMenu();
        }
    }

    public void simulateLayerDown()
    {
        if (currentDisplayedObject.GetComponent<DirectoryPathScript>().isNewDirectory)
        {
            currentDisplayedObject.GetComponent<FolderManager>().printState();
        } else
        {
            lastDisplayedObject.GetComponent<FolderManager>().printState();
        }
        
    }
}
