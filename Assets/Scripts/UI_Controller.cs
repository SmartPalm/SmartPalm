using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {

    public InputField textBox;
    [HideInInspector]
    public static GameObject hologram;
    [HideInInspector]
    public static GameObject options;
    private TextMesh hologramText;
    private bool optionsActive;
    private bool colorStatus = true;    // true = white, false = black

	void Start () {
        options = GameObject.Find("OptionsMenu");
        hologram = GameObject.Find("holotext");
        hologramText = hologram.GetComponent<TextMesh>();
        hologramText.text = "Hologram is online";
        options.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnChangeDisplay()
    {

        hologramText.text = "Changing...";
        hologramText.text = textBox.text;
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
            colorStatus = false;
        } else
        {
            hologramText.color = Color.white;
            colorStatus = true;
        }
    }
}
