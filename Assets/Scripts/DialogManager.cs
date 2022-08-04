using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour {

    [System.Serializable]
    public class DialogDefinition {
        public string name;
        public Animator animator;
        public Transform transform;
    }

    // Unity editor exposed properties
    public List<DialogDefinition> dialogs = new List<DialogDefinition>();
    public OVRInput.Button openButton;
    public OVRInput.Controller openController;
    public Transform headAnchor;
    public float rotationLerpValue = 0;
    public Animator overlayAnimator;

    private DialogDefinition currentDialogDefinition;

    private void Update() {

        if (OVRInput.GetDown(openButton, openController)) {
            Open();
        }

        if (currentDialogDefinition != null) {
            currentDialogDefinition.transform.position = headAnchor.position;

            // Slowly match the users head rotation
            if (rotationLerpValue > 0) {
                currentDialogDefinition.transform.rotation = Quaternion.Lerp(currentDialogDefinition.transform.rotation, headAnchor.rotation, rotationLerpValue);
            }
        }
    }

    public void Open(string pDialogKey = "Alert") {

        DialogDefinition dialogDefinition = dialogs.Find(itemToFind => itemToFind.name == pDialogKey);
        if (currentDialogDefinition == null && dialogDefinition != null) {
            currentDialogDefinition = dialogDefinition;

            // Ignore all physics
            SetPhysicsRaycaster(false);

            // Turn off all OVR Raycasters and then fiend and enable the raycaster for the dialog
            SetAllCanvasRaycasters(false);
            OVRRaycaster dialogRaycaster = currentDialogDefinition.transform.GetComponentInChildren<OVRRaycaster>();
            dialogRaycaster.enabled = true;

            // Disable all external button UI invocation
            SetAllVisibilityToggles(false);

            // Rotate to match the user head
            currentDialogDefinition.transform.eulerAngles = new Vector3(0, headAnchor.eulerAngles.y, 0);

            // Trigger Animation
            SetDialogVisibility(true);
        }
    }

    public void Close() {
        if (currentDialogDefinition != null) {

            // Reset all modified input
            SetPhysicsRaycaster(true);
            SetAllCanvasRaycasters(true);
            SetAllVisibilityToggles(true);

            // Close dialog and release reference
            SetDialogVisibility(false);
            currentDialogDefinition = null;
        }
    }

    private void SetPhysicsRaycaster(bool pEnabled) {
        OVRPhysicsRaycaster physicsRaycaster = FindObjectOfType<OVRPhysicsRaycaster>();
        physicsRaycaster.enabled = pEnabled;
    }

    private void SetAllCanvasRaycasters(bool pEnabled) {
        OVRRaycaster[] raycasters = FindObjectsOfType<OVRRaycaster>(true);
        for (int i=0; i<raycasters.Length; i++) {
            raycasters[i].enabled = pEnabled;
        }
    }

    private void SetAllVisibilityToggles(bool pEnabled) {
        VisibilityToggle[] toggles = FindObjectsOfType<VisibilityToggle>(true);
        for (int i = 0; i < toggles.Length; i++) {
            toggles[i].enabled = pEnabled;
        }
    }

    private void SetDialogVisibility(bool pIsVisible) {
        if (currentDialogDefinition.animator != null) {
            currentDialogDefinition.animator.SetBool("isOpen", pIsVisible);
        }
        else {
            currentDialogDefinition.transform.gameObject.SetActive(pIsVisible);
        }

        if (overlayAnimator != null) {
            overlayAnimator.SetBool("isOpen", pIsVisible);
        }
    }

}
