using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    [SerializeField] private Joystick _joystick;

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_joystick.Direction.x, _joystick.Direction.z, _joystick.Direction.y);
        transform.forward = -Vector3.Lerp(transform.forward, direction, 100 * Time.deltaTime);        
    }
   
}
