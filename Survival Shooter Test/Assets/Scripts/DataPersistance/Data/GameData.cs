using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public PlayerHealthData playerHealthData;
    public PlayerMovementData playerMovementData;
    public CameraPositionData cameraPositionData;
    public Dictionary<EnemyType, List<EnemyEntity>> enemyDictData;
    //public SerializableDictionary<EnemyType, List<EnemyEntity>> enemyDictData;

    public GameData()
    {
        playerHealthData = new PlayerHealthData();
        playerMovementData = new PlayerMovementData();
        cameraPositionData = new CameraPositionData();
        enemyDictData = new Dictionary<EnemyType, List<EnemyEntity>>();
        //enemyDictData = new SerializableDictionary<EnemyType, List<EnemyEntity>>();
    }
}

[Serializable]
public class PlayerHealthData
{
    public int currentHealth;

    public PlayerHealthData()
    {
        currentHealth = 100;
    }    
}

[Serializable]
public class PlayerMovementData
{
    public Vector3 position;
    public Quaternion rotation;

    public PlayerMovementData()
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;
    }    
}

[Serializable]
public class CameraPositionData
{
    public Vector3 position;
    
    public CameraPositionData()
    {
        position = new Vector3(1, 15, -22);
    }    
}


[Serializable]
public class EnemyData
{
    //public EnemyType enemyType;
    public Vector3 position;
    public Quaternion rotation;
    public int currentHealth;

    public EnemyData()
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;
        currentHealth = 100;
    }
}

[System.Serializable]
public class SerializableVector3
{
    public float x;
    public float y;
    public float z;
}