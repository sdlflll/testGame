using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMapGeneration : MonoBehaviour
{
    public Transform canvas;

    private void Awake()
    {
        canvas = transform.GetChild(0).transform;
    }
}
