using UnityEngine;

public class RayPointer : MonoBehaviour {

    public LineRenderer lineRenderer;
    public Transform itemToTrack;
    public float restingLength = 0.3f;

    private void Update() {
        Vector3 position = itemToTrack.gameObject.activeSelf
            ? lineRenderer.transform.parent.InverseTransformPoint(itemToTrack.position) - lineRenderer.transform.localPosition
            : Vector3.forward * restingLength;
        lineRenderer.SetPosition(1, position);
    }
}
