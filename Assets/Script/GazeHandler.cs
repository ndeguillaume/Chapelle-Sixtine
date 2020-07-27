using UnityEngine;
using UnityEngine.Windows.Speech;
using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Renderer))]

public class GazeHandler : MonoBehaviour
{
    public GameObject toDuplicate;
    public Variables variables;
    public GameObject player;
    public GameObject playerParent;
    private float distance;
    private GameObject tmp = null;
    public bool focusIn = false;
    private GameObject hitGameObject = null;
    private float y;

    [HideInInspector]
    private bool boolDuplicated = false;
    [HideInInspector]
    private int duplicateFocus = 0 ;

    public bool onFloor = false;


    void Start()
    {
        variables = player.GetComponent<Variables>();
    }

    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 forwardDirection = player.transform.forward;
        Ray interactionRay = new Ray(playerPosition, forwardDirection);
        RaycastHit interactionRayHit;
        float interactionRayLength =100.0f;
        Vector3 interactionRayEndPoint = forwardDirection * interactionRayLength;
  //      Debug.DrawLine(playerPosition, interactionRayEndPoint, Color.green);
        bool hitFound = Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength);
        if (hitFound)
        { 
            if (interactionRayHit.transform.gameObject.name=="stair1")
            {
                hitFound = false;
                if (variables.go)
                {
                   if (playerParent.transform.position.y == -1.941f)
                    {
                        y = playerParent.transform.position.y + 0.461f;
                    }
                   else
                     y = playerParent.transform.position.y;
                    
                    playerParent.transform.position = interactionRayHit.point;
                    playerParent.transform.position = new Vector3(player.transform.position.x, y , player.transform.position.z);
                    variables.go = false;
                    Debug.Log("ray pos : " + interactionRayHit.point);
                }
            }
            if (interactionRayHit.transform.gameObject.name=="Floor")
            {
                hitFound = false;
                if (variables.go)
                {
                    if (playerParent.transform.position.y == -1.941f + 0.461f)
                        y = playerParent.transform.position.y - 0.461f;
                    else
                        y = playerParent.transform.position.y;

                    playerParent.transform.position = interactionRayHit.point;
                    playerParent.transform.position = new Vector3(player.transform.position.x, y, player.transform.position.z);
                    variables.go = false;
                    Debug.Log("ray pos : " + interactionRayHit.point);
                }
            }
            else if (interactionRayHit.transform.gameObject != variables.focusedGameObject)
             focusIn = false;

        }

        if (variables.go)
            variables.go = false;
            
        if (hitFound && !focusIn)
        {
            
            distance = interactionRayHit.distance * 0.3f;
            hitGameObject = interactionRayHit.transform.gameObject;
         
            focusIn = true;
            variables.focusedGameObject = hitGameObject;
            variables.texture = hitGameObject.GetComponent<Renderer>().material.mainTexture;
            if (hitGameObject.name == "Closer")
            {
                duplicateFocus = 1;
            }
            else
                duplicateFocus = -1;
        }
        else if (!hitFound && focusIn)
        {
            focusIn = false;
            if (variables.focusedGameObject.name == "Closer")
            {
                duplicateFocus = -1;
            }
            variables.focusedGameObject = null;
        }
        if (focusIn)
        {
           if (variables.closer &&  !boolDuplicated) 
         //  if (Input.GetKeyDown(KeyCode.Space) && !boolDuplicated)
            {
               
                boolDuplicated = true;
                Vector3 camera = player.transform.position;
                Vector3 cameraDirection = player.transform.forward;
                Quaternion cameraRotation = player.transform.rotation;
                Vector3 spawnPos = camera + cameraDirection * 2.5f; // * distance;
                spawnPos.y = player.transform.position.y;
              //  spawnPos.x += 2f;
                  tmp =Instantiate(toDuplicate, spawnPos, cameraRotation);
                tmp.name = "Closer";
                tmp.AddComponent<MeshCollider>();
                tmp.transform.localScale = new Vector3((variables.texture.width * (variables.focusedGameObject.transform.localScale.x)) / 300, (variables.texture.height * (variables.focusedGameObject.transform.localScale.y)) / 300, tmp.transform.localScale.z);
                tmp.GetComponent<Renderer>().material.mainTexture = variables.texture;
                tmp.GetComponent<Renderer>().material.mainTexture.name = variables.texture.name;
                tmp.transform.eulerAngles = new Vector3(0f, tmp.transform.eulerAngles.y+180,tmp.transform.eulerAngles.z);
                variables.closer = false;
                 variables.focusedGameObject = tmp;
            }


            if (variables.focusedGameObject.name == "Selected" || variables.focusedGameObject.name == "Closer")
            {
              
                if (variables.bigger && focusIn && boolDuplicated)
                //if (Input.GetKeyDown(KeyCode.O) && focusIn && boolDuplicated)
                {
                    variables.focusedGameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
                    variables.bigger = false;
                }
                if (variables.smaller && focusIn && boolDuplicated)
               // if (Input.GetKeyDown(KeyCode.P) && focusIn && boolDuplicated)
                {
                    if (variables.focusedGameObject.transform.localScale.x > 0.5f && variables.focusedGameObject.transform.localScale.y > 0.5f && variables.focusedGameObject.transform.localScale.z > 0.5f)
                    {
                        variables.focusedGameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                    }

                    variables.smaller = false;
                }
            }

            if (variables.focusedGameObject.name == "Closer")
            {
                if (variables.delete)
               // if (Input.GetKeyDown(KeyCode.U))
                 {
                    Destroy(variables.focusedGameObject);
                    boolDuplicated = false;
                    duplicateFocus = 0;
                    variables.delete = false;
                    focusIn = false;
                 }

            if (variables.faceMe)
            //  if (Input.GetKeyDown(KeyCode.F))
               {
                  variables.focusedGameObject.transform.LookAt(player.transform);
                  variables.faceMe = false;
               }
            }   
        }
    }
}