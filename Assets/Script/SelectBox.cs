using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class SelectBox : MonoBehaviour
{

    // SCRIPT POINT  : being able to select a region of a painting and display it in front of the player 

    [SerializeField]
    private RectTransform selectSquareImage; // Selection box

    public GameObject player; // Mixed Reality Camera
    public GameObject toDuplicate; // 100x100 3D model
    public float distance = 1.0f; // Distance to create a cut painting from

    // Scripts to access to in order to use various variables 

    public Variables variables; 
    public GazeHandler gazeHandlerToAccess = null;
    /////////////////////////////////////////////////////////////////////////////////

    public static bool paintingFocused; // Check if we are looking at a painting
    public bool boolFirstPoint = false; // Check if the first point of the selection square is set
    
    private List<GameObject> duplicatedGameObjects = new List<GameObject>(); // Store the cut paitings in it


    Vector3 startPos = Vector3.zero; // 1st point of the selection box
    Vector3 endPos; // 4th point of the selection box
    Vector2 firstPoint; // Position of the startPos in the array of the texture's pixels
    Vector2 lastPoint; // Position of the endPos in the array of the texture's pixels

    private bool boolFirstPointAnatomy = false;
    private bool boolLastPointAnatomy = false;
    private bool anatomy = false;
    private Dictionary<string,string> map = new Dictionary<string, string>();



    // Use this for initialization
    void Start()
    {
        variables = player.GetComponent<Variables>(); // Accessing to the script named SpeechRecognition associated to the player (Mixed Reality Camera)
        gazeHandlerToAccess = player.GetComponent<GazeHandler>();
        selectSquareImage.gameObject.SetActive(true); // enable the selection box for testing purposes 

        map.Add("lybianSybil", "lybianSybil_anatomy");
        map.Add("god divides light from darkness", "separationOfTheLightFromDarkness_anatomy");
        map.Add("god creates eve", "creationOfEve_anatomy");
        map.Add("family4", "family4_anatomy");

        //   selectSquareImage.transform.position = player.transform.position + player.transform.forward
    }

    // Update is called once per frame
    void Update()
    {

        paintingFocused = variables.focusedGameObject != null; // Is a painting focused ?  
        if (paintingFocused)
        {
            string focusedTexture = variables.focusedGameObject.GetComponent<Renderer>().material.mainTexture.name;
            if (focusedTexture == "lybianSybil" || focusedTexture == "god divides light from darkness" || focusedTexture == "god creates eve" || focusedTexture == "family4")
                anatomy = true;
            else
                anatomy = false;

            if (variables.select && !boolFirstPoint && startPos==Vector3.zero)
        //    if (Input.GetKeyDown(KeyCode.B) && !boolFirstPoint)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f)), out hit, Mathf.Infinity))
                {
                    startPos = hit.point;
                    firstPoint = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
                    Debug.Log("Bottom left point : (" + firstPoint.x + "," + firstPoint.y + ")");
                }
                if (anatomy)
                {
                    switch (variables.texture.name)
                    {
                        case "lybianSybil":
                            if (firstPoint.x < 0.55f && firstPoint.x > 0.34f && firstPoint.y > 0.25f && firstPoint.y < 0.35f)
                            {
                                Debug.Log("debut ok");
                                boolFirstPointAnatomy = true;
                            }
                            break;
                        case "god divides light from darkness":
                            if ( firstPoint.x < 0.55f && firstPoint.x > 0.45f && firstPoint.y > 0.30f && firstPoint.y < 0.40f)
                            {
                                Debug.Log("debut2 ok");
                                boolFirstPointAnatomy = true;
                            }
                            break;
                        case "god creates eve":
                            if (firstPoint.x < 0.55f && firstPoint.x > 0.45f && firstPoint.y > 0.25f && firstPoint.y < 0.32f)
                            {
                                Debug.Log("debut3 ok");
                                boolFirstPointAnatomy = true;
                            }
                            break;
                        case "family4":
                            if (firstPoint.x < 0.23f && firstPoint.x > 0.05f && firstPoint.y > 0.01f && firstPoint.y < 0.17f)
                            {
                                Debug.Log("debut4 ok");
                                boolFirstPointAnatomy = true;
                            }
                            break;
                    }
                }
            }

          if (!variables.ok && variables.select)
       //    if (Input.GetKey(KeyCode.B))
            {
                if (!selectSquareImage.gameObject.activeInHierarchy)
                {
                    selectSquareImage.gameObject.SetActive(true);
                }
                selectSquareImage.gameObject.SetActive(true);
                endPos = new Vector3(Screen.width / 2, Screen.height / 2, 0f);

                Vector3 squareStart = Camera.main.WorldToScreenPoint(startPos);
                squareStart.z = 0f;

                Vector3 centre = (squareStart + endPos) / 2f;

                selectSquareImage.position = centre;

                float sizeX = Mathf.Abs(squareStart.x - endPos.x);
                float sizeY = Mathf.Abs(squareStart.y - endPos.y);

                selectSquareImage.sizeDelta = new Vector2(sizeX, sizeY);
            }

            if (variables.ok && variables.select)
         //  if (Input.GetKeyUp(KeyCode.B))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f)), out hit, Mathf.Infinity))
                {
                    // lastPoint.x = firstPoint.y / 1000.0f
                    lastPoint = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
                //    Debug.Log("x : " + lastPoint.x + " y : " + lastPoint.y);
                    if ( anatomy && lastPoint.x < 0.90f && lastPoint.x > 0.72f && lastPoint.y > 0.60f && lastPoint.y < 0.7f)
                    {
                      //  Debug.Log("lastpointOk");
                        boolLastPointAnatomy = true;
                    }
                    if (anatomy)
                    {
                        switch (variables.texture.name)
                        {
                            case "lybianSybil":
                                if ( lastPoint.x < 0.90f && lastPoint.x > 0.72f && lastPoint.y > 0.60f && lastPoint.y < 0.74f)
                                {
                                    boolLastPointAnatomy = true;
                                  }
                                break;
                            case "god divides light from darkness":
                                if (lastPoint.x < 0.72f && lastPoint.x > 0.65f && lastPoint.y < 0.71f && lastPoint.y > 0.63f)
                                {
                                    boolLastPointAnatomy = true;
                                }
                                break;
                            case "god creates eve":
                                if (lastPoint.x < 0.72f && lastPoint.x > 0.65f && lastPoint.y < 0.72f && lastPoint.y > 0.65f)
                                {
                                    boolLastPointAnatomy = true;
                                }
                                break;
                            case "family4":
                                if (lastPoint.x < 0.85f && lastPoint.x > 0.61f && lastPoint.y < 0.41f && lastPoint.y > 0.27f)
                                {
                                    boolLastPointAnatomy = true;
                                }
                                break;
                        }
                    }
                }

                selectSquareImage.gameObject.SetActive(false);

               
                RectTransform rt = (RectTransform)selectSquareImage.transform;
                Vector3 camera = player.transform.position;
                Vector3 cameraDirection = player.transform.forward;
                Quaternion cameraRotation = player.transform.rotation;
                Vector3 spawnPos = camera + cameraDirection * 1.5f;// * distance;

                if (anatomy && boolFirstPointAnatomy && boolLastPointAnatomy)
                {
                    Debug.Log("anatomy a display");

                    // spawnPos += Vector3(5.0f, 0f, 0f);
                    GameObject gOAnatomy;
                    gOAnatomy = Instantiate(toDuplicate, spawnPos, cameraRotation);
                    gOAnatomy.name = " Selected ";
                     gOAnatomy.GetComponent<Renderer>().material.mainTexture = Resources.Load("Ceiling/"+map[variables.texture.name]) as Texture;
                    
                    if (variables.texture.name == "lybianSybil" || variables.texture.name == "family4" )
                    {
                        gOAnatomy.transform.localScale -= new Vector3(0.5f, 0f, 0f);
                    }
                    else if (variables.texture.name == "god creates eve" || variables.texture.name == "god divides light from darkness"  )
                    {
                        gOAnatomy.transform.localScale -= new Vector3(0f, 0.5f, 0f);
                    }

                    gOAnatomy.transform.eulerAngles = variables.focusedGameObject.transform.eulerAngles;    
                    duplicatedGameObjects.Add(gOAnatomy);

                    
                }
                else
                {
                    
                    GameObject tmp;
                    tmp = Instantiate(toDuplicate, spawnPos, cameraRotation);
                    tmp.name = " Selected ";
                    tmp.transform.localScale = new Vector3(rt.rect.width / 100, rt.rect.height / 100, tmp.transform.localScale.z);
                    tmp.transform.eulerAngles = variables.focusedGameObject.transform.eulerAngles;
    
                    int width = Mathf.FloorToInt(rt.rect.width);
                    int height = Mathf.FloorToInt(rt.rect.height);

                    Texture texture = variables.texture;

                    firstPoint.x *= texture.width;
                    firstPoint.y *= texture.height;

                    Texture2D texture2d = (Texture2D)texture;

                    int x = Mathf.FloorToInt(firstPoint.x);
                    int y = Mathf.FloorToInt(firstPoint.y);

                    Debug.Log("width : " + width + " height : " + height);
                    Color[] pix = texture2d.GetPixels(x, y, width, height);
                    Texture2D destTex = new Texture2D(width, height);
                    destTex.SetPixels(pix);
                    destTex.Apply();

                    tmp.GetComponent<Renderer>().material.mainTexture = destTex;

                    duplicatedGameObjects.Add(tmp);

                }

                boolFirstPointAnatomy = false;
                boolLastPointAnatomy = false;

                selectSquareImage.gameObject.SetActive(false);

                variables.select = false;
                variables.ok = false;
                boolFirstPoint = false;

               
            }
        }
        else
        {
	    variables.select = false;
                variables.ok = false;
                boolFirstPoint = false;
	
            if (duplicatedGameObjects.Count() != 0)
            {
                foreach (GameObject d in duplicatedGameObjects)
                {
                      Destroy(d);
                }
                 duplicatedGameObjects.Clear();
                 Debug.Log("All duplicated paintings are deleted");
            }
            selectSquareImage.gameObject.SetActive(false);
        }
    }


}
