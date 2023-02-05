using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Bullet : MonoBehaviour
{
    private PlayerLiveController plc;
    public float life;
    [SerializeField] private Color[] colores;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float damage = 50f;
    [SerializeField] private EventReference damageEvent;

    private SpriteRenderer sprite;
    void Awake()
    {
        plc = FindObjectOfType<PlayerLiveController>();
        life = plc.bulletLife;
        Destroy(this.gameObject, life);
    }

    private void Start()
    {
        int colorId = Random.Range(0, colores.Length);
        int spriteId = Random.Range(0, sprites.Length);
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = colores[colorId];
        sprite.sprite = sprites[spriteId];
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            FMODUnity.RuntimeManager.PlayOneShot(damageEvent);
            collision.transform.GetComponent<EnemyController>().recibirDamage(damage);
            Destroy(this.gameObject);
        }
        if (collision.transform.tag != "Player") {
            Destroy(this.gameObject);
        }

    }
}
