using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class GunController : MonoBehaviour, IEasyListener
{
    private Camera camera;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    [SerializeField] private EventReference shotEvent;

    private void Start()
    {
        camera = Camera.main;
    }
    public void Shoot() {

        FMODUnity.RuntimeManager.PlayOneShot(shotEvent);
        camera.fieldOfView = 68f;
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

    public void OnBeat(EasyEvent audioEvent) 
	{ 
        Shoot();
	}
    private void Update()
    {
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 70f, 0.5f);
    }
}
