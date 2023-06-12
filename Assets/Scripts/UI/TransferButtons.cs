using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TransferButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image buttonImage;
    public Sprite standardImage;
    public Sprite hoverImage;
    
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    void Hover()
    {
        buttonImage.sprite = hoverImage;
    }

    void StopHover()
    {
        buttonImage.sprite = standardImage;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        StopHover();
    }
}
