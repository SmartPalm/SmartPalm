using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderManager : MonoBehaviour
{
    
    
    
    [HideInInspector]
    public static string state;
    [Range (0f, 1.0f)]
    public float chosenOffset;

    private List<GameObject> Children = new List<GameObject>();
    private GameObject chosenFile;
    private GameObject certainFolder;
    private GameObject animationController;
    private Dictionary<GameObject, float> DistanceDic = new Dictionary<GameObject, float>();
    private Material standard;
    private Material chosen;
    private bool isOnObject;
    private bool printFolder = false;
    //private float directionSpeed;


    void Start()
    {
        foreach (Transform child in transform)
        {
            //Debug.Log(child);
            if (child.gameObject.tag == "Folder" || child.gameObject.tag == "video" || child.gameObject.tag == "bluetooth" || child.gameObject.tag == "audio" || child.gameObject.tag == "document")
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
        isOnObject = false;
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
            key.transform.Find("DescriptionText").gameObject.SetActive(false);
            if (DistanceDic[chosenFile] > DistanceDic[key] && GameObject.Find("ARCamera").transform.position.z < -10)
            {
                chosenFile = key;
            } else if(DistanceDic[chosenFile] < DistanceDic[key] && GameObject.Find("ARCamera").transform.position.z > -10)
            {
                chosenFile = key;
            }
        }

        chosenFile.GetComponent<Renderer>().material = chosen;
        chosenFile.transform.Find("DescriptionText").gameObject.SetActive(true);
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

        if (GameObject.Find(state).GetComponent<DirectoryPathScript>().isNewDirectory && chosenFile.tag == "Folder")
        {
            callAnimation(state);

            state = chosenFile.GetComponent<DirectoryPathScript>().directory;
            printState();
            GameObject.Find("DataManager").GetComponent<DataManager>().setGameObject(state);

            Debug.Log("Selected Folder: " + chosenFile + " and the new directotry is: " + chosenFile.GetComponent<DirectoryPathScript>().directory + " and the GameObject in Focus is: " + GameObject.Find("DataManager").GetComponent<DataManager>().currentDisplayedObject);
            reverseAnimation(state);
        }
        else if (chosenFile.tag == "video")
        {
            printFixedState("video");
            GameObject.Find("VideoManager").GetComponent<VideoManager>().callMethodForGameObject(chosenFile);
        }
        else if (chosenFile.tag == "bluetooth")
        {
            printFixedState("bluetooth");
            GameObject.Find("BluetoothManager").GetComponent<NativeAndroidBluetooth>().callMethodForGameObject(chosenFile);
        }
        else if (chosenFile.tag == "audio")
        {
            printFixedState("audio");
            GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().callMethodForGameObject(chosenFile);
        }
        else if (chosenFile.tag == "document")
        {
            printFixedState("document");
            GameObject.Find("DocumentManager").GetComponent<DocumentManager>().callMethodForGameObject(chosenFile);
        }
    }

    // Changes the backToMenu bool into true
    public void backToMenu()
    {
        if (state != name)
        {
            callAnimation(state);
            reverseAnimation(name);
            state = name;
            printState();
            GameObject.Find("DataManager").GetComponent<DataManager>().setGameObject(state);
        }
        else if (state == name && state != "menu")
        {
            callAnimation(state);
            reverseAnimation("menu");
            state = "menu";
            printState();
            GameObject.Find("DataManager").GetComponent<DataManager>().setGameObject(state);
        }
    }

    public void moveToilet()
    {
        callAnimation("toilet");
    }

    public void logState()
    {
        
        GameObject.Find("Debug").GetComponent<Text>().text = state;
    }

    public void printState()
    {
        Debug.Log("The state is " + state + ", the chosenFile is at this time " + chosenFile + " and was brought to you by " + this);
        logState();
    }

    public void printFixedState(string typeOfAction)
    {
        if (typeOfAction == "video")
        {
            Debug.Log("The state is 'playing a video', the chosenFile is at this time " + chosenFile + " and was brought to you by " + this);
            GameObject.Find("Debug").GetComponent<Text>().text = "playing Video";
        } else if (typeOfAction == "bluetooth")
        {
            Debug.Log("The state is 'connecting', the chosenFile is at this time " + chosenFile + " and was brought to you by " + this);
            GameObject.Find("Debug").GetComponent<Text>().text = "connecting...";
        } else if (typeOfAction == "audio")
        {
            Debug.Log("The state is 'playing audio', the chosenFile is at this time " + chosenFile + " and was brought to you by " + this);
            GameObject.Find("Debug").GetComponent<Text>().text = "playing audio";
        }
    }
}
