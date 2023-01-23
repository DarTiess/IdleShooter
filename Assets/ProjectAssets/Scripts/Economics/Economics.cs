using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economics : MonoBehaviour
{
#pragma warning disable 0649 
     
    public int Money
    {
        get { return PlayerPrefs.GetInt("Money");; }
        set { PlayerPrefs.SetInt("Money", value); }
    }
    
    public static Economics Instance;   
    
#pragma warning restore 0649 
    
    private void Awake()
    {
        if (Instance == null) Instance = this;  
    }
                   
    public void UseMoney(int count)
    {
        Money += count; 
    }
                          
    public bool CanPayPrice(int price)     
    { 
        if (price > Money) return false;
        return true;
    }
}
