using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectWidth = rb.GetComponent<Collider2D>().bounds.size.x / 2;
        objectHeight = rb.GetComponent<Collider2D>().bounds.size.y / 2;

        CalculateScreenBounds();
    }

    void LateUpdate()
    {
        Vector3 viewPos = rb.position;
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        rb.position = viewPos;
    }

    void CalculateScreenBounds()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        screenBounds = new Vector2(cameraWidth / 2, cameraHeight / 2);
    }
}
