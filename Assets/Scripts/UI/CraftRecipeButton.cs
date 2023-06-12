using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftRecipeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public Image buttonImage;
    public Sprite standardImage;
    public Sprite hoverImage;

    public GameObject recipeOverlay;
    public bool overlayIsShown = false;
    
    void Start()
    {
        buttonImage = GetComponent<Image>();
        recipeOverlay.SetActive(false);
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
        if (!overlayIsShown)
        {
            StopHover();
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!overlayIsShown)
        {
            Hover();
            recipeOverlay.SetActive(true);
            overlayIsShown = true;
        }
        else
        {
            StopHover();
            recipeOverlay.SetActive(false);
            overlayIsShown = false;
        }
    }
    
    void Update()
    {
        
    }
}
