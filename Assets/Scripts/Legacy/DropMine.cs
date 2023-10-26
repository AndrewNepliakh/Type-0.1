using Infrastructure;
using UnityEngine;
using Zenject;

public class DropMine : MonoBehaviour
{
    [Inject] private IStorageService _storageService;
    
    [SerializeField] private GameObject _noMoreMines;
    [SerializeField] private GameObject _mine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) MineDrop();
    }

    public void MineDrop()
    {
        if (_storageService.Mines > 0)
        {
            Instantiate(_mine, transform.position, transform.rotation);
            _storageService.SubtractMines(1);
        }
        else if (_storageService.Mines <= 0)
        {
            Instantiate(_noMoreMines, transform.position, Quaternion.identity);
        }
    }
}
