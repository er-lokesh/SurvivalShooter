using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public PlayerHealthData playerHealthData;
    public PlayerMovementData playerMovementData;
    public CameraPositionData cameraPositionData;
    public ScoreData scoreData;
    public Dictionary<EnemyType, List<EnemyEntity>> enemyDictData;
    
    public GameData()
    {
        playerHealthData = new PlayerHealthData();
        playerMovementData = new PlayerMovementData();
        cameraPositionData = new CameraPositionData();
        scoreData = new ScoreData();
        enemyDictData = new Dictionary<EnemyType, List<EnemyEntity>>();
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
    public SerializableVector3 position;
    public SerializableVector3 rotation;
    
    public PlayerMovementData()
    {
        position = new SerializableVector3();
        rotation = new SerializableVector3();
    }    
}

[Serializable]
public class ScoreData
{
    public int score;
}

[Serializable]
public class CameraPositionData
{
    public SerializableVector3 position;
    
    public CameraPositionData()
    {
        position = new SerializableVector3(1, 15, -22);
    }    
}

[Serializable]
public class EnemyEntity
{
    public int id;
    public SerializableVector3 position;
    public SerializableVector3 rotation;
    public int currentHealth;

    public EnemyEntity()
    {
        position = new SerializableVector3();
        rotation = new SerializableVector3();
        currentHealth = 100;
    }
}

[System.Serializable]
public class SerializableVector3
{
    public float x;
    public float y;
    public float z;
    
    public SerializableVector3()
    {
        x = y = z = 0;
    }

    public SerializableVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}