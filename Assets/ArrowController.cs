using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
     private Image _arrow;
     private IJoystiсk _joystick ;

    private void Awake()
    {
        _arrow = GetComponent<Image>();
        _joystick = Joystick.Instance;
    }
    private void Update()
    {
        _arrow.fillAmount = _joystick.DistanceRate;
    }
}
