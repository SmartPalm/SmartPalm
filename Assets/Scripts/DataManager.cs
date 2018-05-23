using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {

    private GameObject currentDisplayedObject;
    private bool canHaveAction;

	// Use this for initialization
	void Start () {

        EventManager.StartListening(TouchController.EVENT_SCROLL_HORIZONTAL, onScrollHorizontal);

        EventManager.StartListening(TouchController.EVENT_SCROLL_VERTICAL, onScrollVertical);

        EventManager.StartListening(TouchController.EVENT_DOUBLE_TAP, onDoubleTap);

        EventManager.StartListening(TouchController.EVENT_LAYER_UP, onLayerUp);

        EventManager.StartListening(TouchController.EVENT_LAYER_DOWN, onLayerDown);
        currentDisplayedObject = GameObject.Find("BubbleExplorer");
        canHaveAction = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void onLayerUp( float changed )
    {
       GameObject.Find("BubbleExplorer").GetComponent<FolderManager>().backToMenu();
    }

    void onLayerDown( float changed )
    {

    }

    void onDoubleTap( float changed )
    {
       GameObject.Find("BubbleExplorer").GetComponent<FolderManager>().makeSelection();
    }


    private void onScrollHorizontal(float changed)
    {
        //GameObject.Find("Debug").GetComponent<Text>().text = "Scroll Horizontal";
        if (canHaveAction)
        {
            currentDisplayedObject.transform.Rotate(0, changed, 0, Space.World);
        }
    }

    private void onScrollVertical(float changed )
    {
        if(canHaveAction)
        {
            currentDisplayedObject.transform.Rotate(changed, 0, 0, Space.World);
        }
    }

    public void setGameObject(string objectName)
    {
        currentDisplayedObject = GameObject.Find(objectName);
    }

    public void startAction()
    {
        canHaveAction = true;
    }

    public void stopAction()
    {
        canHaveAction = false;
    }
}
