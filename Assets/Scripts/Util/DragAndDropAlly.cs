using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using UnityEngine.UI;

public class DragAndDropAlly : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;
    private bool hasEnoughCoins = false;

    [SerializeField] LayerMask slotLayer;
    [SerializeField] AllyStats allyStats;
    [SerializeField] Vector2 boxAreaToCollide = Vector2.one;
    [SerializeField] TextMeshProUGUI coinText;
    private Image spriteImage;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
        coinText.text = allyStats.coins.ToString();
        spriteImage = GetComponent<Image>();
    }

    void Start()
    {
        LevelManager.instance.OnCoinsChanged += HandleCoinsChanged;
    }

    private void HandleCoinsChanged(object sender, int coins)
    {
        hasEnoughCoins = coins >= allyStats.coins;
        if (!hasEnoughCoins)
        {
            GetComponent<Image>().color = Color.black;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!hasEnoughCoins)
        {
            eventData.pointerDrag = null;
            return;
        }
        canvasGroup.alpha = .7f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;

        Collider2D collider2D = Physics2D.OverlapBox(transform.position, boxAreaToCollide, 0, slotLayer);
        if (collider2D)
        {
            AllySlot allySlot = collider2D.GetComponent<AllySlot>();
            if (!allySlot.HasAlly())
            {
                allySlot.SetAlly(allyStats);
            }
        }
        rectTransform.anchoredPosition = initialPosition;
    }
}
