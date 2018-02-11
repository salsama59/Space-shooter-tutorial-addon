using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PowerUpStats {

    [SerializeField]
    private String weaponModifier;
    [SerializeField]
    private String statName;
    [SerializeField]
    private int statEnhanceValue;
    [SerializeField]
    private bool statEnhancePossibility;

    public string WeaponModifier
    {
        get
        {
            return weaponModifier;
        }

        set
        {
            weaponModifier = value;
        }
    }

    public string StatName
    {
        get
        {
            return statName;
        }

        set
        {
            statName = value;
        }
    }

    public int StatEnhanceValue
    {
        get
        {
            return statEnhanceValue;
        }

        set
        {
            statEnhanceValue = value;
        }
    }

    public bool StatEnhancePossibility
    {
        get
        {
            return statEnhancePossibility;
        }

        set
        {
            statEnhancePossibility = value;
        }
    }
}
