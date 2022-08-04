using UnityEngine;

[RequireComponent(typeof(CanvasGroup), typeof(OVRRaycaster))]
public class InputAlphaWatcher : MonoBehaviour {

    private CanvasGroup canvasGroup;
    private OVRRaycaster raycaster;
    private float lastAlphaValue = 1;

    private void Awake() {
        raycaster = GetComponent<OVRRaycaster>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update() {

        // If the alpha has changed
        if (!Mathf.Approximately(canvasGroup.alpha, lastAlphaValue)) {

            // If changed from hidden to visible
            if (canvasGroup.alpha > 0 && Mathf.Approximately(lastAlphaValue, 0)) {
                raycaster.enabled = true;
            }

            // If changed from visible to hidden
            else if(lastAlphaValue > 0 && Mathf.Approximately(canvasGroup.alpha, 0)){
                raycaster.enabled = false;
            }

            lastAlphaValue = canvasGroup.alpha;
        }
    }
}
