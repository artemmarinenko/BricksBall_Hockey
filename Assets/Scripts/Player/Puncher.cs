using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    [SerializeField] private float _distanceConstraint = 6;
    [SerializeField] private float _speed = 20;
    [SerializeField] private float _lerpThreshold = 0.1f;

    private Vector3 _startPosition;
    private IJoystiсk _joystick;
    private bool _isPunching = false;

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
            if (Mathf.Abs(transform.localPosition.z) < _distanceConstraint)
                transform.localPosition = new Vector3(_startPosition.x, _startPosition.y, Mathf.Lerp(_startPosition.z, -_distanceConstraint * _joystick.DistanceRate, _speed * Time.deltaTime));
        }
        else
        {
            transform.localPosition = new Vector3(_startPosition.x, _startPosition.y, Mathf.Lerp(transform.localPosition.z, _startPosition.z, _speed * Time.deltaTime));
            if (Mathf.Abs(transform.localPosition.z - _startPosition.z) < _lerpThreshold)
                _isPunching = false;           
        }
        
    }

    private void OnDragEndEventHandler(object sender, OnDragEndEvent onDragEndEvent)
    {
        _isPunching = true;
    }
}
