using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class ControllerInteraction : MonoBehaviour, IInputHandler, IFocusable, IPointerSpecificFocusable
{
    private GameObject clone = null;
    public float SizeFactor = 100.0f;
    //public float maxHitDistance = 500.0f;

    public void makeSmaller(GameObject gameObject)
    {
        Vector3 scale = gameObject.transform.localScale;
        scale /= SizeFactor;
        gameObject.transform.localScale = scale;
    }

    public void OnInputDown(InputEventData eventData)
    {
        
    }

    public void OnInputUp(InputEventData eventData)
    {
        /*if (Variables.spawn == true)
        {
            foreach (GameObject obj in Variables.cloneList)
            {
                Destroy(obj);
            }
            Variables.cloneList.Clear();
        }
        if (Variables.spawn == false)
        {
            Debug.Log("SPAWN");
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
            clone = Instantiate(this.gameObject, spawnPosition, Camera.main.transform.rotation);
            clone.transform.LookAt(Camera.main.transform);
            Variables.cloneList.Add(clone);
        }
        Variables.spawn = !Variables.spawn;*/
    }

    // Use this for initialization
    void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        if (clone != null)
        {
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
            clone.transform.position = spawnPosition;
            clone.transform.LookAt(Camera.main.transform);
        }
    }

    public void OnFocusEnter()
    {
        Debug.Log("HIT");
    }

    public void OnFocusExit()
    {
        Debug.Log("END HIT");
    }

    public void OnFocusEnter(PointerSpecificEventData eventData)
    {
        Debug.Log("HIT");
    }

    public void OnFocusExit(PointerSpecificEventData eventData)
    {
        Debug.Log("END HIT");
    }
}

/*var interactionSourceStates = InteractionManager.GetCurrentReading();
Debug.Log("SELECT");
RaycastHit hitInfo;
if (Physics.Raycast(
        Camera.main.transform.position,
        Camera.main.transform.forward,
        out hitInfo,
        Mathf.Infinity,
        Physics.DefaultRaycastLayers))
{
    foreach (var interactionSourceState in interactionSourceStates)
{
    if (interactionSourceState.selectPressed)
    {
        Debug.Log("HIT  " + Variables.spawn);
        if (Variables.spawn == true)
        {
            foreach (GameObject obj in Variables.cloneList)
            {
                Destroy(obj);
            }
            Variables.cloneList.Clear();
        }
        if (Variables.spawn == false)
        {
            Debug.Log("SPAWN");
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
            clone = Instantiate(this.gameObject, spawnPosition, Camera.main.transform.rotation);
            clone.transform.LookAt(Camera.main.transform);
            Variables.cloneList.Add(clone);
        }
        Variables.spawn = !Variables.spawn;
    }
}

}*/
/*RaycastHit hitInfo;
if (Physics.Raycast(
    Camera.main.transform.position,
    Camera.main.transform.forward,
    out hitInfo,
    Mathf.Infinity,
    Physics.DefaultRaycastLayers))
{
    Debug.Log("HT");*/
/*if (eventData.PressType == InteractionSourcePressInfo.Select)
{
Debug.Log("SELECT");
    if (Variables.spawn == true)
    {
        foreach (GameObject obj in Variables.cloneList)
        {
            Destroy(obj);
        }
        Variables.cloneList.Clear();
    }
    if (Variables.spawn == false)
    {
        Debug.Log("SPAWN");
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
        clone = Instantiate(this.gameObject, spawnPosition, Camera.main.transform.rotation);
        //clone = Instantiate(GameObject.Find("empty"), spawnPosition, Camera.main.transform.rotation);

        //clone.GetComponent<Renderer>().material.mainTexture = destText;
        //clone.transform.localScale = new Vector3((int) zoomWidth/100, (int) zoomHeight/100, 0);

        clone.transform.LookAt(Camera.main.transform);
        //makeSmaller(clone);
        Variables.cloneList.Add(clone);
    }
    Variables.spawn = !Variables.spawn;
}*/
//}

/*
if (Variables.spawn == true)
        {
            foreach (GameObject obj in Variables.cloneList)
            {
                Destroy(obj);
            }
            Variables.cloneList.Clear();
        }
        if (Variables.spawn == false)
        {
            Debug.Log("SPAWN");
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
            clone = Instantiate(this.gameObject, spawnPosition, Camera.main.transform.rotation);
            clone.transform.LookAt(Camera.main.transform);
            Variables.cloneList.Add(clone);
        }
        Variables.spawn = !Variables.spawn;
if (clone != null)
        {
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
            clone.transform.position = spawnPosition;
            clone.transform.LookAt(Camera.main.transform);
        }
*/