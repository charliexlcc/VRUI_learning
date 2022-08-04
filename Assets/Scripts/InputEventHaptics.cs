using UnityEngine;
using UnityEngine.EventSystems;

public class InputEventHaptics : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler {

    [System.Serializable]
    public class HapticsEvent
    {
        public float duration;
        public float frequency;
        public float amplitude;
        public OVRInput.Controller controller;
    }

    public HapticsEvent onDown;
    public HapticsEvent onEnter;

    private HapticsEvent currentEvent = null;
    private float currentTime = 0;
    private bool isPlaying = false;

    private void Update() {
        if (isPlaying) {
            currentTime += Time.deltaTime;
            if (currentTime >= currentEvent.duration) {
                Stop();
            }
        }
    }

    private void OnDisable() {
        Stop();
    }

    private void OnDestroy() {
        Stop();        
    }

    public void Play(HapticsEvent pEvent) {
        Stop();
        currentEvent = pEvent;
        currentTime = 0;
        isPlaying = true;
        OVRInput.SetControllerVibration(pEvent.frequency, pEvent.amplitude, pEvent.controller);
    }

    public void Stop() {
        OVRInput.SetControllerVibration(0, 0);
        isPlaying = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (onEnter.duration > 0) {
            Play(onEnter);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (onDown.duration > 0) {
            Play(onDown);
        }
    }
}
