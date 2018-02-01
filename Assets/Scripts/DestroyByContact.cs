using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;
    private LifeManager lifeManager;
    private LifeManager otherLifeManager;
    private int damage;
    private BonusManager bonusManager;

    private void Start()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find a game controller script on this scene");
        }

        lifeManager = this.GetComponent<LifeManager>();

        GameObject bonusManagerObject = GameObject.FindWithTag("BonusManager");

        if (bonusManagerObject != null)
        {
            bonusManager = bonusManagerObject.GetComponent<BonusManager>();
        }
        else
        {
            Debug.Log("Cannot find a bonus controller script on this scene");
        }
        
    }

    private void FixedUpdate()
    {
       if (!this.gameController.GameOver1 && IsPlayersAnnihilated())
        {
            this.gameController.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        otherLifeManager = other.GetComponent<LifeManager>();

        if (this.gameObject.name == "Bolt Enemy" && other.CompareTag("Bolt"))
        {
            return;
        }

        if (this.gameObject.CompareTag("Bolt") && other.name == "Bolt Enemy")
        {
            return;
        }

        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }
        else if(other.CompareTag("Player"))
        {
           Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

        }

        ShotManager shotManager = other.GetComponent<ShotManager>();

        if (other.CompareTag("Bolt"))
        {
           
            if(shotManager != null)
            {
                
                this.damage = shotManager.Damage;

            }
            else
            {

                this.damage = 0;

            }
            
        }

        if(!this.CanBeDestroyed())
        {
            this.lifeManager.DecreaseHp(damage);
        }

        if (this.CanBeDestroyed())
        {

            if (explosion != null)
            {
                Instantiate(explosion, this.transform.position, this.transform.rotation);
            }

            if(shotManager != null && shotManager.Parent != null)
            {
                int playerId = 0;

                if (shotManager.Parent.name == "Player 1")
                {
                    this.gameController.AddScore(scoreValue, GameController.playerPool.PLAYER1);
                    playerId = (int)GameController.playerPool.PLAYER1;
                }
                else if (shotManager.Parent.name == "Player 2")
                {
                    this.gameController.AddScore(scoreValue, GameController.playerPool.PLAYER2);
                    playerId = (int)GameController.playerPool.PLAYER2;
                }

                int bonusLevel = bonusManager.GetBonusLevel(this.gameController.scores[playerId]);
                bool isReached = bonusManager.IsThresHoldReached(playerId, bonusLevel, this.gameController.scores[playerId]);

                if (isReached)
                {
                    bonusManager.SpawnBonus(shotManager.Parent, playerId, bonusLevel, this.transform);
                }

            }
            

            Destroy(this.gameObject);
        }

        if (!this.OtherCanBeDestroyed())
        {
            this.otherLifeManager.DecreaseHp(damage);
        }

        if (this.OtherCanBeDestroyed())
        {
            if (explosion != null)
            {
                Instantiate(explosion, this.transform.position, this.transform.rotation);
            }

            Destroy(other.gameObject);
        }
       

    }

    bool IsPlayersAnnihilated()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        return gameObjects.Length == 0;
    }

    bool CanBeDestroyed()
    {
        if(this.lifeManager == null)
        {
            return true;
        }

        return this.lifeManager.Hp <= 0;
    }

    bool OtherCanBeDestroyed()
    {
        if (this.otherLifeManager == null)
        {
            return true;
        }

        return this.otherLifeManager.Hp <= 0;
    }
}