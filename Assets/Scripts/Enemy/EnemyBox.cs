using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBox : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Crusher>(out Crusher crusher))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            EventAggregator.Post(this, new OnEnemyBoxCrushedEvent());
        }
    }
}
