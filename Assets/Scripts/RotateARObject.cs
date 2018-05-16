using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RotateARObject : MonoBehaviour
{
    public static string TAG_AR_ROTATABLE = "CanRot";
    public GUIText message = null;
    private Transform pickedObject = null;
    private Transform foundObject = null;
    private Vector3 lastPlanePoint, yClamp, rotationVector;
    private Text textField;
    private float lastDelta;

    // Use this for initialization
    void Start()
    {
        //textField = GameObject.Find("TouchStatusText").GetComponent<Text>();
        EventManager.StartListening(TouchController.EVENT_SCROLL_LEFT, onScrollHorizontal);
        EventManager.StartListening(TouchController.EVENT_SCROLL_RIGHT, onScrollHorizontal);
    }

    void onScrollHorizontal(float value) {
        lastDelta = value;

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
                    foundObject = hit.transform;

                    if (foundObject.tag == TAG_AR_ROTATABLE)
                    {
                        pickedObject = hit.transform;
                        lastPlanePoint = planePoint;
                    }
                    //textField.text = "Raycast hit! New object: " + pickedObject + ", Last Plane Point: " + lastPlanePoint;
                }
                else
                {
                    pickedObject = null;
                    foundObject = null;

                    //textField.text = "pickedObject is now null";
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                //textField.text = "Touch Move phase";

                if (pickedObject != null)
                {
                    //textField.text = "Object Pickup successful, object: " + pickedObject.name;

                    pickedObject.Rotate(0, lastDelta * 2, 0);
                    lastPlanePoint = planePoint;
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
