using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image hpImage;
    [SerializeField] private LifeManager lifeManager;

    void Awake()
    {
        lifeManager.OnLifeChanged += HandleLifeChanged;
    }

    void OnDestroy()
    {
        lifeManager.OnLifeChanged -= HandleLifeChanged;
    }
    private void HandleLifeChanged(object sender, float life)
    {
        hpImage.fillAmount = life / lifeManager.FullLife;
    }

}
