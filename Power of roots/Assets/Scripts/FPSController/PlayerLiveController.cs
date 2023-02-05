using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Animations;

public class PlayerLiveController : MonoBehaviour
{
    private EasyRhythmAudioManagerCustom er;
    [SerializeField] private EventReference damageEvent;
    [SerializeField] public float life = 100f;
    [SerializeField] private Animator damagePanelAnimator;

    private void Start()
    {
        er = FindAnyObjectByType<EasyRhythmAudioManagerCustom>();
    }
    public void damagePlayer( float amount ) {
        if (life > 0) {
            er.changeMusic(life, false);
            Camera.main.fieldOfView = 60f;
            damagePanelAnimator.SetTrigger("damage");
            life -= amount;
            FMODUnity.RuntimeManager.PlayOneShot(damageEvent);
            print(" Me queda " + life);
            if (life <= 0) {
                print("AAAA ME MUERO F");
            }
        }
        
    }

}
