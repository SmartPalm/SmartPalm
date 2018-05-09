using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbFloat : MonoBehaviour {
    private float turnX;
    private float turnY;

    [Range(0.0f, 100.0f)]
    public float rotVSpeed = 20.0f;
    [Range(0.0f, 100.0f)]
    public float rotHSpeed = 15.0f;
    [Range(0.0f, 1.0f)]
    public float sineStrength = 0.1f;
    [Range(0.0f, 0.1f)]
    public float sineSpeed = 0.015f;

    private Vector3 currentPos, grabbedPos;
    private Vector3 startPos;
    private float startY;
    private float sinePos;
    private float currentSine = 0.0f;
    private bool isFloating = true;
    private bool isGrabbed = false;
    private bool floatBack = false;
    private float smoothTime = 1f;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start () {
        currentPos = transform.position;
        startPos = currentPos;
        startY = currentPos.y;

        //Debug.Log("The object " + gameObject.name + " has this rigidbody " + gameObject.GetComponent<Rigidbody>()); 
	}
	
	// Update is called once per frame
	void Update () {
        /*if (gameObject.tag == "ChangeSphere")
        {
            Debug.Log(gameObject.name + " isGrabbed is " + isGrabbed + " and isFloating is " + isFloating);
        }*/
            #region Drehung um die eigene Achse
            //turn animation
            turnX = Time.deltaTime * rotVSpeed;
            turnY = Time.deltaTime * rotHSpeed;

            transform.Rotate(turnX, turnY, 0);
            #endregion

            #region Wellenbewegung
            //sine wave movement
            sinePos = Mathf.Sin(currentSine) * sineStrength;
            currentSine += sineSpeed;
            currentPos.y = startY + sinePos;
            transform.position = currentPos;
            #endregion

    }

    public void staph ()
    {
        isFloating = false;
        floatBack = false;
    }

    public void goeh ()
    {
        isFloating = true;
 
    }

}
