using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWinner : MonoBehaviour
{
    public Text GameOverText;
    private int _winningPlayer;
    
    void Start()
    {
        _winningPlayer=TurnController.WinningPlayer;
        if(_winningPlayer==1)
        {
            GameOverText.text="Game Over. Player 1 Wins";
        }
        else if(_winningPlayer==2)
        {
            GameOverText.text="Game Over. Player 2 Wins";
        }
    }

    void Update()
    {
        
    }
}
