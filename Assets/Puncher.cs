using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    private Vector3 _startPosition;
    private IJoystiсk _joystick;

    private void Awake()
    {
        _startPosition = transform.localPosition;
        _joystick = Joystick.Instance;
    }
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.localPosition.z) < 3)
            transform.localPosition = new Vector3(_startPosition.x, _startPosition.y, Mathf.Lerp(_startPosition.z, -6 * _joystick.DistanceRate, 20 * Time.deltaTime));
        
    }
}
