using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    private Vector3 _startPosition;
    private IJoystiсk _joystick;
    private bool _isPunching = false;

   // public bool IsPunching { get { return _isPunching; } }

    private void Awake()
    {
        EventAggregator.Subscribe<OnDragEndEvent>(OnDragEndEventHandler);
        _startPosition = transform.localPosition;
        _joystick = Joystick.Instance;
    }

    private void OnDestroy()
    {
        EventAggregator.UnSubscribe<OnDragEndEvent>(OnDragEndEventHandler);
    }

    void FixedUpdate()
    {
        if (!_isPunching) {
            if (Mathf.Abs(transform.localPosition.z) < 6)
                transform.localPosition = new Vector3(_startPosition.x, _startPosition.y, Mathf.Lerp(_startPosition.z, -6 * _joystick.DistanceRate, 20 * Time.deltaTime));
        }
        else
        {
            transform.localPosition = new Vector3(_startPosition.x, _startPosition.y, Mathf.Lerp(transform.localPosition.z, _startPosition.z, 20 * Time.deltaTime));
            if (Mathf.Abs(transform.localPosition.z - _startPosition.z) < 0.1f)
                _isPunching = false;           
        }
        
    }

    private void OnDragEndEventHandler(object sender, OnDragEndEvent onDragEndEvent)
    {
        _isPunching = true;
    }
}
