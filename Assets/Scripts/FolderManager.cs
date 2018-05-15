using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderManager : MonoBehaviour
{
    [HideInInspector]
    public static List<GameObject> Children;
    private GameObject certainFolder;
    private static FolderManager folderManager;

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

    // Use this for initialization
    void Start()
    {
        Init();
        foreach (Transform child in transform)
        {
            Debug.Log(child);
            Children.Add(child.gameObject);
        }
        
        for ( int counter = 0; counter < Children.Count; counter++)
        {
            Debug.Log(Children[counter]);
        }
        
    }



    // Update is called once per frame
    void Update()
    {
        /*for (int counter = 0; counter < Children.Count; counter++)
        {
            certainFolder = Children[counter];
            Debug.Log(certainFolder);
            if (certainFolder.transform.position.y > 0.91 || certainFolder.transform.position.y < 0.09)
            {
                certainFolder.SetActive(false);
            } else
            {
                certainFolder.SetActive(true);
            }
        }*/
    }
}
