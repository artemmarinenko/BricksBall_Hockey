using GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Transform _crusherStartPosition;
    [SerializeField] private Crusher _crusherPrefab;
    [SerializeField] private Player _playerPrefab;

    
    private void Awake()
    {
        EventAggregator.Subscribe<OnEnemyBoxCrushedEvent>(OnEnemyBoxCrushedEventHandler);
    }

    private void OnEnemyBoxCrushedEventHandler(object sender, OnEnemyBoxCrushedEvent onEnemyBoxCrushedEvent)
    {
        CreateCrusher();
    }

    private void CreateCrusher()
    {
        Instantiate(_crusherPrefab, Player.Instance.transform);
    }
}
