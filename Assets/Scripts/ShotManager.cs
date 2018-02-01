﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotManager : MonoBehaviour {

    private int damage;
    private GameObject parent;

    // Use this for initialization
    void Start () {

        //this.Damage = 0;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public GameObject Parent
    {
        get
        {
            return parent;
        }

        set
        {
            parent = value;
        }
    }

    public void IncreaseDamage(int damage)
    {
        this.Damage += damage;
    }

    public void DecreaseDamage(int damage)
    {
        this.Damage -= damage;
    }

}
