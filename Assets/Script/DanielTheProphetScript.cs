using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class DanielTheProphetScript : MonoBehaviour, IFocusable {

    private Material[] defaultMaterials;

    private void Start()
    {
        defaultMaterials = GetComponent<Renderer>().materials;
    }

    public void OnFocusEnter()
    {
        for (int i=0; i<defaultMaterials.Length; i++)
        {
            // Highlight the material when gaze enters using the shader property.
            defaultMaterials[i].SetFloat("_Gloss", 10.0f);
        }
    }

    public void OnFocusExit()
    {
        for (int i = 0; i < defaultMaterials.Length; i++)
        {
            // Remove highlight on material when gaze exits.
            defaultMaterials[i].SetFloat("_Gloss", 1.0f);
        }
    }

    private void OnDestroy()
    {
        foreach (var material in defaultMaterials)
        {
            Destroy(material);
        }
    }
}


