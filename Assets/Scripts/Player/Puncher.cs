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
    private Vector3 _positionDuringPunch;

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
        PunchControll();
    }
    private void PunchControll()
    {
        if (!_isPunching)
        {
            if (Mathf.Abs(transform.localPosition.z) < _distanceConstraint)
            {
                _positionDuringPunch.x = _startPosition.x;
                _positionDuringPunch.y = _startPosition.y;
                _positionDuringPunch.z = Mathf.Lerp(_startPosition.z, -_distanceConstraint * _joystick.DistanceRate, _speed * Time.fixedDeltaTime);
                transform.localPosition = _positionDuringPunch;
            }

        }
        else
        {
            _positionDuringPunch.x = _startPosition.x;
            _positionDuringPunch.y = _startPosition.y;
            _positionDuringPunch.z = Mathf.Lerp(transform.localPosition.z, _startPosition.z, _speed * Time.fixedDeltaTime);
            transform.localPosition = _positionDuringPunch;
            if (Mathf.Abs(transform.localPosition.z - _startPosition.z) < _lerpThreshold)
                _isPunching = false;
        }
    }
    private void OnDragEndEventHandler(object sender, OnDragEndEvent onDragEndEvent)
    {
        _isPunching = true;
    }
}
