using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ZonePostProcessing : MonoBehaviour
{
    [SerializeField] private GameObject zone;
    [SerializeField] private Transform player;
    private UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments;

    private Color normalColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    private Color zoneColor = new Color(0.75f, 0.25f, 0.25f, 0.0f);

    void Start()
    {
        UnityEngine.Rendering.VolumeProfile volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;

        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        if (!volumeProfile.TryGet(out colorAdjustments)) throw new System.NullReferenceException(nameof(colorAdjustments));

    }

    void Update()
    {
        if (zone.GetComponent<ZoneUpdater>().isPointInsideZone(player.position)) 
        {
            transitionToSafeZone(); 
        }
        else 
        { 
            transitionToDangerZone(); 
        }
    }

    private void transitionToSafeZone()
    {
        if (colorAdjustments.colorFilter.value != normalColor) ;
        {
            colorAdjustments.colorFilter.Override(Color.Lerp(colorAdjustments.colorFilter.value, normalColor, Time.deltaTime * 0.9f));
        }
    }

    private void transitionToDangerZone()
    {
        if (colorAdjustments.colorFilter.value != zoneColor) ;
        {
            colorAdjustments.colorFilter.Override(Color.Lerp(colorAdjustments.colorFilter.value, zoneColor, Time.deltaTime * 0.9f));
        }
    }
}