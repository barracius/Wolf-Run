using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftCycle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float leftWayPointX = -8f, rightWayPointX = 8f;

    // Update is called once per frame
    void Update()
    {
        var transform1 = transform;
        var position = transform1.position;
        position = new Vector2(position.x + moveSpeed * Time.deltaTime, position.y);
        transform1.position = position;
        if (!(transform.position.x < leftWayPointX)) return;
        var transform2 = transform;
        transform2.position = new Vector2(rightWayPointX, transform2.position.y);
    }
}
