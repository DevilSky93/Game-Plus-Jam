using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerFront : MonoBehaviour
{
    [SerializeField] private string layerToPush;
    [SerializeField] private int sortingOrder;

    private void Start()
    {
        GetComponent<Renderer>().sortingLayerName = layerToPush;
        GetComponent<Renderer>().sortingOrder = sortingOrder;
    }

    public void ChangeLayer()
    {
        GetComponent<Renderer>().sortingLayerName = layerToPush;
        GetComponent<Renderer>().sortingOrder = sortingOrder;
    }
}
