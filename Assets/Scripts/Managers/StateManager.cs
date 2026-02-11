using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
This singleton holds information about all the important states of the game, namely:
Sanity - stores the player's current sanity throughout the game
Win/Lose - an interruption for winning or losing
*/
public enum SanityState
{
    Low,
    Medium,
    High
};

public class StateManager : MonoBehaviour
{
    private StateManager _instance;
    public StateManager Instance { get { return _instance; } }
    [SerializeField] private int maxSanity;
    private int Sanity;
    private bool playing;
    private bool win;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        Sanity = maxSanity;
        playing = false;
        win = false;
    }
    void Update()
    {
        if (!playing)
        {
            Application.Quit();
        }
    }

    void GameEnded()
    {
        playing = false;
    }

    void WinOrLose(bool state)
    {
        win = state;
    }

    bool HasWinState()
    {
        return win;
    }

    public int CurrentSanity()
    {
        return Sanity;
    }

    public bool SpendSanity(int amount)
    {
        if (amount >= Sanity) return false;
        Sanity -= amount;
        return true;
    }

    public bool IsAliveAfterDamage(int damage)
    {
        Sanity -= damage;
        return Sanity > 0;
    }

    public void HealSanity(int amount)
    {
        Sanity = Mathf.Min(Sanity + amount, maxSanity);
    }
}
