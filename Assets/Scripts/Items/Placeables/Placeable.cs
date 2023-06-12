using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items.Placeables
{ 
    /**
     * Responsible to place some items inside the game (like bench for example)
     */
    public class Placeable : MonoBehaviour
    {
        public GameObject preFab;
        // Placeable Object inside the Game. Will be instantiated once player has set a Position
        public GameObject placedItem;

        public GameObject selectedItemToPlace;

        public InputManager input;
        public GameObject player;
        


        private void Awake()
        {
            input = new InputManager();
            player = GameObject.FindWithTag("Player");
        }


        /**
        * Checks for collision with floor and
        * instantiates placeable object
        */
        protected void CheckForPlacingPlaceable()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Debug.Log("hit");
                Vector3 positionToPlace = ray.GetPoint(distance);
                placedItem = GameObject.Instantiate(preFab, positionToPlace + Vector3.up * 2, Quaternion.identity);
                placedItem.AddComponent<BoxCollider>();
            }
        }

        protected virtual void InteractWithPlaceable()
        {
            Debug.Log("NICE");
        }

        public void Update()
        {
            if (Mouse.current.middleButton.wasPressedThisFrame)
            {
                CheckForPlacingPlaceable();
            }
            
            
            if (Keyboard.current.fKey.wasPressedThisFrame &&
                Vector3.Distance(player.transform.position, gameObject.transform.position) <= 5)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit = new RaycastHit();
                Debug.Log(placedItem.GetComponent<BoxCollider>().Raycast(ray, out hit, 1000));
                if (placedItem.GetComponent<BoxCollider>().Raycast(ray, out hit, 1000))
                {
                    InteractWithPlaceable();
                }
            }
        }
    }
}