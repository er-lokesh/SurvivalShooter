using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IDataPersistence
{
    public EnemyStackHandler enemyStackData;
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public EnemyType enemyType;
    private int enemyId;

    private List<EnemyHealth> enemyHealthData = new List<EnemyHealth>();

    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        if(playerHealth.CurrentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        var enemyObj = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        var enemyHealth = enemyObj.GetComponent<EnemyHealth>();
        enemyStackData.AddEntity(enemyType, new EnemyEntity() { id = enemyId, currentHealth = 100 });
        enemyHealth.enemyId = enemyId++;
        enemyHealth.OnEnemyDead += OnEnemyDead;
        enemyHealthData.Add(enemyHealth);
        //id = System.Guid.NewGuid().ToString();
    }

    private void OnEnemyDead(int id)
    {
        enemyStackData.RemoveEntity(enemyType, id);
        var enemy = enemyHealthData.Find(x => x.enemyId == id);
        enemyHealthData.Remove(enemy);
    }

    public void LoadData(GameData data)
    {
        List<EnemyEntity> enemyDataList; // = new List<EnemyData>();
        if (data.enemyDictData.Count > 0)
        {
            enemyDataList = data.enemyDictData[enemyType];
            if (enemyDataList != null && enemyDataList.Count > 0)
            {
                foreach (var enemyData in enemyDataList)
                {
                    var enemyObj = Instantiate(enemy, enemyData.position, enemyData.rotation);
                    var enemyHealth = enemyObj.GetComponent<EnemyHealth>();
                    enemyHealth.CurrentHealth = enemyData.currentHealth;

                    enemyStackData.AddEntity(enemyType, new EnemyEntity() { id = enemyData.id, currentHealth = enemyData.currentHealth, position = enemyData.position, rotation = enemyData.rotation });
                    enemyHealth.enemyId = enemyId++;
                    enemyHealth.OnEnemyDead += OnEnemyDead;
                    enemyHealthData.Add(enemyHealth);
                }
            }
        }
    }

    public void SaveData(GameData data)
    {
        foreach (var enemy in enemyHealthData)
        {
            EnemyEntity entity = new EnemyEntity()
            {
                id = enemy.enemyId,
                currentHealth = enemy.CurrentHealth,
                position = enemy.transform.position,
                rotation = enemy.transform.rotation
            };

            enemyStackData.UpdateEntity(enemyType, entity);
        }

        data.enemyDictData[enemyType] = enemyStackData.FetchEntites(enemyType);
    }

    //private void OnApplicationQuit()
    //{
    //    DataPersistenceManager.instance.SaveGame();
    //}
}

[System.Serializable]
public enum EnemyType { Zombear, Zombunny, Hellephant }
