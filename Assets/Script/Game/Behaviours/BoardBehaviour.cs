using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameBoardSize { Small = 1, Normal = 2, Large = 3 };

public class BoardBehaviour : MonoBehaviour
{
    [Header("Board Settings")]
    [SerializeField] private PlayerPeace[] players;

    [Header("References")]
    [SerializeField] private GenerateGrid generateGrid;
    [SerializeField] private GenerateCollectables generateCollectables;
    [SerializeField] private CheckGrid checkGrid;
    [SerializeField] private CameraBehaviour cameraBehaviour;

    Grid[] grids;
    private GameBoardSize boardSize;
    private PlayerPeace currentPlayer;
    private int boardCols = 0;
    private int boardRows = 0;

    private event Action<PlayerPeace> onBoardChange;

    public void Init(Action<PlayerPeace> onBoardChangeAction)
    {
        players[0].Init();
        players[1].Init();

        cameraBehaviour.MoveToPosition(Vector3.zero, 1f);

        StartCoroutine(InitBoard());

        onBoardChange = onBoardChangeAction;
    }

    #region Board
    public void CreateBoard(GameBoardSize size)
    {
        boardSize = size;
        generateCollectables.Clear();

        switch (boardSize)
        {
            case GameBoardSize.Small:
                boardCols = UnityEngine.Random.Range(2, 6);
                boardRows = UnityEngine.Random.Range(4, 6);
                break;
            case GameBoardSize.Normal:
                boardCols = UnityEngine.Random.Range(3, 10);
                boardRows = UnityEngine.Random.Range(6, 10);
                break;
            case GameBoardSize.Large:
                boardCols = UnityEngine.Random.Range(5, 16);
                boardRows = UnityEngine.Random.Range(10, 16);
                break;
            default:
                break;
        }

        generateGrid.Clear();
        generateGrid.Generate(boardCols, boardRows, 1, 0, .25f / (int)size, OnGridClick);
    }

    public void CreateCollectables()
    {
        grids = generateGrid.gridList.ToArray();
        generateCollectables.Clear();
        generateCollectables.Generate(grids, grids.Length / 2);
    }

    public void SetBoardSize(int size)
    {
        boardSize = (GameBoardSize)size;
    }

    public void ClearBoard()
    {
        generateGrid.Clear();
    }

    private void GridChecker(Grid grid)
    {
        checkGrid.transform.position = grid.transform.position;
        checkGrid.ClearNeigbors();
        checkGrid.GetNeighbors();
    }

    IEnumerator InitBoard()
    {
        CreateBoard(boardSize);
        yield return new WaitUntil(() => generateGrid.finishGeneration);
        
        SetPlayerLocation();
        CreateCollectables();

        yield return new WaitUntil(() => generateCollectables.finishGeneration);

        SetCurrentPlayer((PlayerType) UnityEngine.Random.Range(0, 1));
    }
    #endregion

    #region Player
    public void SetCurrentPlayer(PlayerType player)
    {
        int playerIndex = (int)player;
        currentPlayer = players[playerIndex];

        currentPlayer.StartTurn();

        cameraBehaviour.MoveToTarget(currentPlayer.GetPlayerGrid().transform, 1f);

        GridChecker(currentPlayer.GetPlayerGrid());
    }

    public void SetPlayerLocation()
    {
        Grid grid = generateGrid.gridList[generateGrid.gridList.Count - 1];
        int maxCol = Mathf.Abs(grid.col);
        int maxRow = Mathf.Abs(grid.row);

        for (int i = 0; i < players.Length; i++)
        {
            int rangeRow = 0;
            int side = i % 2 == 0 ? -1 : 1;

            if (boardSize != 0)
            {
                while (rangeRow == 0)
                    rangeRow = UnityEngine.Random.Range(maxRow * (-1), maxRow);

                while (!grid.isEmpty)
                    grid = generateGrid.gridList.Find(x => x.col == side * maxCol && x.row == rangeRow);
            }

            players[i].transform.DOMove(grid.transform.position, .5f);
            players[i].SetPlayerGrid(grid);
            grid.SetGrid(GridType.HasPlayer);
        }
    }
    #endregion

    #region Actions
    private void OnGridClick(Grid grid)
    {
        if (currentPlayer.GetPlayerType() != PlayerType.None)
        {
            int playerIndex = (int)currentPlayer.GetPlayerType();

            if (!grid.isEmpty)
            {
                grid.GridItem.Collect(players[playerIndex]);
                generateCollectables.collectablesCount--;
            }

            float collectablesLeft = (grids.Length / 2) * 0.1f;
            if (generateCollectables.collectablesCount <= (int) collectablesLeft)
                generateCollectables.Generate(grids, (grids.Length / 2) - (int) collectablesLeft);

            players[playerIndex].MovePeace(grid);

            if (onBoardChange != null)
                onBoardChange(currentPlayer);
        }
    }
    #endregion
}
