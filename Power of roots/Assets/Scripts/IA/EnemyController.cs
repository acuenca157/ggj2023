using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using FMODUnity;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float vidaBase = 100f;
    private PlayerLiveController player;
    private float vidaActual;
    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;
    private SpriteRenderer sprite;
    private RoundsController roundController;
    [SerializeField] private float damageAmt = 5f, damageWaitTime = 0.5f;
    [Range(0f, 5f)] [SerializeField] private float attackRadio = 2f;
    [SerializeField] private EventReference eventMuere;

    private bool muerto = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerLiveController>();
        roundController = FindAnyObjectByType<RoundsController>();
        vidaActual = vidaBase;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = FindObjectOfType<Movement>().transform;
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(damagePlayer());
    }

    private void Update()
    {
        if (!muerto)
            navMeshAgent.destination = playerTransform.position;
    }

    public void recibirDamage( float damage ) {
        if (!muerto)
        {
            StartCoroutine(changeColor());
            vidaActual -= damage;
            if (vidaActual <= 0)
            {
                muerto = true;
                roundController.CheckForEndRound();
                FMODUnity.RuntimeManager.PlayOneShot(eventMuere);
                animator.SetTrigger("muere");
            }
        }
    }

    public void hulkAplasta() {
        Destroy(this.gameObject);
    }

    IEnumerator changeColor() {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        sprite.color = Color.white;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator damagePlayer() {
        while (!muerto) {
            float distance = Vector3.Distance(this.transform.position, player.transform.position);
            if (distance <= attackRadio) {
                player.damagePlayer(damageAmt);
            }
            yield return new WaitForSeconds(damageWaitTime);
        }
        yield return new WaitForEndOfFrame();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadio);
    }
}
