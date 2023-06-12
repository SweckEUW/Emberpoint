using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour  
{

   public GameObject CutSceneCamera;
   public GameObject PlayerCamera;
   public string AnimationName;
   public GameObject Zone;
   public Vector3 position;

   private void Update() {
      // Debug.Log(Vector3.Distance(player.position,transform.position) < 20);
      if(Input.GetMouseButtonDown (0)){ 
         RaycastHit hit; 
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
         if (Physics.Raycast (ray,out hit,10000.0f)) {
            if(hit.transform.gameObject.name == name){
               CutSceneCamera.GetComponent<Camera>().enabled = true;
               PlayerCamera.GetComponent<Camera>().enabled = false;
               CutSceneCamera.GetComponent<Animation>().Play(AnimationName);

               Zone.GetComponent<ZoneUpdater>().CreatePermanentZone(position);
            }
         }
      }
   }
}
