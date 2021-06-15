using GameEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{   [SerializeField] private float _speedIncrease = 0.5f;

    [SerializeField] private Transform _crusherStartPosition;
    [SerializeField] private Crusher _crusherPrefab;
    [SerializeField] private EnemyBox _enemyBoxPrefab;
    [SerializeField] private GameObject _enemyBoxes;
    
    private int _startEnemyBoxesAmount;
    private List<Vector3> _enemyBoxesPositions = new List<Vector3>();
    public static int _currentEnemyBoxesAmount;
    
    private void Awake()
    {
        EventAggregator.Subscribe<OnCrusherDestroyEvent>(OnCrusherDestroyEventEventHandler);
        EventAggregator.Subscribe<OnBoxDestroyEvent>(OnBoxDestroyEventEventHandler);

        
    }

    private void Start()
    {
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
    }

    private void SaveEnemyBoxesPositions()
    {
        var enemyBoxes = _enemyBoxes.GetComponentsInChildren<EnemyBox>();
        foreach(var enemyBox in enemyBoxes)
        {
            _enemyBoxesPositions.Add(enemyBox.transform.position);
        }
    }

    

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
}
