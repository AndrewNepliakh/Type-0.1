using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instanse;
    public GameObject turretPrefab;
    private GameObject turretToBuild;

    private void Awake()
    {
        instanse = this;
    }

    private void Start()
    {
        turretToBuild = turretPrefab;
    }

    public GameObject GetTurretToBuild ()
    {
        return turretToBuild;
    }
}
