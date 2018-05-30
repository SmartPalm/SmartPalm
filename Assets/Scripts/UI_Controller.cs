using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {

    //public InputField textBox;
    [HideInInspector]
    public static GameObject hologram;
    [HideInInspector]
    public static GameObject options;
    GameObject touchAreaObject;
    
    private TextMesh hologramText;
    private bool optionsActive;
    private bool colorStatus = true;    // true = white, false = black

    void Start () {
        options = GameObject.Find("OptionsMenu");
        //hologram = GameObject.Find("holotext");
        //hologramText = hologram.GetComponent<TextMesh>();
        //hologramText.text = "Hologram is online";
        options.SetActive(false);

        float halfScreen = (float) Screen.height / 2;

        touchAreaObject = GameObject.Find("TouchArea");
        touchAreaObject.GetComponent<Image>().rectTransform.localScale = new Vector3(Screen.width, halfScreen, 0);
        Vector3 posTmp = touchAreaObject.transform.position;
        posTmp.y -= Screen.height / 4;
        touchAreaObject.transform.position = posTmp;
        //touchAreaObject.transform.position = new Vector3(touchAreaObject.transform.position.x, -(Screen.height / 4), touchAreaObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnChangeDisplay()
    {

        hologramText.text = "Changing...";
        //hologramText.text = textBox.text;
    } 

    public void OpenOptions()
    {
        optionsActive = options.activeSelf;
        options.SetActive(!optionsActive);
    }

    public void ChangeColor()
    {
        if (colorStatus)
        {
            hologramText.color = Color.black;
            GameObject.Find("Debug").GetComponent<Text>().color = Color.white;
            colorStatus = false;
        } else
        {
            hologramText.color = Color.white;
            GameObject.Find("Debug").GetComponent<Text>().color = Color.black;
            colorStatus = true;
        }
    }
}
