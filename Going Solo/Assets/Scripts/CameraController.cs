using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float movementSpeed;

    private Vector3 playerPos;
    private static bool cameraExists;
    private Rigidbody2D rBody;

    public BoxCollider2D moveBoundary;
    private Vector3 minBounds, maxBounds;

    private Camera mainCamera;
    private float halfHeight, halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();

        mainCamera = GetComponent<Camera>();
        halfHeight = mainCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

        minBounds = moveBoundary.bounds.min;
        maxBounds = moveBoundary.bounds.max;

        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = playerPos * movementSpeed;

        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void SetBounds(BoxCollider2D newBoundary)
    {
        moveBoundary = newBoundary;

        minBounds = moveBoundary.bounds.min;
        maxBounds = moveBoundary.bounds.max;
    }
}
