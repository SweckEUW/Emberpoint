using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DashCounter : MonoBehaviour
{
    
    public Image firstBar;
    public Image secondBar;
    
    private Sprite filledBarSprite;
    private Sprite depletedBarSprite;
    void Awake()
    {
        filledBarSprite =
            Resources.Load<Sprite>("Sprites/GUI Icons/Icon_dash_bar");
        depletedBarSprite =
             Resources.Load<Sprite>("Sprites/GUI Icons/Icon_dash_bar_grey");
        
        firstBar.sprite = filledBarSprite;
        secondBar.sprite = filledBarSprite;
    }

    public void SetValue(int dashCount)
    {
        switch (dashCount)
        {
            case 0:
                firstBar.sprite = depletedBarSprite;
                secondBar.sprite = depletedBarSprite;
                break;
            case 1:
                firstBar.sprite = filledBarSprite;
                secondBar.sprite = depletedBarSprite;
                break;
            case 2:
                firstBar.sprite = filledBarSprite;
                secondBar.sprite = filledBarSprite;
                break;
        }
    }
}
