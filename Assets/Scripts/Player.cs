using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    private Vector2 rawinput;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    [SerializeField] private float sidePadding = 0.5f;
    [SerializeField] private float topPadding = 5;
    [SerializeField] private float bottomPadding = 2;

    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0)) + new Vector3(sidePadding,bottomPadding);
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1)) - new Vector3(sidePadding,topPadding);
    }

    private void Move()
    {
        Vector3 delta = Time.deltaTime * moveSpeed * rawinput;
        Vector2 newPos = transform.position + delta;
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawinput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
