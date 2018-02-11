using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour {


    public GameObject[] bonuses;
    public int thresholdSlope;
    public int originPoint;
    private Dictionary<int, List<int>> bonusFlags = new Dictionary<int, List<int>>();

    private void Start()
    {
        bonusFlags.Add(0, new List<int>());
        bonusFlags.Add(1, new List<int>());
    }

    public int GetThreshold(int bonusLevel)
    {
        return thresholdSlope * bonusLevel + originPoint;
    }

    public int GetBonusLevel(int currentPoints)
    {
        return (currentPoints - originPoint) / thresholdSlope;
    }

    public bool IsThresHoldReached(int playerId, int currentBonusLevel, int currentPoints)
    {

        if(bonusFlags[playerId].Contains(currentBonusLevel))
        {
            return false;
        }

        return currentPoints >= this.GetThreshold(currentBonusLevel) ? true : false;
    }

    public void SpawnBonus(GameObject player, int playerId, int currentBonusLevel, Transform targetTransform)
    {
        int adjustedBonusLevel = Mathf.Clamp(currentBonusLevel, 0, bonuses.Length);
        GameObject bonus = bonuses[Random.Range(0, adjustedBonusLevel)];
        Vector3 spawnPosition = new Vector3(targetTransform.position.x, targetTransform.position.y, targetTransform.position.z);
        Quaternion spawnRotation = bonus.transform.rotation;

        Instantiate(bonus, spawnPosition, spawnRotation);

        bonusFlags[playerId].Add(currentBonusLevel);
    }

    public void AddBonusEffect(PowerUpStats powerUp, GameObject playerGameObject)
    {
        PlayerController playerController = playerGameObject.GetComponent<PlayerController>();
        playerController.Stats.CurrentWeapon = powerUp.WeaponModifier;
        if(powerUp.StatName != null && !powerUp.StatName.Equals(""))
        {
            switch (powerUp.StatName)
            {
                case PlayerStats.StatNameConstants.statSpeed:
                    playerController.Stats.Speed = powerUp.StatEnhanceValue;
                    break;
                case PlayerStats.StatNameConstants.statShield:
                    playerController.Stats.Shield = powerUp.StatEnhancePossibility;
                    break;
                case PlayerStats.StatNameConstants.statShotPower:
                    playerController.Stats.ShotPower = powerUp.StatEnhanceValue;
                    break;
                default:
                    break;
            }
            
        }
    }
}
