using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IDataPersistence
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public EnemyType enemyType;
    //private string id;

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
        enemyHealth.enemyType = this.enemyType;
        //id = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        List<EnemyData> enemyDataList; // = new List<EnemyData>();
        enemyDataList = data.enemyDictData[enemyType];
        if (enemyDataList.Count > 0)
        {
            foreach (var enemyData in enemyDataList)
            {
                var enemyObj = Instantiate(enemy, enemyData.position, enemyData.rotation);
                var enemyHealth = enemyObj.GetComponent<EnemyHealth>();
                enemyHealth.CurrentHealth = enemyData.currentHealth;
                enemyHealth.enemyType = this.enemyType;
            }
        }
    }

    public void SaveData(GameData data)
    {

    }
}

public enum EnemyType { Zombear, Zombunny, Hellephant }
