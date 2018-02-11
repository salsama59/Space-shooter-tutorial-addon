using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats {

    private String currentWeapon;
    private float speed;
    private bool shield;
    private int shotPower;

    public PlayerStats(String currentWeapon, float speed, bool shield, int shotPower)
    {
        this.CurrentWeapon = currentWeapon;
        this.Speed = speed;
        this.Shield = shield;
        this.ShotPower = shotPower;
    }

    public class StatNameConstants
    {
        public const string statSpeed = "speed";
        public const string statShield = "shield";
        public const string statShotPower = "shotPower";
    }

    public string CurrentWeapon
    {
        get
        {
            return currentWeapon;
        }

        set
        {
            currentWeapon = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public bool Shield
    {
        get
        {
            return shield;
        }

        set
        {
            shield = value;
        }
    }

    public int ShotPower
    {
        get
        {
            return shotPower;
        }

        set
        {
            shotPower = value;
        }
    }
}
