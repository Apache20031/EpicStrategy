using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "ScriptableObjects/EnemiesData")]
public class EnemiesData : ScriptableObject
{
    [SerializeField] private List<EnemyObjectData> _enemies;

    public List<EnemyObjectData> Enemies => _enemies;
}

[System.Serializable]
public class EnemyObjectData
{
    [SerializeField] private string _enemyName;
    [SerializeField] private GameObject _enemyObject;

    public string EnemyName => _enemyName;
    public GameObject EnemyObject => _enemyObject;
}
