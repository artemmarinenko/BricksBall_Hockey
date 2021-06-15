using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    private IJoystiсk _joystick;
    private Vector3 _direction;

    private void Awake()
    {
        _joystick = Joystick.Instance;
    }

    private void FixedUpdate()
    {
        _direction.x = _joystick.Direction.x;
        _direction.y = _joystick.Direction.z;
        _direction.z = _joystick.Direction.y;

        transform.forward = -Vector3.Lerp(transform.forward, _direction, 100 * Time.deltaTime);        
    }
   
}
