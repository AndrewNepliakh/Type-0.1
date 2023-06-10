using UnityEngine;

public class BaseTurret : MonoBehaviour
{

    public  Color hoverColorWhite;
    public Color hoverColorDark;
    private Color startColorWhite;
    private Color startColorDark;
    //private Material[] rend;
    public GameObject powerIsRequiredUi;
    public GameObject buildCanvas;
    public GameObject aura;
    GameObject energyRequireBool;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");

        //rend = GetComponent<Renderer>().materials;

        buildCanvas.SetActive(false);

        //startColorWhite = rend[0].color;
        //startColorDark = rend[1].color;
    }

    void Update()
    {
        energyRequireBool = GameObject.FindGameObjectWithTag("300energyRequire");
    }

   /* private void OnMouseEnter()
    {
        buildCanvas.SetActive(true);

        //rend[0].color = hoverColorWhite;
        //rend[1].color = hoverColorDark;
    }

    private void OnMouseExit()
    {
        buildCanvas.SetActive(false);

        //rend[0].color = startColorWhite;
        //rend[1].color = startColorDark;
    }*/

    private void OnMouseDown()
    {

        if (player != null)
        {
            Earning plr = player.GetComponent<Earning>();
            if (plr.earning < 300 && energyRequireBool == null)
            {
                Instantiate(powerIsRequiredUi, transform.position, transform.rotation);
            }
            else if (plr.earning >= 300)
            {
                GameObject turretToBuild = BuildManager.instanse.GetTurretToBuild();
                Instantiate(turretToBuild, transform.position, transform.rotation);
                plr.earning -= 300;
                Destroy(aura);
            }
        }     
    }
}
