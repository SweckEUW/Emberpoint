using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private RectTransform rectTransform;

    public bool mouseIsOverButton;
    private Text buttonText;
    public Color buttonTextColor;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        buttonText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        mouseOverButton();
    }

    void mouseOverButton()
    {
        bool xCrossed = Mouse.current.position.ReadValue().x > rectTransform.position.x  - rectTransform.rect.width/2 &&
                        Mouse.current.position.ReadValue().x < rectTransform.position.x + rectTransform.rect.width/2;

        bool yCrossed = Mouse.current.position.ReadValue().y > rectTransform.position.y - rectTransform.rect.height / 2 &&
                        Mouse.current.position.ReadValue().y < rectTransform.position.y + rectTransform.rect.height / 2;

        mouseIsOverButton = xCrossed && yCrossed;

        if (mouseIsOverButton)
        {
            buttonText.color = Color.white;
        }
        else
        {
            buttonText.color = buttonTextColor;
        }
    }
}
