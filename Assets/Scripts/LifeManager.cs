using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {

    public int hp;
    private bool isDestroyed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DecreaseHp(int damage)
    {
        this.Hp -= damage;
    }


    public int Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }

    public bool IsDestroyed
    {
        get
        {
            return isDestroyed;
        }

        set
        {
            isDestroyed = value;
        }
    }
}
