// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using HoloToolkit.Unity.InputModule;
using UnityEngine;

/// <summary>
/// This class shows how to handle focus events and speech input events.
/// </summary>
[RequireComponent(typeof(Renderer))]
public class FocusedObjectTest : MonoBehaviour, IInputHandler
{
    public GameObject clone = null;
    public float SizeFactor = 100.0f;

    public string s;
    private float zoomHeight;
    private float zoomWidth;

    public void OnInputDown(InputEventData eventData)
    {
        //cloneList.Clear();
    }

    public void OnInputUp(InputEventData eventData)
    {
        if (eventData.PressType == InteractionSourcePressInfo.Select)
        {
            //Debug.Log(Variables.spawn);
            RaycastHit hitInfo;
            if (Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hitInfo,
                20.0f,
                Physics.DefaultRaycastLayers))
            {
                /*if (Variables.spawn == true)
                {
                    Debug.Log("XXX");
                    Variables.cptSelect = 0;
                    foreach (GameObject obj in Variables.cloneList)
                    {
                        Destroy(obj);
                    }
                    Variables.cloneList.Clear();
                }
                if (Variables.spawn == false)
                            {
                                Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
                                clone = Instantiate(this.gameObject, spawnPosition, Camera.main.transform.rotation);
                                clone.transform.LookAt(Camera.main.transform);
                                makeSmaller(clone);
                                Variables.cloneList.Add(clone);
                            }
                Variables.spawn = !Variables.spawn;*/

                if (Variables.pointList.Count < 4)
                {
                    Variables.pointList.Add(hitInfo.point);
                }
                if (Variables.pointList.Count == 4)
                {
                    if (Vector3.Distance(Variables.pointList[0], Variables.pointList[1]) > Vector3.Distance(Variables.pointList[2], Variables.pointList[3]))
                    {
                        zoomWidth = Vector3.Distance(Variables.pointList[0], Variables.pointList[1]);
                    }
                    else
                    {
                        zoomWidth = Vector3.Distance(Variables.pointList[2], Variables.pointList[3]);
                    }

                    if (Vector3.Distance(Variables.pointList[0], Variables.pointList[3]) > Vector3.Distance(Variables.pointList[1], Variables.pointList[2]))
                    {
                        zoomHeight = Vector3.Distance(Variables.pointList[0], Variables.pointList[3]);
                    }
                    else
                    {
                        zoomHeight = Vector3.Distance(Variables.pointList[1], Variables.pointList[2]);
                    }

                    /*Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
                    clone = Instantiate(this.gameObject, spawnPosition, Camera.main.transform.rotation);

                    Texture2D texture2 = clone.GetComponent<Renderer>().material.mainTexture as Texture2D;
                    texture2.SetPixels(colors);
                    clone.GetComponent<Renderer>().material.mainTexture = texture2;

                    clone.transform.LookAt(Camera.main.transform);
                    makeSmaller(clone);
                    Variables.cloneList.Add(clone);*/

                    //Texture2D texture = this.GetComponent<Renderer>().material.mainTexture as Texture2D;
                    //Color[] colors = texture.GetPixels((int)Variables.pointList[0].x, (int)Variables.pointList[0].y, (int)zoomWidth, (int)zoomHeight);

                    Color[] pix = (this.GetComponent<Renderer>().material.mainTexture as Texture2D).GetPixels(0, 0, (int) zoomWidth, (int) zoomHeight);
                    Texture2D destText = new Texture2D((int) zoomWidth, (int) zoomHeight);
                    destText.SetPixels(pix);
                    destText.Apply();

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
                        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
                        clone = Instantiate(this.gameObject, spawnPosition, Camera.main.transform.rotation);
                        //clone = Instantiate(GameObject.Find("empty"), spawnPosition, Camera.main.transform.rotation);

                        clone.GetComponent<Renderer>().material.mainTexture = destText;
                        clone.transform.localScale = new Vector3((int) zoomWidth/100, (int) zoomHeight/100, 0);

                        clone.transform.LookAt(Camera.main.transform);
                        //makeSmaller(clone);
                        Variables.cloneList.Add(clone);
                    }
                    Variables.spawn = !Variables.spawn;
                }
            }
        }
    }

    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(
                Camera.main.transform.position,
                Camera.main.transform.forward,
                out hitInfo,
                20.0f,
                Physics.DefaultRaycastLayers))
        {
            if (clone != null)
            {
                Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
                clone.transform.position = spawnPosition;
                clone.transform.LookAt(Camera.main.transform);
            }
        }
    }

    public void makeSmaller(GameObject gameObject)
    {
        Vector3 scale = gameObject.transform.localScale;
        scale /= SizeFactor;
        gameObject.transform.localScale = scale;
    }
}
