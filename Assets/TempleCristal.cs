using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempleCristal : MonoBehaviour  
{

   public GameObject CutSceneCamera;
   public GameObject PlayerCamera;
   public string AnimationName;
   public GameObject Zone;
   public bool done = false;

   private void Update() {
        if(!done){
            if(Input.GetMouseButtonDown (0)){
                if (Camera.main != null)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                    if (Physics.Raycast(ray, out hit, 10000.0f))
                    {
                        if (hit.transform.gameObject.name == name)
                        {
                            done = true;
                            CutSceneCamera.GetComponent<Camera>().enabled = true;
                            PlayerCamera.GetComponent<Camera>().enabled = false;
                            CutSceneCamera.GetComponent<Animation>().Play(AnimationName);

                            if (AnimationName == "CSTemple 1" || AnimationName == "CSTemple 3")
                                Zone.GetComponent<ZoneUpdater>().CreatePermanentZone(transform.position + new Vector3(0, -20, 0));
                            else
                                Zone.GetComponent<ZoneUpdater>().CreatePermanentZone(transform.position);

                            StartCoroutine(SwitchCameras());
                        }
                    }
                }
            }
        }
    }

   IEnumerator SwitchCameras(){
      yield return new WaitForSeconds(8);

      CutSceneCamera.GetComponent<Camera>().enabled = false;
      PlayerCamera.GetComponent<Camera>().enabled = true;

      if(AnimationName == "CSTemple 1")
         GameObject.Find("Outro Cutscene").GetComponent<OutroCutscene>().temple1Done = true;
      if(AnimationName == "CSTemple 2")
         GameObject.Find("Outro Cutscene").GetComponent<OutroCutscene>().temple2Done = true;
      if(AnimationName == "CSTemple 3")
         GameObject.Find("Outro Cutscene").GetComponent<OutroCutscene>().temple3Done = true;
   }
}
