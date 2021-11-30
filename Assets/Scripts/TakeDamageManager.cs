using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class TakeDamageManager : MonoBehaviour
{
    [SerializeField] LifeManager lifeManager;
    private SpriteRenderer spriteRenderer;
    private TweenerCore<Color, Color, ColorOptions> takeDamageTween;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void OnDestroy()
    {
        SafeDestroyTween();
    }
    public void TakeDamage()
    {
        SafeDestroyTween();
        takeDamageTween = spriteRenderer.DOColor(Color.red, .2f).SetLoops(2, LoopType.Yoyo);
        lifeManager.Life--;
    }

    private void SafeDestroyTween()
    {
        if (takeDamageTween != null && takeDamageTween.active && takeDamageTween.IsPlaying())
        {
            takeDamageTween.Kill();
        }
    }
}
