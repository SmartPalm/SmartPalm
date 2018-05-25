using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanEditorScript : MonoBehaviour {

    public GameObject First;
    public GameObject Second;
    public GameObject Third;


    void Start () {
        First.SetActive(true);
        Second.SetActive(true);
        Third.SetActive(true);
	}
}
