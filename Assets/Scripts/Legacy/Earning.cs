using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Earning : MonoBehaviour {

    public int earning;
    public Text earnedValueText;

    private void Update()
    {
        earnedValueText.text = earning.ToString();
    }

}
