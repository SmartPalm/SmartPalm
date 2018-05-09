using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {

    public InputField textBox;
    [HideInInspector]
    public static GameObject hologram;
    private TextMesh hologramText;

	void Start () {
        hologram = GameObject.Find("holotext");
        hologramText = hologram.GetComponent<TextMesh>();
        hologramText.text = "Hologram is online";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnChangeDisplay()
    {

        hologramText.text = "Changing...";
        hologramText.text = textBox.text;
    } 
}
