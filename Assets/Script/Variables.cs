using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour {

    public static bool spawn = false;
    public static List<GameObject> cloneList = new List<GameObject>();

    public static int cptPoints = 0;
    public static List<Vector3> pointList = new List<Vector3>();
    public GameObject focusedGameObject = null;
    public Texture texture;
    public bool bigger = false;
    public bool smaller = false;
    public bool closer = false;
    public bool select = false;
    public bool ok = false;
    public bool delete = false;
    public bool faceMe = false;
    public bool search = false;
    public bool go = false;
    public Vector3 vect = new Vector3(1.05f,-0.45f,-13.75f);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
