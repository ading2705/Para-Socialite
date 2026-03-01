using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
This singleton holds information about all the important states of the game, namely:
Sanity - stores the player's current sanity throughout the game
Win/Lose - an interruption for winning or losing
IsFastForwarding - for the tower defense section, increases the speed of all processes
*/
public enum SanityState
{
    Low,
    Medium,
    High
};

public class StateManager : MonoBehaviour
{
    [SerializeField] private float FastForwardSpeed;
    private bool isFastForwarding;
    private static StateManager _instance;
    public static StateManager Instance { get { return _instance; } }
    [SerializeField] private int maxSanity;
    private int Sanity;
    private bool playing;
    private bool win;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        Sanity = PlayerPrefs.GetInt("Sanity");
        playing = false;
        win = false;
        isFastForwarding = false;
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

    public int GetMaxSanity()
    {
        return maxSanity;
    }
    public int CurrentSanity()
    {
        return Sanity;
    }

    public void LoseSanity(int amount)
    {
        Sanity -= amount;
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

    public void SetFastForward(bool state)
    {
        isFastForwarding = state;
    }

    public float FastForwarding()
    {
        return isFastForwarding ? FastForwardSpeed : 1.0f;
    }

    public void SaveSanity()
    {
        PlayerPrefs.SetInt("Sanity", Sanity);
    }
}
