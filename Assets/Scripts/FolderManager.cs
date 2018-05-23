using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderManager : MonoBehaviour
{
    [HideInInspector]
    public static List<GameObject> Children;
    [HideInInspector]
    public static GameObject chosenFile;
    public static GameObject hDDIcon;
    [Range (0f, 1.0f)]
    public float chosenOffset;
    public bool widerScroll = false;
    private GameObject certainFolder;
    private GameObject animationController;
    private static FolderManager folderManager;
    private Dictionary<GameObject, float> DistanceDic = new Dictionary<GameObject, float>();
    private GameObject best;
    private Material standard;
    private Material chosen;
    private bool backToHardDrive = false;
    private bool forwardToFolder = false;
    private bool printFolder = false;
    private float borderLeft = -0.27f;
    private float borderRight = 0.3f;
    private float borderAbove = 0.91f;
    private float borderBelow = 0.09f;
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
        best = Children[0];
        hDDIcon = GameObject.Find("hdd");
        standard = (Material)Resources.Load("Glassy", typeof(Material));
        chosen = (Material)Resources.Load("Chosen", typeof(Material));
        /*for ( int counter = 0; counter < Children.Count; counter++)
        {
            Debug.Log(Children[counter]);
        }*/

    }


    void Update()
    {   
        if(widerScroll)
        {
            borderLeft = -0.7f;
            borderRight = 0.7f;
            borderAbove = 3f;
            borderBelow = -3f;
        } else
        {
            borderLeft = -0.27f;
            borderRight = 0.3f;
            borderAbove = 0.91f;
            borderBelow = 0.09f;
        }

        if (hDDIcon.transform.localPosition.x > borderRight || hDDIcon.transform.localPosition.x < borderLeft)
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
            Debug.Log("Changed by hdd");
        }
        if (hDDIcon.transform.localPosition.x <= -0.3f && forwardToFolder)
        {
            forwardToFolder = false;
        }
        moveToPosition(hDDIcon);

        for (int counter = 0; counter < Children.Count; counter++)
        {
            certainFolder = Children[counter];
            if(printFolder)
            {
                Debug.Log(certainFolder.transform.position.z);
            }
            //Debug.Log(certainFolder);
            /*if (certainFolder.transform.localPosition.y > borderAbove || certainFolder.transform.localPosition.y < borderBelow || certainFolder.transform.localPosition.x > borderRight|| certainFolder.transform.localPosition.x < borderLeft)
            {
                
                certainFolder.SetActive(false);
            }
            else
            {
                certainFolder.SetActive(true);
            }
            

            if (certainFolder.transform.localPosition.x >= 0.3f && backToHardDrive)
            {
                Debug.Log("Reseted by folders");
                backToHardDrive = false;
                //Debug.Log("Changed by folder with " + certainFolder + " because its position was " + certainFolder.transform.localPosition.x);
            }
            if (certainFolder.transform.localPosition.x <= 0f && forwardToFolder)
            {
                forwardToFolder = false;
            }*/

            moveToPosition(certainFolder);

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
            if(DistanceDic[best] > DistanceDic[key] && GameObject.Find("ARCamera").transform.position.z < -10)
            {
                best = key;
            } else if(DistanceDic[best] < DistanceDic[key] && GameObject.Find("ARCamera").transform.position.z > -10)
            {
                best = key;
            }
        }

        best.GetComponent<Renderer>().material = chosen;
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

    // Changes the backtohdd bool into true
    public void goToHardDrive()
    {
        backToHardDrive = true;
    }

    // Changes the forwardtofolder bool into true
    public void goToFolder()
    {
        forwardToFolder = true;
    }

    // Changes the forwardToSelection bool into true
    public void makeSelection()
    {
        callAnimation("menu");
    }

    // Changes the backToMenu bool into true
    public void backToMenu()
    {
        reverseAnimation("menu");
    }

    public void moveToilet()
    {
        callAnimation("toilet");
    }
}
