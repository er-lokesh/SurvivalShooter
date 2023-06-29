using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStackHandler : MonoBehaviour
{
    private Dictionary<EnemyType, List<EnemyEntity>> enemyStackDict = new Dictionary<EnemyType, List<EnemyEntity>>();
    //private SerializableDictionary<EnemyType, List<EnemyEntity>> enemyStackDict = new SerializableDictionary<EnemyType, List<EnemyEntity>>();

    public void AddEntity(EnemyType type, EnemyEntity entity)
    {
        if (!enemyStackDict.ContainsKey(type))
            enemyStackDict.Add(type, new List<EnemyEntity>());

        enemyStackDict[type].Add(entity);
    }

    public void RemoveEntity(EnemyType type, int id)
    {
        var enemy = enemyStackDict[type].Find(x => x.id == id);
        enemyStackDict[type].Remove(enemy);
    }

    public void UpdateEntity(EnemyType type, EnemyEntity entity)
    {
        var enemy = enemyStackDict[type].Find(x => x.id == entity.id);
        enemy = entity;
    }

    public List<EnemyEntity> FetchEntites(EnemyType type)
    {
        if (!enemyStackDict.ContainsKey(type)) return null;
        return enemyStackDict[type];
    }
}

[Serializable]
public class EnemyEntity
{
    public int id;
    public Vector3 position;
    public Quaternion rotation;
    public int currentHealth;
}