using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveARObject : MonoBehaviour
{
    public GUIText message = null;
    private Transform pickedObject = null;
    private Vector3 lastPlanePoint, yClamp;
    private Text textField;

    // Use this for initialization
    void Start()
    {
        //textField = GameObject.Find("TouchStatusText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Plane targetPlane = new Plane(transform.up, transform.position);
        foreach (Touch touch in Input.touches)
        {
            //textField.text = "Touch recognized";

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            float dist = 0.0f;
            targetPlane.Raycast(ray, out dist);
            Vector3 planePoint = ray.GetPoint(dist);

            if (touch.phase == TouchPhase.Began)
            {
                //textField.text = "Touch phase begin";

                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    pickedObject = hit.transform;
                    lastPlanePoint = planePoint;

                    //textField.text = "Raycast hit! New object: " + pickedObject + ", Last Plane Point: " + lastPlanePoint;
                }
                else
                {
                    pickedObject = null;

                    //textField.text = "pickedObject is now null";
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                //textField.text = "Touch Move phase";

                if (pickedObject != null)
                {
                    //textField.text = "Object Pickup successful, object: " + pickedObject.name;
                    for (int counter = 0; counter < FolderManager.Children.Count; counter++)
                    {
                        if (lastPlanePoint == null)
                        {
                            lastPlanePoint = planePoint;
                        }
                        FolderManager.Children[counter].transform.position = planePoint - lastPlanePoint;
                        lastPlanePoint = planePoint;
                    }
                    
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                //textField.text = "Touch ended";

                pickedObject = null;
            }
        }
    }
}