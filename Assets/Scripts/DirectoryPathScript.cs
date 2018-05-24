using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectoryPathScript : MonoBehaviour {

    [Tooltip ("Enter the name of the subfolder")]
    public string directory;
    public bool isNewDirectory;

	// Use this for initialization
	void Start () {
		if(transform.GetChild(0) == null)
        {
            isNewDirectory = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
