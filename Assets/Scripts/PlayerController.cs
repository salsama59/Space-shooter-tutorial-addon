﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class WeaponTypeConstants
{
    public const string SINGLE_SHOT = "SGS";
    public const string DOUBLE_SHOT = "DS";
    public const string TRIPLE_SHOT = "TS";
    public const string SCATTER_SHOT = "SS";
}

public class PlayerController : MonoBehaviour
{

    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    private float nextFire;
    private PlayerStats stats;

    private void Start()
    {
        this.Stats = new PlayerStats(WeaponTypeConstants.SINGLE_SHOT, 5f, false, 1, 0.25f);
    }

    private void Update()
    {
        switch (this.gameObject.name)
        {

            case "Player 1":
                if (Input.GetKey(KeyCode.RightControl) && Time.time > nextFire)
                {
                    this.FireShot();
                }
                break;
            case "Player 2":
                if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
                {
                    this.FireShot();
                }
                break;
        }
                

    }

    private void FixedUpdate()
    {

        float moveHorizontal = 0.0f;
        float moveVertical = 0.0f;
        Rigidbody rigidBodyComponent = null;
        Vector3 newVelocity = new Vector3();

        switch (this.gameObject.name)
        {

            case "Player 1":

                if (Input.GetKey("[4]"))
                {
                    moveHorizontal = -1;
                }

                if (Input.GetKey("[6]"))
                {
                    moveHorizontal = 1;
                }

                if (Input.GetKey("[8]"))
                {
                    moveVertical = 1;
                }

                if (Input.GetKey("[2]"))
                {
                    moveVertical = -1;
                }
                rigidBodyComponent = GetComponent<Rigidbody>();
                newVelocity = new Vector3(moveHorizontal, 0.0f, moveVertical);
                break;
            case "Player 2":

                if(Input.GetKey(KeyCode.Q))
                {
                    moveHorizontal = - 1;
                }

                if(Input.GetKey(KeyCode.D))
                {
                    moveHorizontal = 1;
                }

                if (Input.GetKey(KeyCode.Z))
                {
                    moveVertical = 1;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    moveVertical = -1;
                }
                rigidBodyComponent = GetComponent<Rigidbody>();
                newVelocity = new Vector3(moveHorizontal, 0.0f, moveVertical);
                break;

        }

       if(rigidBodyComponent != null)
        {
            rigidBodyComponent.velocity = newVelocity * this.stats.Speed;

            rigidBodyComponent.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBodyComponent.velocity.x * -tilt);

            float rigidBodyPositionX = Mathf.Clamp(rigidBodyComponent.position.x, boundary.xMin, boundary.xMax);
            float rigidBodyPositionY = 0.0f;
            float rigidBodyPositionZ = Mathf.Clamp(rigidBodyComponent.position.z, boundary.zMin, boundary.zMax);

            rigidBodyComponent.position = new Vector3(rigidBodyPositionX, rigidBodyPositionY, rigidBodyPositionZ);
        }
       
    }

    public PlayerStats Stats
    {
        get
        {
            return Stats1;
        }

        set
        {
            Stats1 = value;
        }
    }

    public PlayerStats Stats1
    {
        get
        {
            return stats;
        }

        set
        {
            stats = value;
        }
    }

    public void FireShot()
    {
        nextFire = Time.time + this.stats.FireRate;
        this.DefineShootEffect();
        AudioSource laserShotAudioSource = GetComponent<AudioSource>();
        laserShotAudioSource.Play();
    }


    public void DefineShootEffect()
    {
        switch (this.Stats1.CurrentWeapon)
        {
            case WeaponTypeConstants.DOUBLE_SHOT:
                this.DoDoubleShot();
                break;
            case WeaponTypeConstants.TRIPLE_SHOT:
                this.DoTipleShot();
                break;
            case WeaponTypeConstants.SCATTER_SHOT:
                this.DoScatterShot();
                break;
            default:
                this.DoSimpleShot();
                break;
        }
    }

    private void UpdateShotManager(GameObject newShot)
    {
        ShotManager shotManager = newShot.GetComponent<ShotManager>();
        shotManager.Damage = this.stats.ShotPower;
        shotManager.Parent = this.gameObject;
    }

    private void UpdateShotManager(List<GameObject> newShots)
    {
        foreach(GameObject shotElement in newShots)
        {
            this.UpdateShotManager(shotElement);
        }
    }

    private void DoSimpleShot()
    {
        GameObject newShot = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        this.UpdateShotManager(newShot);
    }

    private void DoDoubleShot()
    {
        float fistShotXposition = -0.5f + shotSpawn.position.x;
        float secondShotXposition = 0.5f + shotSpawn.position.x;

        float fistShotZposition = -0.75f + shotSpawn.position.z;
        float secondShotZposition = -0.75f + shotSpawn.position.z;

        Vector3 firstShotPosition = new Vector3(fistShotXposition, shotSpawn.position.y, fistShotZposition);
        Vector3 secondShotPosition = new Vector3(secondShotXposition, shotSpawn.position.y, secondShotZposition);

        GameObject firstShot = Instantiate(shot, firstShotPosition, shotSpawn.rotation);
        GameObject secondShot = Instantiate(shot, secondShotPosition, shotSpawn.rotation);

        List<GameObject> shots = new List<GameObject>
        {
            firstShot,
            secondShot
        };

        this.UpdateShotManager(shots);

    }

    private void DoTipleShot()
    {
        float fistShotXposition = -0.5f + shotSpawn.position.x;
        float secondShotXposition = 0f + shotSpawn.position.x;
        float thirdShotXposition = 0.5f + shotSpawn.position.x;

        float fistShotZposition = -0.75f + shotSpawn.position.z;
        float secondShotZposition = 0f + shotSpawn.position.z;
        float thirdShotZposition = -0.75f + shotSpawn.position.z;

        Vector3 firstShotPosition = new Vector3(fistShotXposition, shotSpawn.position.y, fistShotZposition);
        Vector3 secondShotPosition = new Vector3(secondShotXposition, shotSpawn.position.y, secondShotZposition);
        Vector3 thirdShotPosition = new Vector3(thirdShotXposition, shotSpawn.position.y, thirdShotZposition);

        GameObject firstShot = Instantiate(shot, firstShotPosition, shotSpawn.rotation);
        GameObject secondShot = Instantiate(shot, secondShotPosition, shotSpawn.rotation);
        GameObject thirdShot = Instantiate(shot, thirdShotPosition, shotSpawn.rotation);

        List<GameObject> shots = new List<GameObject>
        {
            firstShot,
            secondShot,
            thirdShot
        };

        this.UpdateShotManager(shots);

    }


    private void DoScatterShot()
    {
        float fistShotXposition = -0.5f + shotSpawn.position.x;
        float secondShotXposition = 0f + shotSpawn.position.x;
        float thirdShotXposition = 0.5f + shotSpawn.position.x;

        float fistShotZposition = -0.75f + shotSpawn.position.z;
        float secondShotZposition = 0f + shotSpawn.position.z;
        float thirdShotZposition = -0.75f + shotSpawn.position.z;


        Quaternion firstShotRotation  = Quaternion.Euler(new Vector3(shotSpawn.rotation.x, -45f + shotSpawn.rotation.y, shotSpawn.rotation.z));
        Quaternion secondShotRotation = Quaternion.Euler(new Vector3(shotSpawn.rotation.x, 0f + shotSpawn.rotation.y, shotSpawn.rotation.z));
        Quaternion thirdShotRotation = Quaternion.Euler(new Vector3(shotSpawn.rotation.x, 45f + shotSpawn.rotation.y, shotSpawn.rotation.z));


        Vector3 firstShotPosition = new Vector3(fistShotXposition, shotSpawn.position.y, fistShotZposition);
        Vector3 secondShotPosition = new Vector3(secondShotXposition, shotSpawn.position.y, secondShotZposition);
        Vector3 thirdShotPosition = new Vector3(thirdShotXposition, shotSpawn.position.y, thirdShotZposition);

        GameObject firstShot = Instantiate(shot, firstShotPosition, firstShotRotation);
        GameObject secondShot = Instantiate(shot, secondShotPosition, secondShotRotation);
        GameObject thirdShot = Instantiate(shot, thirdShotPosition, thirdShotRotation);

        List<GameObject> shots = new List<GameObject>
        {
            firstShot,
            secondShot,
            thirdShot
        };

        this.UpdateShotManager(shots);

    }
}