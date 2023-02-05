using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private EventReference eventBuy;
    [SerializeField] private float  startLifeCost, startWeaponCost;
    private float  actualLifeCost, actualWeaponCost;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button  lifeButton, weaponButton;
    [SerializeField] private TextMeshProUGUI textWeaponExp, textLifeExp, textActualXP;
    [SerializeField] private GameObject popUp;

    private PlayerLiveController PLC;
    private Movement PM;
    private bool inMenu = false;

    [HideInInspector] public bool usedInRound = true;


    [Range(0f,5f)] [SerializeField] private float actionRange;

    // Start is called before the first frame update
    void Start()
    {
        popUp.SetActive(false);
        panel.SetActive(false);
        PLC = FindObjectOfType<PlayerLiveController>();
        PM = FindObjectOfType<Movement>();

        actualLifeCost = startLifeCost;
        actualWeaponCost = startWeaponCost;
    }

    private void Update()
    {
        popUp.SetActive(false);
        if (Vector3.Distance(this.transform.position, PLC.transform.position) < actionRange && !inMenu && !usedInRound) {
            popUp.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                openMenu();
            }
        }
        else if (inMenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                closeMenu();
            }
        }
    } 
    public void openMenu() {
        textActualXP.text = "YOU HAVE " + PLC.exp + " XP POINTS";
        inMenu = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PM.canMove = false;
        panel.SetActive(true);
        lifeButton.interactable = true;
        weaponButton.interactable = true;

        textLifeExp.text = actualLifeCost + "";
        textWeaponExp.text = actualWeaponCost + "";

        if (actualLifeCost > PLC.exp)
            lifeButton.interactable = false;
        if (actualWeaponCost > PLC.exp)
            weaponButton.interactable = false;

    }

    public void closeMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        panel.SetActive(false);
        PM.canMove = true;
        inMenu = false;
    }

    public void buyLife()
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventBuy);
        usedInRound = true;
        PLC.healthAll();
        PLC.exp -= actualLifeCost;
        actualLifeCost = actualLifeCost * 1.25f;
        closeMenu();
    }

    public void buyWeapon()
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventBuy);
        usedInRound = true;
        PLC.bulletLife = PLC.bulletLife * 1.2f;
        PLC.exp -= actualWeaponCost;
        actualWeaponCost = actualWeaponCost * 1.5f;
        closeMenu();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, actionRange);
    }
}
