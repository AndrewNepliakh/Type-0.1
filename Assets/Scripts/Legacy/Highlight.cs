using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour {

    Material[] rendObj;
    public Color colorToChange;
    Color originColor;

    private void Start()
    {
        rendObj = GetComponent<Renderer>().materials;
    }

    private void OnMouseEnter()
    {
        rendObj[0].color = colorToChange;
        rendObj[1].shader = Shader.Find("Self-Illumin/Diffuse");
        rendObj[1].color = Color.green;
    }

    private void OnMouseExit()
    {
        foreach (Material mat in rendObj)
        {
            rendObj[0].color = Color.black;
            rendObj[1].shader = Shader.Find("Diffuse");
            rendObj[1].color = Color.white;
        }
    }
}
