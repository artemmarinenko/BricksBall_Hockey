using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private float _rotationSpeed = 100f;
    private IJoystiсk _joystick;
    private Vector3 _direction;

    private void Awake()
    {
        _joystick = Joystick.Instance;
    }

    private void FixedUpdate()
    {
        RotateToDirection(_joystick.Direction, _rotationSpeed);
    }
   
    private void RotateToDirection(Vector3 direction, float speed)
    {
        _direction.x = direction.x;
        _direction.y = direction.z;
        _direction.z = direction.y;

        transform.forward = -Vector3.Lerp(transform.forward, _direction, speed * Time.deltaTime);
    }

}
