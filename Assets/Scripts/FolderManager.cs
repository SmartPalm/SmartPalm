using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderManager : MonoBehaviour
{
    [HideInInspector]
    public static List<GameObject> Children;
    public static GameObject hDDIcon;
    private GameObject certainFolder;
    private static FolderManager folderManager;
    private bool backToHardDrive = false;
    private bool forwardToFolder = false;

    #region static functions
    public static FolderManager instance
    {
        get
        {
            if (!folderManager)
            {
                folderManager = FindObjectOfType(typeof(FolderManager)) as FolderManager;

                if (!folderManager)
                {
                    Debug.LogError("There needs to be one active FolderManager script on a GameObject in your scene.");
                }
                else
                {
                    folderManager.Init();
                }
            }

            return folderManager;
        }
    }

    void Init()
    {
        if (certainFolder == null)
        {
            certainFolder = new GameObject();
        }

        if (Children == null)
        {
            Children = new List<GameObject>();
        }
    }
    #endregion

    void Start()
    {
        Init();
        foreach (Transform child in transform)
        {
            //Debug.Log(child);
            if (child.gameObject.tag == "Folder")
            {
                Children.Add(child.gameObject);
            }
        }

        hDDIcon = GameObject.Find("hdd");

        /*for ( int counter = 0; counter < Children.Count; counter++)
        {
            Debug.Log(Children[counter]);
        }*/
        
    }


    void Update()
    {
        if (hDDIcon.transform.localPosition.x > 0.3f || hDDIcon.transform.localPosition.x < -0.27f)
        {
            hDDIcon.SetActive(false);
        }
        else
        {
            hDDIcon.SetActive(true);
        }


        if (hDDIcon.transform.localPosition.x >= 0f && backToHardDrive)
        {
            backToHardDrive = false;
            //Debug.Log("Changed");
        }
        if (hDDIcon.transform.localPosition.x <= -0.3f && forwardToFolder)
        {
            forwardToFolder = false;
        }
        moveToPosition(hDDIcon);

        for (int counter = 0; counter < Children.Count; counter++)
        {
            certainFolder = Children[counter];
            //Debug.Log(certainFolder);
            if (certainFolder.transform.localPosition.y > 0.91f || certainFolder.transform.localPosition.y < 0.09f || certainFolder.transform.localPosition.x > 0.3f || certainFolder.transform.localPosition.x < -0.27f)
            {
                certainFolder.SetActive(false);
            } else
            {
                certainFolder.SetActive(true);
            }

            if (certainFolder.transform.localPosition.x >= 0.3f && backToHardDrive)
            {
                backToHardDrive = false;
                //Debug.Log("Changed by folder with " + certainFolder + " because its position was " + certainFolder.transform.localPosition.x);
            }
            if (certainFolder.transform.localPosition.x <= 0f && forwardToFolder)
            {
                forwardToFolder = false;
            }

            moveToPosition(certainFolder);
        }

        if (Input.GetKeyDown("m"))
        {
            goToHardDrive(); 
        }

        if (Input.GetKeyDown("n"))
        {
            Debug.Log("Called");
            goToFolder();
        }

    }

    private void moveToPosition (GameObject movingObj)
    {
        if (backToHardDrive)
        {
            //Debug.Log("Called for " + movingObj);
            movingObj.transform.localPosition = new Vector3((movingObj.transform.localPosition.x + 0.01f), movingObj.transform.localPosition.y, movingObj.transform.localPosition.z);
        } else if (forwardToFolder)
        {
            //Debug.Log("Called to move forward " + movingObj);
            movingObj.transform.localPosition = new Vector3((movingObj.transform.localPosition.x - 0.01f), movingObj.transform.localPosition.y, movingObj.transform.localPosition.z);
        }
        
    }

    public void goToHardDrive()
    {
        backToHardDrive = true;
    }

    public void goToFolder()
    {
        forwardToFolder = true;
    }
}
