using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameBoardSize { Small, Normal, Large };

public class BoardBehaviour : MonoBehaviour
{
    [Header("Board Settings")]
    public PlayerManager[] players;
    public GameBoardSize boardSize;
    public PlayerType currentPlayer;

    [Header("Cells")]
    [SerializeField] private Grid[] grids;

    [Header("References")]
    [SerializeField] private GenerateGrid generateGrid;
    [SerializeField] private CheckGrid checkGrid;

    [Header("Grid Object")]
    [SerializeField] private GameObject gridPrefab;
    
    private int boardCols = 0;
    private int boardRows = 0;

    private event Action<PlayerType> onBoardChange;

    public void Init(Action<PlayerType> onBoardChangeAction)
    {
        CreateBoard(boardSize);
        SetPlayerLocation();
        onBoardChange = onBoardChangeAction;
    }

    public void CreateBoard(GameBoardSize boardSize)
    {
        this.boardSize = boardSize;

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
        generateGrid.GenerateHex(boardCols, boardRows, 1, 0, OnGridClick, OnGridSelect);
        
        //Debug.Log(String.Format("Board {0} Side & {1} Rows", boardCols, boardRows));
    }

    public void ClearBoard()
    {
        generateGrid.Clear();
    }

    public void SetCurrentPlayer(PlayerType player)
    {
        currentPlayer = player;
        checkGrid.transform.position = player == PlayerType.PlayerRed ? players[0].transform.position : players[1].transform.position;
        checkGrid.ClearNeigbors();
        checkGrid.GetNeighbors();
    }

    public void SetPlayerLocation() 
    {
        Grid grid = generateGrid.gridList[generateGrid.gridList.Count - 1];
        int maxCol = Mathf.Abs(grid.col);
        int maxRow = Mathf.Abs(grid.row);

        int rangeRowA = UnityEngine.Random.Range(maxRow * (-1), maxRow);
        grid = generateGrid.gridList.Find(x => x.col == -maxCol && x.row == rangeRowA);
        players[0].transform.position = grid.transform.position;
        grid.SetGrid(GridType.HasPlayer);
        //Debug.Log(String.Format("Player 1: [{0}][{1}]", -maxCol, rangeRowA));

        int rangeRowB = UnityEngine.Random.Range(maxRow * (-1), maxRow); 
        grid = generateGrid.gridList.Find(x => x.col == maxCol && x.row == rangeRowB);
        players[1].transform.position = grid.transform.position;
        grid.SetGrid(GridType.HasPlayer);
        //Debug.Log(String.Format("Player 1: [{0}][{1}]", -maxCol, rangeRowB));

    }

    private void OnGridSelect(Grid grid)
    {
        //Change position
    }

    private void OnGridClick(Grid grid)
    {
        if (currentPlayer != PlayerType.None && grid.isEmpty)
        {
            grid.SetGrid(GridType.HasPlayer);

            if (onBoardChange != null)
                onBoardChange(currentPlayer);

        }
    }


}
