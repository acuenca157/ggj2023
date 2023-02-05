using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTrigger : MonoBehaviour
{
    private RoundsController RC;

    private void Start()
    {
        RC = FindObjectOfType<RoundsController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player") {
            RC.StartRound();
        }
    }

}
