﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null) { Debug.LogError("UI Manager is null"); }
            return _instance;
        }        
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField] private Text _collectableText;

    public void UpdateCollectableText(int collectableCount)
    {
        _collectableText.text = "Balls: " + collectableCount;
    }
}
