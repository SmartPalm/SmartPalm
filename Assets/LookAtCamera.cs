using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    [Tooltip ("Check this box, to stop the shaking of the target object")]
    public bool lookAtTargetStrictly;
    [Range(0.0f, 10f)]
    public float additionOffset;
    [Range (0.0f, 10f)]
    public float subtractionOffset;

    private Transform target;
    private float xRot;
    private float yRot;
    private float zRot;
    private Vector3 newRot;
    private Transform lateTarget;
    private float lateYAddOffset;
    private float lateYSubOffset;
    
	// Use this for initialization
	void Start () {
        target = GameObject.Find("ARCamera").transform;

        lateYAddOffset = target.eulerAngles.y + additionOffset;
        lateYSubOffset = target.eulerAngles.y - subtractionOffset;
    }
	
	// Update is called once per frame
	void Update () {

        xRot = target.eulerAngles.x;
        yRot = target.eulerAngles.y;
        zRot = target.eulerAngles.z;
        newRot = new Vector3(0, yRot, 0);
        

        if (lookAtTargetStrictly)
        {

            transform.eulerAngles = newRot;
            
        } else
        {
            if (yRot > lateYAddOffset || yRot < lateYSubOffset)
            {
                transform.eulerAngles = newRot;
                Debug.Log("Updated");
            }
            
        }

        lateYAddOffset = target.eulerAngles.y + additionOffset;
        lateYSubOffset = target.eulerAngles.y - subtractionOffset;
        #region TastaturEingabeHilfe
        if (Input.GetKeyDown("h"))
        {
            Debug.Log("X is " + xRot + " Y is " + yRot  + " Z is " + zRot + " and late target is " + (lateTarget.eulerAngles.y + additionOffset));
            //Debug.Log("The rotation is: " + target.rotation);
        }
        if (Input.GetKeyDown("c"))
        {
            Debug.Log("Im comparing yRot: " + yRot + " to addOfset: " + lateYAddOffset + " and subOffset: " + lateYSubOffset);
        }
        #endregion

    }
}
