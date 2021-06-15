using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    Rigidbody _rigidbody;
    private bool _isPunched = false;
    private float _punchRate;
    private void Awake()
    {
        EventAggregator.Subscribe<OnDragEndEvent>(OnDragEndEventHandler);
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Puncher>(out Puncher puncher) && _isPunched)
        {
                transform.parent = null;
                _rigidbody.AddForce(puncher.transform.forward * 10 * _punchRate, ForceMode.Impulse);
                gameObject.layer = 8;
                _isPunched = false;           
        }
    }

    private void OnDragEndEventHandler(object sender, OnDragEndEvent onDragEndEvent)
    {
        _isPunched = true;
        _punchRate = onDragEndEvent.PunchRate;
    }
}
