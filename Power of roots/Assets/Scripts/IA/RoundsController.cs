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
    private int roundCounter = 1;
    private SpawnEnemies spawnController;
    [SerializeField] private GameObject persianas;
    [SerializeField] private EventReference eventPersiana;

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
        er.changeMusic(plc.life, true);
        persianas.SetActive(false);
        lastEnemiesCount = Mathf.Floor(lastEnemiesCount * roundMultiplier);
        roundCounter++;
    }

    // Start is called before the first frame update
    void Start()
    {
        plc = FindAnyObjectByType<PlayerLiveController>();
        er = FindAnyObjectByType<EasyRhythmAudioManagerCustom>();
        lastEnemiesCount = startEnemiesCount;
        lastEnemiesCount = startEnemiesCount;
        spawnController = GetComponent<SpawnEnemies>();

    }

    IEnumerator StartRoundCorrutine() {
        yield return new WaitForSeconds(1.0f);
        er.changeMusic(plc.life, false);
        persianas.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot(eventPersiana);
        actualEnemiesCount = lastEnemiesCount;
        spawnController.Spawn(lastEnemiesCount);
        yield return new WaitForEndOfFrame();
    }
}
