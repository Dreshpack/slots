using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float balance;
    private int freeSpins;

    private void OnEnable()
    {
        balance = PlayerPrefs.GetFloat("balance");
    }

    public void SavePlayerConsumables()
    {
        PlayerPrefs.SetFloat("balance", balance);
        PlayerPrefs.SetInt("freeSpins", freeSpins);
    }

    public bool WithdrawFromBalance(float cost)
    {
        if (balance >= cost)
        {
            balance -= cost;
            PlayerPrefs.SetFloat("balance", balance);
            return true;
        }
        return false;
    }

    public void BalanceMultiplier(float multiplier)
    {
        balance = balance * multiplier;
    }

    public void ReplenishOnBalance(float money)
    {
        PlayerPrefs.SetFloat("balance", balance);
        balance += money;
    }

    public float GetBalance()
    {
        return balance;
    }

    public void AddFreeSpins(int spins)
    {
        freeSpins += spins;
    }

    public int GetFreeSpins()
    {
        return freeSpins;
    }
    
    
}
