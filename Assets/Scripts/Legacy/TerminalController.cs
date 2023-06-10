﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalController : MonoBehaviour
{

    bool sheepClose;

    GameObject player;
    Animator _animation;
    Renderer[] children;
    Material[] rend;
    Color startColorWhite;
    Color startColorDark;

    public GameObject buildCanvas;
    public GameObject lamp;
    public GameObject energyRequire;
    GameObject energyRequireBool;
    public Color hoverColorWhite;
    public Color hoverColorDark;
    public CameraController cam;


    void Start()
    {
        children = GetComponentsInChildren<Renderer>();
        _animation = GetComponent<Animator>();
        player = GameObject.Find("Player");
        sheepClose = false;
        buildCanvas.SetActive(false);

        rend = GetComponentInChildren<Renderer>().materials;
        startColorDark = rend[1].color;
        startColorWhite = rend[0].color;
    }

    void Update()
    {
        energyRequireBool = GameObject.FindGameObjectWithTag("CanvasRequire");

        if (player != null && buildCanvas != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 2)
            {
                _animation.SetBool("sheepClose", true);
                sheepClose = true;
            }
            else
            {
                _animation.SetBool("sheepClose", false);
                sheepClose = false;
            }
        }
        else _animation.SetBool("sheepClose", false);

    }

    private void OnMouseOver()
    {
        if (sheepClose && buildCanvas != null)
        {
            buildCanvas.SetActive(true);
            foreach (Renderer rnd in children)
            {
                rend = rnd.GetComponent<Renderer>().materials;

                rend[0].color = hoverColorWhite;
                rend[1].color = hoverColorDark;
            }
        }
    }

    private void OnMouseExit()
    {
        if (buildCanvas != null) buildCanvas.SetActive(false);
        foreach (Renderer rnd in children)
        {
            rend = rnd.GetComponent<Renderer>().materials;

            rend[0].color = startColorWhite;
            rend[1].color = startColorDark;
        }
    }

    void OnMouseDown()
    {
        if (sheepClose && buildCanvas != null)
        {
            if (player.GetComponent<Earning>().earning >= 1200)
            {
                player.GetComponent<Earning>().earning -= 1200;
                rend[0].color = startColorWhite;
                rend[1].color = startColorDark;
                Destroy(lamp);
                Destroy(buildCanvas);
                _animation.SetBool("sheepClose", false);
                StartCoroutine("TurnOnUltGun");
                cam.GetComponent<CameraController>().currentZoom = 15f;
            }
            else
            {
                if (energyRequireBool == null) Instantiate(energyRequire, transform.position, transform.rotation);
            }
        }
    }

    IEnumerator TurnOnUltGun()
    {
        GameObject ultGun = GameObject.Find("Ultimate_Gun_FBX");
        ultGun.GetComponent<Animator>().SetBool("UltGunBought", true);
        yield return new WaitForSeconds(2f);
        ultGun.GetComponent<UltimateGun>().enabled = true;
    }
}
