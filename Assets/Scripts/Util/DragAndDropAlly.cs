using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropAlly : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;

    [SerializeField] LayerMask slotLayer;
    [SerializeField] AllyStats allyStats;
    [SerializeField] Vector2 boxAreaToCollide = Vector2.one;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
