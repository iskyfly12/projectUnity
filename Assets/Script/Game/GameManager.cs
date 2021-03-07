using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoardSize{ }

public class GameManager : MonoBehaviour
{
    [Header("References")] public BoardBehaviour boardBehaviour;

    void Start()
    {
        StartNewGame();
    }


    public void StartNewGame()
    {
        boardBehaviour.Init(ChangeTurn);
        boardBehaviour.SetCurrentPlayer(PlayerType.PlayerRed);
    }

    public void ResetGame()
    {

    }

    public void ChangeTurn(PlayerType player)
    {
        PlayerType nextPlayer = player == PlayerType.PlayerRed ? PlayerType.PlayerBlue : PlayerType.PlayerRed;
        boardBehaviour.SetCurrentPlayer(nextPlayer);
    }
}
