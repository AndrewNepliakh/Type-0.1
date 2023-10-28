﻿using System.Collections;
using System.Threading.Tasks;
using Infrastructure;
using Player;
using Services.Factory;
using Signals;
using UnityEngine;
using Zenject;

public class TerminalController : MonoBehaviour, IFactorizable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private IStorageService _storageService;
    [Inject] private IGameObjectsFactory _gameObjectsFactory;

    bool sheepClose;

    GameObject player;
    Animator _animation;
    Renderer[] children;
    Material[] rend;
    Color startColorWhite;
    Color startColorDark;

    public GameObject buildCanvas;
    public GameObject energyRequire;
    GameObject energyRequireBool;
    public Color hoverColorWhite;
    public Color hoverColorDark;

    private int _priseToByUltGun = 1200; 


    public void Start()
    {
        _signalBus.Subscribe<GameLateRestartSignal>(GameRestart);
        
        children = GetComponentsInChildren<Renderer>();
        _animation = GetComponent<Animator>();
        player = _gameObjectsFactory.GetSingleGameObject<PlayerController>();
        sheepClose = false;
        buildCanvas.SetActive(false);

        rend = GetComponentInChildren<Renderer>().materials;
        startColorDark = rend[1].color;
        startColorWhite = rend[0].color;
    }

    private async void GameRestart()
    {
        player = _gameObjectsFactory.GetSingleGameObject<PlayerController>();
    }

    private void Update()
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

    private void OnMouseDown()
    {
        if (sheepClose && buildCanvas != null)
        {
            if (_storageService.Energy >= _priseToByUltGun)
            {
                _storageService.SubtractEnergy(_priseToByUltGun);
                rend[0].color = startColorWhite;
                rend[1].color = startColorDark;
                Destroy(buildCanvas);
                _animation.SetBool("sheepClose", false);
                StartCoroutine(TurnOnUltimateGun());
            }
            else
            {
                if (energyRequireBool == null) Instantiate(energyRequire, transform.position, transform.rotation);
            }
        }
    }

    private IEnumerator TurnOnUltimateGun()
    {
        GameObject ultGun = _gameObjectsFactory.GetSingleGameObject<UltimateGun>();
        ultGun.GetComponent<Animator>().SetBool("UltGunBought", true);
        yield return new WaitForSeconds(2f);
        ultGun.GetComponent<UltimateGun>().enabled = true;
    }
}
