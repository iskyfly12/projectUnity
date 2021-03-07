using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { None, PlayerRed, PlayerBlue }

public class PlayerManager : MonoBehaviour, ICollect
{
    public int health { get; private set; }
    public int attackPower { get; private set; }
    public int stepsCount { get; private set; }

    private int maxSteps;

    public void Init(int health, int attack, int steps)
    {
        this.health = health;
        this.attackPower = attack;
        this.stepsCount = steps;
        maxSteps = steps;
    }

    public void EndPlayerTurn()
    {
        stepsCount = maxSteps;
    }

    public void GetAttack()
    {
        attackPower++;
    }

    public void GetExtraMove()
    {
        maxSteps++;
    }

    public void GetLife()
    {
        health++;
    }
}
