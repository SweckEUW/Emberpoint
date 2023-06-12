using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBuildingMode : MonoBehaviour
{
    [SerializeField] private Material possiblePositionMat;
    [SerializeField] private Material blockedPositionMat;
    [SerializeField] private float maxPlaceDistance = 10f;
    [SerializeField] private float possibleNormalAngle = 30f; // max placeable angle between the ground normal to the up vector
    [SerializeField] private float collisionFactor = 1.2f; // factor by which the collision detection around the placable object is increased
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private LayerMask placeableLayer;

    private bool inBuildingMode = false;

    private bool isPossiblePosition = false;

    private bool inInventory = false;

    private GameObject buildPrefab;
    private GameObject buildObject;

    private void Awake()
    {
        inBuildingMode = false;
        isPossiblePosition = false;
        buildPrefab = null;
        buildObject = null;
    }

    public void StartBuildMode(Items.ActiveItems.ActiveItemPlaceables placeableItem)
    {
        inBuildingMode = true;
        buildPrefab = placeableItem.buildPreviewPrefab;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainLayer))
        {
            buildObject = Instantiate(buildPrefab,hit.point + (Vector3.up * transform.localScale.y) / 2, Quaternion.identity);
            buildObject.SetActive(false);
            buildObject.transform.up = hit.normal;
        }
    }

    public void SetPreviewActive(bool active)
    {
        if(buildObject != null)
        {
            buildObject.SetActive(active);
            inInventory = !active;
        }
    }

    public void Reset()
    {
        inBuildingMode = false;
        isPossiblePosition = false;
        if(buildObject != null)
        {
            Destroy(buildObject);
        }
        buildObject = null;
    }

    public Transform CheckPlaceable()
    {
        if (isPossiblePosition)
        {
            return buildObject.transform;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inInventory && inBuildingMode)
        {
            CheckBuildPosition();
        }
        
    }

    private void CheckBuildPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainLayer))
        {
            if (Vector3.Distance(hit.point, transform.position) > maxPlaceDistance)
            {
                buildObject.SetActive(false);
                ChangeToPositionBlocked();
                return;
                
            }

            buildObject.SetActive(true);
            buildObject.transform.position = hit.point;
            buildObject.transform.up = hit.normal;

            if (Vector3.Angle(Vector3.up,hit.normal) > possibleNormalAngle){
                ChangeToPositionBlocked();
                return;
            }

            // check if any other placeable is around the position of the desired location to place at
            Collider[] colliderArray = Physics.OverlapBox(
                hit.point,
                buildObject.GetComponent<BoxCollider>().bounds.extents * collisionFactor,
                buildObject.transform.rotation,
                placeableLayer
                );
            if (colliderArray.Length > 0)
            {
                ChangeToPositionBlocked();
                return;
            }

            ChangeToPositionPossible();



        }
    }

    private void ChangeToPositionBlocked()
    {
        if (isPossiblePosition)
        {
            isPossiblePosition = false;
            Renderer objectRenderer = buildObject.GetComponent<Renderer>();
            Material[] mats = new Material[objectRenderer.materials.Length];
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = blockedPositionMat;
            }
            objectRenderer.materials = mats;
        }
    }

    private void ChangeToPositionPossible()
    {
        if (!isPossiblePosition)
        {
            isPossiblePosition = true;
            Renderer objectRenderer = buildObject.GetComponent<Renderer>();
            Material[] mats = new Material[objectRenderer.materials.Length];
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = possiblePositionMat;
            }
            objectRenderer.materials = mats;
        }
    }



}
