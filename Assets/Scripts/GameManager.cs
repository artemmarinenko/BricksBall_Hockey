using GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{   [SerializeField] private float _speedIncrease = 0.5f;

    [SerializeField] private Transform _crusherStartPosition;
    [SerializeField] private Crusher _crusherPrefab;
    [SerializeField] private EnemyBox _enemyBoxPrefab;
    [SerializeField] private GameObject _enemyBoxes;

    [SerializeField] private Text _currentLvlText;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private StartPanel _menu;

    
    private int _startEnemyBoxesAmount;
    private List<Vector3> _enemyBoxesPositions = new List<Vector3>();

    private static int _currentEnemyBoxesAmount;
    private static int _currentLvl = 1;
    
    private void Awake()
    {
        EventAggregator.Subscribe<OnCrusherDestroyEvent>(OnCrusherDestroyEventEventHandler);
        EventAggregator.Subscribe<OnBoxDestroyEvent>(OnBoxDestroyEventEventHandler);       
    }

    private void Start()
    {
        PauseListenerStart();
        InitScore();
        SaveEnemyBoxesPositions();
    }

    private void CreateCrusher()
    {       
        Enemy.Instance.Crusher = Instantiate(_crusherPrefab, Player.Instance.transform).GetComponent<Crusher>().transform;
    }

    private void InitScore()
    {       
        _startEnemyBoxesAmount = _enemyBoxes.transform.childCount;
        _currentEnemyBoxesAmount = _startEnemyBoxesAmount;
        _currentLvlText.text = _currentLvl.ToString();
    }

    private void PauseListenerStart() {
        _pauseButton.onClick.AddListener(() =>
        {
            _menu.gameObject.SetActive(true);
        });
    }

    private void SaveEnemyBoxesPositions()
    {
        var enemyBoxes = _enemyBoxes.GetComponentsInChildren<EnemyBox>();
        foreach(var enemyBox in enemyBoxes)
        {
            _enemyBoxesPositions.Add(enemyBox.transform.position);
        }
    }
    
    private void LevelUp()
    {
        foreach(var enemyBoxPos in _enemyBoxesPositions)
        {
            Instantiate(_enemyBoxPrefab, enemyBoxPos,Quaternion.identity);
        }
        Enemy.Instance.Speed += _speedIncrease ;

        _currentEnemyBoxesAmount = _startEnemyBoxesAmount;
    }


    #region EventHanlers
    private void OnCrusherDestroyEventEventHandler(object sender, OnCrusherDestroyEvent onCrusherDestroyEvent)
    {
        CreateCrusher();
    }
    private void OnBoxDestroyEventEventHandler(object sender, OnBoxDestroyEvent onBoxDestroyEvent)
    {
        --_currentEnemyBoxesAmount;


        Debug.Log(_currentEnemyBoxesAmount);
        if (_currentEnemyBoxesAmount == 0)
        {
            LevelUp();
            _currentLvlText.text = (++_currentLvl).ToString();
        }
    }
    #endregion
}
