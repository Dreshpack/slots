using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpinController : MonoBehaviour
{
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_Text spinCostText;
    [SerializeField] private TMP_Text winText;
    [SerializeField] private TMP_Text freeSpinsText;
    [SerializeField] private Player player;
    [SerializeField] private WheelManager wheelManager;
    private void OnEnable()
    {
        UpdateUI();
        WheelManager.updatePresenter += UpdateUI;
        WheelManager.win += WinUI;
        WheelManager.freeSpins += FreeSpinUI;
    }
    
    private void OnDisable()
    {
        WheelManager.updatePresenter -= UpdateUI;
        WheelManager.win -= WinUI;
        WheelManager.freeSpins -= FreeSpinUI;
    }

    public void UpdateUI()
    {
        freeSpinsText.text = player.GetFreeSpins().ToString();
        winText.text = String.Empty;
        balanceText.text = $"Balance \n{player.GetBalance():0.0}$";
        spinCostText.text = $"Bet \n{wheelManager.GetSpinCost().ToString()}$";
    }

    private void FreeSpinUI(int spins)
    {
        freeSpinsText.text = player.GetFreeSpins().ToString();
        balanceText.text = $"Balance \n{player.GetBalance():0.0}$";
        winText.text = $"Wow you have won {spins} FREE spins";
    }

    private void WinUI(float winValue)
    {
        freeSpinsText.text = player.GetFreeSpins().ToString();
        balanceText.text = $"Balance \n{player.GetBalance():0.0}$";
        winText.text = $"Wow you have won {winValue:0.0}";
    }
}
