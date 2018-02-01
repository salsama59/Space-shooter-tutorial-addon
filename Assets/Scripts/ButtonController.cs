using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    public GameObject[] players;
    private GameObject mainMenu;
    public GameObject gameController;

    public GameObject MainMenu
    {
        get
        {
            return mainMenu;
        }

        set
        {
            mainMenu = value;
        }
    }

    public void FindMainMenu () {

        MainMenu = GameObject.FindWithTag("MainMenu");



        if(MainMenu == null)
        {
            Debug.Log("Uneable to find the canvas in the scene");
        }

    }

    public void LaunchOnePlayerMode()
    {
        this.FindMainMenu();
        GameObject player = players[1];
        Destroy(player);
        this.LaunchGame(1);
    }

    public void LaunchTwoPlayerMode()
    {
        this.FindMainMenu();
        this.LaunchGame(2);
    }

    private void LaunchGame(int playerNumber)
    {
        GameController gameControllerScript = gameController.GetComponent<GameController>();
        gameControllerScript.PlayerNumber = playerNumber;
        Destroy(MainMenu);
        gameController.SetActive(true);
        this.ActivateShips();
    }

    private void ActivateShips()
    {
        for(int i = 0; i < players.Length; i++)
        {
            GameObject player = players[i];
            player.SetActive(true);
        }
    }

}
