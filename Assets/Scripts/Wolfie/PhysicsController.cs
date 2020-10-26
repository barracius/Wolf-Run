using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    [SerializeField] private MainController _mainController;
    private const int MovementSpeed = 5;
    private void FixedUpdate()
    {
        if (_mainController.collisionController.stunned)
        {
            transform.Translate(MovementSpeed * Time.deltaTime * Vector3.left);
        }
    }
}
