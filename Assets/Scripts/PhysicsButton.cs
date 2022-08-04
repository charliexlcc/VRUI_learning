using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PhysicsButton : MonoBehaviour, IPointerClickHandler {

    public UnityEvent onClick;

    public void OnPointerClick(PointerEventData eventData) {
        onClick.Invoke();
    }
}
