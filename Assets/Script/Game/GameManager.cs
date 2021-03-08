using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")] public BoardBehaviour boardBehaviour;

    public void StartNewGame()
    {
        boardBehaviour.Init(ChangeTurn);
    }

    public void ResetGame()
    {
        StartNewGame();
    }

    public void ChangeTurn(PlayerPeace player)
    {
        if (player.stepsCount > 0)
        {
            boardBehaviour.SetCurrentPlayer(player.GetPlayerType());
            return;
        }

        player.EndPlayerTurn();
        PlayerType nextPlayer = player.GetPlayerType() == PlayerType.PlayerRed ? PlayerType.PlayerBlue : PlayerType.PlayerRed;
        boardBehaviour.SetCurrentPlayer(nextPlayer);
    }
}
