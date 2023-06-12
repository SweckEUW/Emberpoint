using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplePath : MonoBehaviour
{
    [SerializeField] private float maxViewingDistance = 3;
    private Transform player;
    private Renderer rend;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        rend = GetComponent<Renderer>();

        if(rend != null)
        {
            rend.material.SetFloat("Vector1_e9db613eb7ca455aad706c75c62126a0", maxViewingDistance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && rend != null)
        {
            rend.material.SetVector("Vector3_1d058331d7bc48b8b9879efe189622a6", player.position);
        }
        
    }
}
