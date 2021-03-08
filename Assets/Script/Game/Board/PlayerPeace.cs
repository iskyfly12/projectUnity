using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.VFX;

public enum PlayerType { None = -1, PlayerRed = 0, PlayerBlue = 1 }

public class PlayerPeace : MonoBehaviour, ICollect
{
    [Header("Settins")]
    [SerializeField] private PlayerSettings playerData;
    [SerializeField] private PlayerType playerType;

    [Header("UI")]
    [SerializeField] private Transform playerPanel;
    [SerializeField] private TextMeshProUGUI textHealth;
    [SerializeField] private TextMeshProUGUI textAttack;
    [SerializeField] private TextMeshProUGUI textSteps;

    [Header("VFX")]
    [SerializeField] private VisualEffect getItemVFX;

    public int health { get; private set; }
    public int attackPower { get; private set; }
    public int stepsCount { get; private set; }

    private int maxSteps;
    private Grid currentPlayerGrid;

    public void Init()
    {
        health = playerData.Health();
        attackPower = playerData.AttackPower();
        stepsCount = playerData.MaxSteps();
        maxSteps = playerData.MaxSteps();

        textHealth.text = playerData.Health().ToString();
        textAttack.text = playerData.AttackPower().ToString();
        textSteps.text = playerData.MaxSteps().ToString();
    }

    public void MovePeace(Grid grid)
    {
        currentPlayerGrid.SetGrid(GridType.Empty);

        grid.SetGrid(GridType.HasPlayer);
        SetPlayerGrid(grid);

        transform.DOComplete(this);
        transform.DOJump(grid.transform.position, .75f, 1, .5f);

        stepsCount--;

        textSteps.text = stepsCount.ToString();
    }

    public PlayerType GetPlayerType()
    {
        return playerType;
    }

    public void SetPlayerGrid(Grid grid)
    {
        currentPlayerGrid = grid;
    }

    public Grid GetPlayerGrid()
    {
        return currentPlayerGrid;
    }

    public void PlayEffect()
    {
        getItemVFX.Play();
    }

    public void StartTurn()
    {
        playerPanel.DOScale(new Vector3(1.3f, 1.3f, 1.3f), .25f);
    }

    public void EndPlayerTurn()
    {
        stepsCount = maxSteps;
        textSteps.text = stepsCount.ToString();
        playerPanel.DOScale(Vector3.one, .25f);
    }

    #region Bonus
    public void GetExtraAttack(int amount)
    {
        attackPower += amount;
        textAttack.text = attackPower.ToString();
        PlayEffect();
    }

    public void GetExtraMove(int amount)
    {
        maxSteps += amount;
        stepsCount += amount;
        textSteps.text = stepsCount.ToString();
        PlayEffect();
    }

    public void GetExtraLife(int amount)
    {
        health += amount;
        textHealth.text = health.ToString();
        PlayEffect();
    }
    #endregion
}
