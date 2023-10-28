using System.Collections;
using Services.Factory;
using Infrastructure;
using UnityEngine;
using Signals;
using Zenject;
using Player;

public class TerminalController : MonoBehaviour, IFactorizable
{
    [Inject] private IGameObjectsFactory _gameObjectsFactory;
    [Inject] private IStorageService _storageService;
    [Inject] private SignalBus _signalBus;
    
    public GameObject energyRequire;
    public GameObject buildCanvas;
    public Color hoverColorWhite;
    public Color hoverColorDark;

    private GameObject _energyRequireBool;
    private int _priseToByUltGun = 1200; 
    private Color _startColorWhite;
    private Color _startColorDark;
    private Renderer[] _children;
    private Animator _animation;
    private GameObject _player;
    private bool _sheepClose;
    private Material[] _rend;


    public void Start()
    {
        _signalBus.Subscribe<GameLateRestartSignal>(GameRestart);
        
        _children = GetComponentsInChildren<Renderer>();
        _animation = GetComponent<Animator>();
        _player = _gameObjectsFactory.GetSingleGameObject<PlayerController>();
        _sheepClose = false;
        buildCanvas.SetActive(false);

        _rend = GetComponentInChildren<Renderer>().materials;
        _startColorDark = _rend[1].color;
        _startColorWhite = _rend[0].color;
    }

    private async void GameRestart()
    {
        _player = _gameObjectsFactory.GetSingleGameObject<PlayerController>();
    }

    private void Update()
    {
        _energyRequireBool = GameObject.FindGameObjectWithTag("CanvasRequire");

        if (_player != null && buildCanvas != null)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) <= 2)
            {
                _animation.SetBool("sheepClose", true);
                _sheepClose = true;
            }
            else
            {
                _animation.SetBool("sheepClose", false);
                _sheepClose = false;
            }
        }
        else _animation.SetBool("sheepClose", false);
    }

    private void OnMouseOver()
    {
        if (_sheepClose && buildCanvas != null)
        {
            buildCanvas.SetActive(true);
            foreach (Renderer rnd in _children)
            {
                _rend = rnd.GetComponent<Renderer>().materials;

                _rend[0].color = hoverColorWhite;
                _rend[1].color = hoverColorDark;
            }
        }
    }

    private void OnMouseExit()
    {
        if (buildCanvas != null) buildCanvas.SetActive(false);
        foreach (Renderer rnd in _children)
        {
            _rend = rnd.GetComponent<Renderer>().materials;

            _rend[0].color = _startColorWhite;
            _rend[1].color = _startColorDark;
        }
    }

    private void OnMouseDown()
    {
        if (_sheepClose && buildCanvas != null)
        {
            if (_storageService.Energy >= _priseToByUltGun)
            {
                _storageService.SubtractEnergy(_priseToByUltGun);
                _rend[0].color = _startColorWhite;
                _rend[1].color = _startColorDark;
                Destroy(buildCanvas);
                _animation.SetBool("sheepClose", false);
                StartCoroutine(TurnOnUltimateGun());
            }
            else
            {
                if (_energyRequireBool == null) Instantiate(energyRequire, transform.position, transform.rotation);
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
