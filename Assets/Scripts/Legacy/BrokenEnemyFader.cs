using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenEnemyFader : MonoBehaviour {

    Color _color;

   

    void FadeOut()
    {
        _color = gameObject.GetComponent<Renderer>().material.color;
        _color.a -= 0.01f;
        if (_color.a == 0) _color.a = 0;
        gameObject.GetComponent<Renderer>().material.color = _color;
    }

    void Update ()
    {
        FadeOut();
    }
}

