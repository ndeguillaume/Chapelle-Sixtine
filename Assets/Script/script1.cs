using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class script1 : MonoBehaviour, IFocusable {

    public bool isFocus = false;
    public GameObject clone = null;
    private List<GameObject> cloneList = new List<GameObject>();

    public void OnFocusEnter()
    {
        isFocus = true;
        throw new System.NotImplementedException();
    }

    public void OnFocusExit()
    {
        isFocus = false;
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
        //father = this.gameObject;
	}

    // Update is called once per frame
    void Update()
    {
		var interactionSourceStates = InteractionManager.GetCurrentReading();
        foreach (var interactionSourceState in interactionSourceStates)
        {
            if (interactionSourceState.selectPressed)
            {
                Debug.Log("selectPressed");
                var headPose = interactionSourceState.headPose;
                RaycastHit raycastHit;
                if (Physics.Raycast(headPose.position, headPose.forward, out raycastHit, 10))
                {
                    Debug.Log("Clone creation");
                    var targetObject = raycastHit.collider.gameObject;
                    Vector3 spawnPos = new Vector3(headPose.position.x + 0.5F, headPose.position.y + 0.5F, headPose.position.z + 0.5F);
                    Vector3 spawnRotation = new Vector3(headPose.rotation.x, headPose.rotation.y, headPose.rotation.z);
                    Vector3 objectRotation = new Vector3(targetObject.transform.rotation.x, targetObject.transform.rotation.y, targetObject.transform.rotation.z);
                    //Instantiate(targetObject, spawnPos, Quaternion.FromToRotation(objectRotation, spawnRotation));
                    Instantiate(targetObject, spawnPos, Quaternion.FromToRotation(objectRotation, spawnRotation));
                }
            }
        }
        /*if (isFocus && Input.GetButton("Submit"))
        {
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 10.0F;
            clone = Instantiate(this.gameObject, spawnPosition, Camera.main.transform.rotation);
            clone.transform.LookAt(Camera.main.transform);
            cloneList.Add(clone);
        }
        else if (!isFocus && cloneList.Count > 0)
        {
            cloneList.Clear();
        }*/
		
		

    }
}

//public GameObject[] clones
//public GameObject[] clones = new GameObject[] { };
//public GameObject father = null;
//public GameObject clone = null;
/*
var interactionSourceStates = InteractionManager.GetCurrentReading();
        foreach (var interactionSourceState in interactionSourceStates)
        {
            if (interactionSourceState.selectPressed)
            {
                Debug.Log("selectPressed");
                var headPose = interactionSourceState.headPose;
                RaycastHit raycastHit;
                if (Physics.Raycast(headPose.position, headPose.forward, out raycastHit, 10))
                {
                    Debug.Log("Clone creation");
                    var targetObject = raycastHit.collider.gameObject;
                    Vector3 spawnPos = new Vector3(headPose.position.x + 0.5F, headPose.position.y + 0.5F, headPose.position.z + 0.5F);
                    Vector3 spawnRotation = new Vector3(headPose.rotation.x, headPose.rotation.y, headPose.rotation.z);
                    Vector3 objectRotation = new Vector3(targetObject.transform.rotation.x, targetObject.transform.rotation.y, targetObject.transform.rotation.z);
                    //Instantiate(targetObject, spawnPos, Quaternion.FromToRotation(objectRotation, spawnRotation));
                    Instantiate(targetObject, spawnPos, Quaternion.FromToRotation(objectRotation, spawnRotation));
                }
            }
        }
        */
//RaycastHit hitInfo;
//var interactionSourceStates = InteractionManager.GetCurrentReading();
//father = this.gameObject;

//if (Physics.Raycast(
//        Camera.main.transform.position,
//        Camera.main.transform.forward,
//        out hitInfo,
//        20.0f,
//        Physics.DefaultRaycastLayers))
//{
//    if (Input.GetButton("Submit")) {
/*Vector3 playerPos = Camera.main.transform.position;
    Vector3 playerDirection = Camera.main.transform.forward;
    Quaternion playerRotation = Camera.main.transform.rotation;
    float spawnDistance = 10;

    Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

    Instantiate(clone, spawnPos, playerRotation);*/

//Instantiate(clone, new Vector3(Camera.main.transform.position.x + 5, Camera.main.transform.position.y, Camera.main.transform.position.z), Quaternion.identity);
//Instantiate(clone, new Vector3(Camera.main.transform.position.x + 5, Camera.main.transform.position.y, Camera.main.transform.position.z), Quaternion.Inverse(Camera.main.transform.rotation));
//Instantiate(clone, new Vector3(5, 5, 5), Quaternion.Inverse(Camera.main.transform.rotation));
//Quaternion qqq = new Quaternion(0.2F, 0.2F, 0.2F, 0);
//Instantiate(clone, new Vector3(0, 5, 0),qqq);
//Debug.Log("ROTATION : " + Camera.main.transform.rotation);
//Quaternion qqq = new Quaternion(0, 0, 0, 0);
//Instantiate(this.gameObject, new Vector3(0, 5, 0),qqq);

//Vector3 gazeDirection = Camera.main.transform.forward;
//this.transform.rotation = Quaternion.LookRotation(gazeDirection);

//Vector3 devicePosition = Camera.main.transform.position;
//this.transform.position = new Vector3(devicePosition.x, devicePosition.y, devicePosition.z + 2);
//this.transform.position = new Vector3(0, 0, 0);

//Vector3 devicePosition = Camera.main.transform.position;
//GameObject clone = this.gameObject;
//clone.transform.localScale -= new Vector3(0.5F, 0F, 0F);
//transform.localScale += new Vector3(0.1F, 0, 0);
//Quaternion quaternion = Camera.main.transform.rotation;
//quaternion.z = 0.9F;
//quaternion.x = 0F;
//quaternion.y = 0F;
//Instantiate(clone, new Vector3(devicePosition.x, devicePosition.y, devicePosition.z), this.transform.rotation);

//GameObject temp = father;
//clones[clones.Length] = temp;

//    clone = father;
//    clone.SetActive(false);
//    Instantiate(clone, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z), father.transform.rotation);
//    while(Input.GetButton("Submit")) {
//        clone.SetActive(true);
//    }
//    Destroy(clone);

//InteractionManager.InteractionSourcePressed += InteractionManager_InteractionSourcePressed;

/*void InteractionManager_InteractionSourcePressed(InteractionSourcePressedEventArgs args)
{
    // Source was pressed. This will be after the source was detected and before it is 
    // released or lost
    // args.state has the current state of the source including id, position, kind, etc.
    var interactionSourceState = args.state;
    var headPose = interactionSourceState.headPose;
    RaycastHit raycastHit;
    if (Physics.Raycast(headPose.position, headPose.forward, out raycastHit, 10))
    {
        Debug.Log("METHOD");
        var targetObject = raycastHit.collider.gameObject;
        //Instantiate(targetObject, headPose.position, headPose.rotation);
    }
}*/

/*
 * using UnityEngine;
using UnityEngine.Windows.Speech;
using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Renderer))]

public class GazeHandler : MonoBehaviour, IFocusable
{

   
    public GameObject player;
    public GameObject that;
    public Transform playerT;
    public Texture texture;
    private List<GameObject> duplicated = new List<GameObject>();

    public float distance = 10.0f;
    GameObject tmp = null;
    private bool FocusIn = false;


    void Start()
    {
  
    }
    

    void IFocusable.OnFocusEnter()
    {
        FocusIn = true;
        
        
    }
    void IFocusable.OnFocusExit()
    {
        FocusIn = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && FocusIn)
        {
            Vector3 camera = player.transform.position;
            Vector3 cameraDirection = player.transform.forward;
            Quaternion cameraRotation = player.transform.rotation;
            Vector3 spawnPos = camera + cameraDirection * distance;
            tmp = Instantiate(that, spawnPos, cameraRotation);
            tmp.GetComponent<Renderer>().material.mainTexture = texture;
            tmp.transform.LookAt(playerT);
            duplicated.Add(tmp);
            Debug.Log(that.name + " duplicated ");
        }
        else if (!FocusIn && duplicated.Count > 0)
        {
            //foreach (GameObject d in duplicated) {
           //     Destroy(d);
            //}
            duplicated.Clear();
        }

    }
}

    */
