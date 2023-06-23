using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WheelManager : MonoBehaviour
{
    [SerializeField] private List<Transform> wheels;
    [SerializeField] private Player player;
    [SerializeField] private Button spinButton;
    [SerializeField] private List<SpinAnimation> animColumns;

    [SerializeField] private float timeForSpinInSec = 3;
    public static Action wheel;
    public static Action updatePresenter;
    public static Action<float> win;
    public static Action<int> freeSpins;

    private int spinCost = 1;

    private SymbolType[,] recievedSymbols = new SymbolType[3, 5];

    private void OnEnable()
    {
        for (int i = 0; i <animColumns.Count; i++)
        {
            animColumns[i].Initialize();
        }
    }

    public void SetBet(int value)
    {
        spinCost = value;
        updatePresenter?.Invoke();
    }

    public int GetSpinCost()
    {
        return spinCost;
    }

    private IEnumerator WheelCoroutine()
    {
        if (player.GetFreeSpins() > 0)
        {
            for (int i = 0; i < animColumns.Count; i++)
            {
                animColumns[i].SetVisibility(true);
            }
            wheel?.Invoke();
            updatePresenter?.Invoke();
            yield return new WaitForSeconds(timeForSpinInSec);
            for (int i = 0; i < wheels.Count; i++)
            {
                for (int j = 0; j < wheels[i].childCount; j++)
                {
                    recievedSymbols[i,j] = wheels[i].GetChild(j).GetComponent<Symbol>().GetType();
                }
            }
            foreach (var tube in wheels)
            {
                for (int i = 0; i < tube.transform.childCount; i++)
                {
                    tube.GetChild(i).GetComponent<Symbol>().Visibility(false);
                }
            }
            CheckWinLines(20);
        }
        else
        {

            if (player.WithdrawFromBalance(spinCost))
            {
                for (int i = 0; i < animColumns.Count; i++)
                {
                    animColumns[i].SetVisibility(true);
                }
                foreach (var tube in wheels)
                {
                    for (int i = 0; i < tube.childCount; i++)
                    {
                        tube.GetChild(i).GetComponent<Symbol>().Visibility(false);
                    }
                }

                wheel?.Invoke();
                updatePresenter?.Invoke();
                yield return new WaitForSeconds(timeForSpinInSec);
                for (int i = 0; i < wheels.Count; i++)
                {
                    for (int j = 0; j < wheels[i].transform.childCount; j++)
                    {
                        recievedSymbols[i, j] = wheels[i].transform.GetChild(j).GetComponent<Symbol>().GetType();
                    }
                }
                
                foreach (var tube in wheels)
                {
                    for (int i = 0; i < tube.childCount; i++)
                    {
                        tube.GetChild(i).GetComponent<Symbol>().Visibility(true);
                    }
                }

                for (int i = 0; i < animColumns.Count; i++)
                {
                    animColumns[i].SetVisibility(false);
                }

                CheckWinLines(spinCost);
            }
        }

        spinButton.interactable = true;
    }

    private void CheckWinLines(int cost)
    {
        for (int i = 0; i < 5; i++)
        {
            if (recievedSymbols[0, i] == recievedSymbols[1, i] && recievedSymbols[0, i] == recievedSymbols[2, i])
            {
                GetWin(recievedSymbols[0,i], spinCost);
            }
        }
    }

    private void GetWin(SymbolType type, int cost)
    {
        float winPay = 0;
        switch (type)
        {
            case SymbolType.Cherry:
            {
                winPay = cost * 1.2f;
                break;
            }
            case SymbolType.WaterMelon:
            {
                winPay = cost * 1.5f;
                break;
            }
            case SymbolType.Orange:
            {
                winPay = cost * 1.7f;
                break;
            }
            case SymbolType.Lemon:
            {
                winPay = cost * 2;
                break;
            }
            case SymbolType.Seven:
            {
                winPay = cost * 5;
                break;
            }
            case SymbolType.Crown:
            {
                player.AddFreeSpins(3);
                player.ReplenishOnBalance(cost);
                freeSpins?.Invoke(3);
                return;
            }
        }
        player.ReplenishOnBalance(winPay);
        win?.Invoke(winPay);
    }

    public void Wheel()
    {
        spinButton.interactable = false;
        StartCoroutine(WheelCoroutine());
    }
}