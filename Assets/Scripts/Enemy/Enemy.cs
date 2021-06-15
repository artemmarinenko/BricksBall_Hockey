using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Singleton<Enemy>
{
    [SerializeField] private Transform _crusher;
    [SerializeField] private float _speed = 1;
    private Vector3 _chasePosition;

    public float Speed { get { return _speed; } set { _speed = value; } }
    
    public Transform Crusher { get { return _crusher; } set { _crusher = value; } }
    
    void FixedUpdate()
    {
        if (_crusher != null) {
            _chasePosition.x = Mathf.Lerp(transform.position.x, _crusher.position.x, _speed * Time.fixedDeltaTime);
            _chasePosition.y = transform.position.y;
            _chasePosition.z = transform.position.z;
            transform.position = _chasePosition;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Crusher>(out Crusher crusher))
        {
            EventAggregator.Post(this, new OnCrusherDestroyEvent());
            Destroy(crusher.gameObject);
            
        }
    }
}
