using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    /*public Color hoverColorWhite;
    public Color hoverColorDark;
    private Color startColorWhite;
    private Color startColorDark;*/

    /*private Material[] matsLafet;
    private Material[] matsAmmo;
    private Material[] matsGun;*/

    public GameObject lafet;
    public GameObject ammo;
    public GameObject gun;

    public GameObject upgradeCanvas;
    public GameObject objectToUpgrade;
    public GameObject powerIsRequiredUi;
    GameObject energyRequireBool;
    Earning energyValue;

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        energyValue = player.GetComponent<Earning>();

        upgradeCanvas.SetActive(false);

        /*matsLafet = lafet.GetComponent<Renderer>().materials;
        matsAmmo = ammo.GetComponent<Renderer>().materials;
        matsGun = gun.GetComponent<Renderer>().materials;

        startColorDark = matsLafet[0].color;
        startColorWhite = matsLafet[1].color;*/
    }

    private void Update()
    {
        energyRequireBool = GameObject.FindGameObjectWithTag("600energyRequire");
    }

    /* private void OnMouseEnter()
     {
         upgradeCanvas.SetActive(true);
         matsLafet[0].color = hoverColorDark;
         matsAmmo[0].color = hoverColorDark;
         matsGun[0].color = hoverColorDark;

         matsLafet[1].color = hoverColorWhite;
         matsAmmo[1].color = hoverColorWhite;
         matsGun[1].color = hoverColorWhite;
     }

     private void OnMouseExit()
     {
         upgradeCanvas.SetActive(false);
         matsLafet[0].color = startColorDark;
         matsAmmo[0].color = startColorDark;
         matsGun[0].color = startColorDark;

         matsLafet[1].color = startColorWhite;
         matsAmmo[1].color = startColorWhite;
         matsGun[1].color = startColorWhite;
     }*/

    private void OnMouseDown()
    {
        if (energyValue.earning >= 300)
        {
            TurretUpgrade();
            energyValue.earning -= 300;
        }
        else if (energyValue.earning < 300 && energyRequireBool == null)
        {
            Instantiate(powerIsRequiredUi, transform.position, transform.rotation);
        }
    }

    void TurretUpgrade()
    {
        Destroy(gameObject);
        Instantiate(objectToUpgrade, transform.position, transform.rotation);
    }
}
