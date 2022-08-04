using UnityEngine;

public class SortingLayerSetter : MonoBehaviour {

    public Renderer rendererToSort;
    public string sortingLayerName;
    public int orderInSortingLayer;

    private void Awake() {
        rendererToSort.sortingLayerName = sortingLayerName;
        rendererToSort.sortingOrder = orderInSortingLayer;
    }
}
