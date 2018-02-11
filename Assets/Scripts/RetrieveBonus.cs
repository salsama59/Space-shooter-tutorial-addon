using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveBonus : MonoBehaviour {

    public PowerUpStats power;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject bonusManagerGameObject = GameObject.FindGameObjectWithTag("BonusManager");
            BonusManager bonusManager = bonusManagerGameObject.GetComponent<BonusManager>();
            bonusManager.AddBonusEffect(power, other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
