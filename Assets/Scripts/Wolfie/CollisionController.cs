using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private MainController _mainController;
    internal bool onFire;
    internal bool stunned;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            onFire = true;
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            stunned = true;
        }
    }
}
