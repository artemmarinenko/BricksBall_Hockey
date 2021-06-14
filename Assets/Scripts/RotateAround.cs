using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    private float angle;

    private void Update()
    {

        transform.RotateAround(_target.transform.position, Vector3.up, 20 * Time.deltaTime);
    }

}
