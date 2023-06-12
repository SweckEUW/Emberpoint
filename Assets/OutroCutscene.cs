using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroCutscene : MonoBehaviour  
{
    public bool temple1Done = false;
    public bool temple2Done = false;
    public bool temple3Done = false;

    private bool playOutro = false;
   public GameObject PlayerCamera;

   private void Update() {
       if( temple1Done && temple2Done && temple3Done && !playOutro){
           playOutro = true;
           playOutroAnimation();
       }
   }
   private void playOutroAnimation() {
        GetComponent<Camera>().enabled = true;
        PlayerCamera.GetComponent<Camera>().enabled = false;
        GetComponent<Animation>().Play("CSOutro");
   }

}
