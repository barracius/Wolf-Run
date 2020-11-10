using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = -5f;


    private void Update()
    {
        var transform1 = transform;
        var position = transform1.position;
        position = new Vector2(position.x + moveSpeed * Time.deltaTime, position.y);
        transform1.position = position;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.transform.CompareTag("Fire")) return;
        Destroy(gameObject); 
        GameControl.Instance.obstaclesInScene.RemoveAt(0);
    }
}
