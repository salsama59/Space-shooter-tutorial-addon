using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

    private AudioSource audioSource;

	void Start () {
        audioSource = this.GetComponent<AudioSource>();
        InvokeRepeating("Fire", fireRate, delay);
        this.Fire();

    }

    void Fire() {

        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();

    }

}
