using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economics : MonoBehaviour
{
     public event Action<EnemyMovement> OnGetMoney;
     public event Action OnPayMoney;
    [SerializeField]private Text _money;
    public int Money
    {
        get { return PlayerPrefs.GetInt("Money");; }
        set { PlayerPrefs.SetInt("Money", value); }
    }

    private void Start()
    {
        _money.text=Money.ToString();
    }

    public void GetMoney(int count, EnemyMovement enemy)
    {
        int result=Money+count;
        
        _money.DOCounter(Money, result, 0.5f)
            .OnPlay(() =>
            {
                _money.transform.DOScale(1.5f, 0.5f)
                .OnComplete(() =>
                {
                    _money.transform.DOScale(1, 0.5f);
                });
            })
            .OnComplete(() =>
            {
                Money += count; 
            });
        OnGetMoney?.Invoke(enemy);
    }
                          
    public bool CanPayPrice(int price)     
    { 
        if (price > Money) return false;
        return true;
    }
}
