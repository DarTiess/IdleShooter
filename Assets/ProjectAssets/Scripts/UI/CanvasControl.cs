﻿using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class CanvasControl : MonoBehaviour
{
    
    [Header("Panels")]
    [SerializeField] private CanvasGroup _panelMenu;
    [SerializeField] private CanvasGroup _panelInGame;
    [SerializeField] private CanvasGroup _panelWin;  
    [SerializeField] private CanvasGroup _panelLost;
    [SerializeField]private Text _timer;
    [SerializeField]private float _timeToStart;
    
     List<CanvasGroup> _canvasGroupes = new List<CanvasGroup>();

    LevelManager _levelManager;

    [Inject]
    void Initialization(LevelManager LevelManager)
    {
        _levelManager = LevelManager;
    }
    
    private void Start()
    {      
       _levelManager.OnLevelStart += OnLevelStart;
       _levelManager.OnLateWin += OnLevelWin; 
        _levelManager.OnLateLost += OnLevelLost;   

        _canvasGroupes.Add(_panelMenu);
        _canvasGroupes.Add(_panelInGame);
        _canvasGroupes.Add(_panelWin);
        _canvasGroupes.Add(_panelLost);

        SwitchOnAllCanvasObjects();
         ActivateUIScreen(_panelMenu);
    }

    void SwitchOnAllCanvasObjects()
    {
        foreach( CanvasGroup cG in _canvasGroupes )
        {
            cG.gameObject.SetActive(true);
        }
    }
    private void OnLevelStart()      
    {   
        int starting=(int)_timeToStart;
        _timer.DOCounter(starting, 0, _timeToStart)
            .OnComplete(()=>
            { 
                _levelManager.LevelPlay();
                _timer.gameObject.SetActive(false);
            });
        ActivateUIScreen(_panelInGame); 
    }
                             
    private void OnLevelWin()      
    {    
        Debug.Log("Level Win"); 
       ActivateUIScreen(_panelWin);  
    }

    private void OnLevelLost()           
    {                                                     
        Debug.Log("Level Lost");  
       ActivateUIScreen(_panelLost);
    }

    // out to Level Manager
    public void LoadNextLevel()   
    {
        _levelManager.LoadNextLevel();
    }  
    public void LevelStart()
    {
        _levelManager.LevelStart();
    }

     void ActivateUIScreen(CanvasGroup uiScreen)
    {
        foreach (CanvasGroup cGr in _canvasGroupes)
        {
            if (cGr != uiScreen)
            {
                cGr.alpha = 0;
                cGr.interactable = false;
                cGr.blocksRaycasts = false;
            }
            else
            {
                cGr.alpha =1;
                cGr.interactable = true;
                cGr.blocksRaycasts =true;
            }
        }
    }
    
    
}
