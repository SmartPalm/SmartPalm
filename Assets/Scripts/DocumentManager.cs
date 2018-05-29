using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentManager : MonoBehaviour {

    GameObject documentPlaceholder;

	// Use this for initialization
	void Start () {
        documentPlaceholder = GameObject.Find("DocumentPlaceholder");
        documentPlaceholder.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void callMethodForGameObject(GameObject obj)
    {
        Material referencedMaterial = obj.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Renderer>().material;
        documentPlaceholder.GetComponent<Renderer>().material = referencedMaterial;
        documentPlaceholder.SetActive(true);
    }
}
