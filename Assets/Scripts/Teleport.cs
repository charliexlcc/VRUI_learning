using System;
using UnityEngine;

public class Teleport : MonoBehaviour {

    private const int MAX_RAYCAST_RESULTS = 10;

    // Unity editor exposed properties
    public Transform userTransform;
    public Transform handAnchor;
    public OVRInput.Button executeButton;
    public LayerMask layerMask;
    public Transform marker;
    public float maxDistance = 5;
    public float groundOffset = 1;

    private bool isAbleToMove = false;
    private Ray ray = new Ray();
    private RaycastHit[] raycastHits = new RaycastHit[MAX_RAYCAST_RESULTS];
    private RaycastHit closestPosition;

    private void Update() {

        // Calculate if and where the user can move
        isAbleToMove = IsAbleToMove();

        // Hide/show the marker and set it's position
        if (marker != null) {
            marker.gameObject.SetActive(isAbleToMove);
            if (isAbleToMove) {
                marker.position = closestPosition.point;
                marker.up = closestPosition.normal;
            }
        }

        // If the button is executed, move the user
        if (isAbleToMove && OVRInput.GetDown(executeButton)) {
            userTransform.position = closestPosition.point + (Vector3.up * groundOffset);
        }
    }

    private bool IsAbleToMove() {
        ray.origin = handAnchor.position;
        ray.direction = handAnchor.forward;
        float hitCount = Physics.RaycastNonAlloc(ray, raycastHits, maxDistance, layerMask);
        if (hitCount > 0){
            Array.Sort(raycastHits, RaycastHitComparer);
            closestPosition = raycastHits[0];
            return true;
        }

        return false;
    }

    private int RaycastHitComparer(RaycastHit pValueA, RaycastHit pValueB) {
        // Compare ray cast hit results by distance
        return pValueA.distance.CompareTo(pValueB.distance);
    }

}
