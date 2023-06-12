using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitScreen : MonoBehaviour
{
    public Image hitScreen;

    void Start()
    {
        hitScreen = GetComponent<Image>();
    }
    
    public void TweenColor()
    {
        Color tempColor = hitScreen.color;
        
        LeanTween.value(gameObject, 0, 60, 0.15f)
            .setEaseInSine()
            .setOnUpdate((value) =>
            {
                tempColor.a = value;
                hitScreen.color = tempColor;
            })
            .setLoopPingPong(1);
    }
}
