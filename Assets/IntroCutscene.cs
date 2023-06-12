using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour  
{

   public GameObject PlayerCamera;

    private Player.Player player;

   private void Awake() {
         GetComponent<Camera>().enabled = true;
        PlayerCamera.GetComponent<Camera>().enabled = false;
        player = GameObject.Find("Player").GetComponent<Player.Player>();
   }

   public void PlayIntro() {
        GetComponent<Animation>().Play("CSIntro");

        StartCoroutine(SwitchCameras());
   }

   IEnumerator SwitchCameras(){
      yield return new WaitForSeconds(20);

      GetComponent<Camera>().enabled = false;
      PlayerCamera.GetComponent<Camera>().enabled = true;
        player.StartGame();
   }
}
