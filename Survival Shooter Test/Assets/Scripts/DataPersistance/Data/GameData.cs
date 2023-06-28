using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public PlayerHealthData playerHealthData;
    public PlayerMovementData playerMovementData;
    public CameraPositionData cameraPositionData;
    public SerializableDictionary<EnemyType, List<EnemyData>> enemyDictData;

    public GameData()
    {
        playerHealthData = new PlayerHealthData();
        playerMovementData = new PlayerMovementData();
        cameraPositionData = new CameraPositionData();
        enemyDictData = new SerializableDictionary<EnemyType, List<EnemyData>>();
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
