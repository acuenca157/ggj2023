using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class RoundsController : MonoBehaviour
{
    private EasyRhythmAudioManagerCustom er;
    private PlayerLiveController plc;
    [SerializeField] private int startEnemiesCount = 5;
    private float lastEnemiesCount, actualEnemiesCount;
    [SerializeField] private float roundMultiplier = 1.25f;
    [HideInInspector] public int roundCounter = 1;
    private SpawnEnemies spawnController;
    [SerializeField] private GameObject persianas;
    [SerializeField] private EventReference eventPersiana;
    private UpgradeSystem us;

    public void StartRound() {
        print("Inicio ronda " + roundCounter);
        StartCoroutine(StartRoundCorrutine());
    }

    public void CheckForEndRound() {
        actualEnemiesCount--;
        if (actualEnemiesCount <= 0)
        {
            print(" Fin de la Ronda ");
            endRound();
        }
    }

    void endRound() {
        FMODUnity.RuntimeManager.PlayOneShot(eventPersiana);
        us.usedInRound = false;
        er.changeMusic(plc.Life, true);
        persianas.SetActive(false);
        lastEnemiesCount = Mathf.Floor(lastEnemiesCount * roundMultiplier);
        roundCounter++;
    }

    // Start is called before the first frame update
    void Start()
    {
        us = FindObjectOfType<UpgradeSystem>();
        plc = FindObjectOfType<PlayerLiveController>();
        er = FindObjectOfType<EasyRhythmAudioManagerCustom>();
        lastEnemiesCount = startEnemiesCount;
        lastEnemiesCount = startEnemiesCount;
        spawnController = GetComponent<SpawnEnemies>();

    }

    IEnumerator StartRoundCorrutine() {
        yield return new WaitForSeconds(1.0f);
        er.changeMusic(plc.Life, false);
        persianas.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot(eventPersiana);
        actualEnemiesCount = lastEnemiesCount;
        spawnController.Spawn(lastEnemiesCount);
        yield return new WaitForEndOfFrame();
    }
}
