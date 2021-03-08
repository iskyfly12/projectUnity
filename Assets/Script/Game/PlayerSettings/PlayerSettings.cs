using UnityEngine;

[CreateAssetMenu(fileName = "Status", menuName = "ScriptableObjects/PlayerStatus")]
public class PlayerSettings : ScriptableObject
{
    [Header("Player Status")]
    [SerializeField] private int health;
    [SerializeField] private int attackPower;
    [SerializeField] private int maxSteps;

    public int Health() { return health; }
    public int AttackPower() { return attackPower; }
    public int MaxSteps() { return maxSteps; }
}
