using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveBonus : MonoBehaviour {

    public Object power;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            BonusManager bonusManager = other.GetComponent<BonusManager>();
            Destroy(this.gameObject);
        }
    }
}
