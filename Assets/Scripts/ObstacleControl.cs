using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = -5f;

    // Update is called once per frame
    private void Update()
    {
        var transform1 = transform;
        var position = transform1.position;
        position = new Vector2(position.x + moveSpeed * Time.deltaTime, position.y);
        transform1.position = position;
        if (transform.position.x < -13f)
            Destroy(gameObject);
        if (Input.GetKey("up"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Wolfie"))
            GameControl.instance.WolfieCrushes();
    }
    
}
