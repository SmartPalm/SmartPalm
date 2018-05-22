using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    [Tooltip ("Check this box, to enable a strict traking of the rotation")]
    public bool lookAtTargetStrictly;
    [Tooltip("Check this box, to enable a traking of all directions")]
    public bool useAllDirections;
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
    private float lateXAddOffset;
    private float lateXSubOffset;
    private float lateZAddOffset;
    private float lateZSubOffset;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("ARCamera").transform;

        lateYAddOffset = target.eulerAngles.y + additionOffset;
        lateYSubOffset = target.eulerAngles.y - subtractionOffset;
        lateXAddOffset = target.eulerAngles.x + additionOffset;
        lateXSubOffset = target.eulerAngles.x - subtractionOffset;
        lateZAddOffset = target.eulerAngles.z + additionOffset;
        lateZSubOffset = target.eulerAngles.z - subtractionOffset;
    }
	
	// Update is called once per frame
	void Update () {

        if (gameObject.tag == "Folder")
        {
            xRot = target.eulerAngles.x - 120f;
            yRot = target.eulerAngles.y + 180f;
            zRot = target.eulerAngles.z;
        } else
        {
            xRot = target.eulerAngles.x;
            yRot = target.eulerAngles.y;
            zRot = target.eulerAngles.z;
        }
        
        
        if (!useAllDirections)
        {
            if (gameObject.tag == "Folder")
            {
                newRot = new Vector3(-90, yRot, 0);
            } else
            {
                newRot = new Vector3(0, yRot, 0);
            }
            
        } else
        {
            newRot = new Vector3(xRot, yRot, zRot);
        }
                
        
        

        if (lookAtTargetStrictly)
        {

            transform.eulerAngles = newRot;
            
        } else
        {
            if (useAllDirections)
            {
                if (yRot > lateYAddOffset || yRot < lateYSubOffset || xRot > lateXAddOffset || xRot < lateXSubOffset || zRot > lateZAddOffset || zRot < lateZSubOffset)
                {
                    transform.eulerAngles = newRot;
                    //Debug.Log("Updated");
                }
            } else
            {
                if (yRot > lateYAddOffset || yRot < lateYSubOffset)
                {
                    transform.eulerAngles = newRot;
                    //Debug.Log("Updated");
                }
            }
            
            
        }

        lateYAddOffset = yRot + additionOffset;
        lateYSubOffset = yRot - subtractionOffset;
        lateXAddOffset = target.eulerAngles.x + additionOffset;
        lateXSubOffset = target.eulerAngles.x - subtractionOffset;
        lateZAddOffset = zRot + additionOffset;
        lateZSubOffset = zRot - subtractionOffset;
        #region TastaturEingabeHilfe
        if (Input.GetKeyDown("h"))
        {
            Debug.Log("X is " + xRot + " Y is " + yRot  + " Z is " + zRot);
            //Debug.Log("The rotation is: " + target.rotation);
        }
        if (Input.GetKeyDown("c"))
        {
            Debug.Log("Im comparing yRot: " + yRot + " to addOfset: " + lateYAddOffset + " and subOffset: " + lateYSubOffset);
        }
        #endregion

    }
}
