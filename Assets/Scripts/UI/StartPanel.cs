using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _exitButton;

    void Awake()
    {
        _exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        _startButton.onClick.AddListener(() =>
        {            
            _startButton.GetComponentInChildren<Text>().text = StringHelper.Resume;
            gameObject.SetActive(false);
        });
    }

}
