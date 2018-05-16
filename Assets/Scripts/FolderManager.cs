using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderManager : MonoBehaviour
{
    [HideInInspector]
    public static List<GameObject> Children;
    private GameObject certainFolder;
    private static FolderManager folderManager;
    private bool backToHardDrive = false;

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
        
        /*for ( int counter = 0; counter < Children.Count; counter++)
        {
            Debug.Log(Children[counter]);
        }*/
        
    }


    void Update()
    {
        for (int counter = 0; counter < Children.Count; counter++)
        {
            certainFolder = Children[counter];
            Debug.Log(certainFolder);
            if (certainFolder.transform.position.y > 0.91 || certainFolder.transform.position.y < 0.09 || certainFolder.transform.position.x > 0.3 || certainFolder.transform.position.x > -0.27)
            {
                certainFolder.SetActive(false);
            } else
            {
                certainFolder.SetActive(true);
            }

            if (backToHardDrive)
            {
                certainFolder.transform.position = new Vector3((certainFolder.transform.position.x + 0.01f), certainFolder.transform.position.y, certainFolder.transform.position.z);
            }
        }
    }

    public void goToHardDrive()
    {
        for (int counter = 0; counter < Children.Count; counter++)
        {
            if (Children[counter].tag == "Folder")
            {
                
            }
        }
}
