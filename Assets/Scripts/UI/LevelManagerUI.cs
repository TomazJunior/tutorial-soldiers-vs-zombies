using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelManagerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI remainingEnemiesText;
    [SerializeField] TextMeshProUGUI waveText;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.OnCoinsChanged += HandleCoinsChanged;
        LevelManager.instance.OnRemainingEnemiesChanged += HandleRemainingEnemiesChanged;
        LevelManager.instance.OnWaveChanged += HandleWaveChanged;
    }

    private void HandleWaveChanged(object sender, int wave)
    {
        waveText.text = $"Wave {wave}";
    }

    private void HandleRemainingEnemiesChanged(object sender, int remainingEnemies)
    {
        remainingEnemiesText.text = remainingEnemies.ToString();
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
