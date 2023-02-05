using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerLiveController : MonoBehaviour
{
    private EasyRhythmAudioManagerCustom er;
    [SerializeField] private EventReference damageEvent, dieEvent;
    [SerializeField] private float life;
    [SerializeField] private Animator damagePanelAnimator;
    [HideInInspector] public float exp = 0;
    [HideInInspector] public float bulletLife = 1f;
    [SerializeField] private Image lifeImage;
    [SerializeField] private Sprite[] lifeSprites;
    [SerializeField] private GameObject panelMuerte;
    private Movement pm;

    public float Life
    {
        get => life;
        set
        {
            life = value;
            lifeImage.sprite = lifeSprites[(int)life / 10];
        }
    }

    private void Start()
    {
        er = FindAnyObjectByType<EasyRhythmAudioManagerCustom>();
        pm = GetComponent<Movement>();
        panelMuerte.SetActive(false);
    }
    public void damagePlayer( float amount ) {
        if (Life > 0) {
            er.changeMusic(life, false);
            Camera.main.fieldOfView = 60f;
            damagePanelAnimator.SetTrigger("damage");
            Life -= amount;
            FMODUnity.RuntimeManager.PlayOneShot(damageEvent);
            print(" Me queda " + life);
            if (life <= 0) {
                pm.canMove = false;
                GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                FMODUnity.RuntimeManager.PlayOneShot(dieEvent);
                foreach (GameObject go in allEnemies) {
                    Destroy(go);
                }
                er.changeMusic(life, false, true);
                panelMuerte.SetActive(true);
            }
        }
        
    }

    public void healthAll() {
        Life = 80f;
    }

}
