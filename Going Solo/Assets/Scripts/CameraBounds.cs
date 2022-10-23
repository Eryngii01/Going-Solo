using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private BoxCollider2D boundary;
    private CameraController mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        boundary = GetComponent<BoxCollider2D>();
        mainCamera = FindObjectOfType<CameraController>();

        mainCamera.SetBounds(boundary);
    }
}
