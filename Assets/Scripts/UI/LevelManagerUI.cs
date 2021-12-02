using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelManagerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.OnCoinsChanged += HandleCoinsChanged;
    }

    private void HandleCoinsChanged(object sender, int coins)
    {
        coinsText.text = coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
