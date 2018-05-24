using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderManager : MonoBehaviour
{
    [HideInInspector]
    public static List<GameObject> Children;
    [HideInInspector]
    public static GameObject chosenFile;
    [HideInInspector]
    public static string state;
    [Range (0f, 1.0f)]
    public float chosenOffset;
    private GameObject certainFolder;
    private GameObject animationController;
    private static FolderManager folderManager;
    private Dictionary<GameObject, float> DistanceDic = new Dictionary<GameObject, float>();
    private Material standard;
    private Material chosen;
    private bool printFolder = false;
    //private float directionSpeed;

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
                saveComparedPositionToCamera(child.gameObject);
            }
        }

        animationController = GameObject.Find("AnimationController");
        chosenFile = Children[0];
        state = "menu";
        standard = (Material)Resources.Load("Glassy", typeof(Material));
        chosen = (Material)Resources.Load("Chosen", typeof(Material));
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
            if(printFolder)
            {
                Debug.Log(certainFolder.transform.position.z);
            }
            //Debug.Log(certainFolder);

            if (certainFolder.transform.position.z < transform.position.z - chosenOffset)
            {
                certainFolder.GetComponent<Renderer>().material = chosen;
            } else
            {
                certainFolder.GetComponent<Renderer>().material = standard;
            }
            changeComparedPositionToCamera(certainFolder);
        }

        comparePositionsComparedToCamera();
        #region TastaturEingabeHilfe
        if (Input.GetKeyDown("m"))
        {
            makeSelection();
        }

        if (Input.GetKeyDown("n"))
        {
            //Debug.Log("Called");
            backToMenu();
        }

        if (Input.GetKeyDown("t"))
        {
            printFolder = !printFolder;
        }
        #endregion

    }

    // Fills the dictionary with files
    private void saveComparedPositionToCamera(GameObject file) 
    {
        //Debug.Log("We use " + file + " and the position of this " + GameObject.Find("ARCamera").transform.position.z);
        DistanceDic.Add(file, GameObject.Find("ARCamera").transform.position.z + file.transform.position.z);
    }

    // Updates the position of every file every update
    private void changeComparedPositionToCamera(GameObject file)
    {
        DistanceDic[file] = GameObject.Find("ARCamera").transform.position.z + file.transform.position.z;
    }

    // Compares every position and colors the nearest to the camerea blue
    private void comparePositionsComparedToCamera()
    {
        foreach (GameObject key in DistanceDic.Keys)
        {
            key.GetComponent<Renderer>().material = standard;
            if(DistanceDic[chosenFile] > DistanceDic[key] && GameObject.Find("ARCamera").transform.position.z < -10)
            {
                chosenFile = key;
            } else if(DistanceDic[chosenFile] < DistanceDic[key] && GameObject.Find("ARCamera").transform.position.z > -10)
            {
                chosenFile = key;
            }
        }

        chosenFile.GetComponent<Renderer>().material = chosen;
    }

    private void callAnimation(string ani)
    {
        animationController.GetComponent<AnimationControllerScript>().setFocusAnimation(ani);
        animationController.GetComponent<AnimationControllerScript>().playFocusAnimation();
    }

    private void reverseAnimation(string ani)
    {
        animationController.GetComponent<AnimationControllerScript>().setFocusAnimation(ani);
        animationController.GetComponent<AnimationControllerScript>().playFocusAnimationReversed();
    }

    // Changes the forwardToSelection bool into true
    public void makeSelection()
    {
        if (GameObject.Find(state).GetComponent<DirectoryPathScript>().isNewDirectory)
        {
            callAnimation(state);
            Debug.Log("Selected Folder: " + chosenFile);
            state = chosenFile.GetComponent<DirectoryPathScript>().directory;
            reverseAnimation(state);
        }
    }

    // Changes the backToMenu bool into true
    public void backToMenu()
    {
        if (state != "menu")
        {
            callAnimation(state);
            reverseAnimation("menu");
            state = "menu";
        }
    }

    public void moveToilet()
    {
        callAnimation("toilet");
    }
}
