using UnityEngine;

public class DMMSizer : MonoBehaviour {

    public Transform headTransform;
    public Transform sizeTransform;
    public bool faceHead = false;

    private Vector3 scale = new Vector3();

    private void Update() {
        float distance = Vector3.Distance(headTransform.position, sizeTransform.position);
        scale.x = scale.y = scale.y = distance * 0.001f;
        sizeTransform.localScale = scale;

        if (faceHead) {
            sizeTransform.LookAt(headTransform);
        }
    }
}
