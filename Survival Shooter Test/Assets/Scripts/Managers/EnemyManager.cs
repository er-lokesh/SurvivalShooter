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
            var lastEnemyId = -1;
            if (enemyDataList != null && enemyDataList.Count > 0)
            {
                foreach (var enemyData in enemyDataList)
                {
                    Vector3 ePos = new Vector3(enemyData.position.x, enemyData.position.y, enemyData.position.z);
                    Quaternion eRot = Quaternion.Euler(enemyData.rotation.x, enemyData.rotation.y, enemyData.rotation.z);
                    var enemyObj = Instantiate(enemy, ePos, eRot);
                    var enemyHealth = enemyObj.GetComponent<EnemyHealth>();
                    enemyHealth.CurrentHealth = enemyData.currentHealth;
                    enemyHealth.enemyId = enemyData.id;
                    lastEnemyId = enemyData.id;
                    enemyStackData.AddEntity(enemyType, new EnemyEntity() { id = enemyData.id, currentHealth = enemyData.currentHealth, position = enemyData.position, rotation = enemyData.rotation });
                    enemyHealth.OnEnemyDead += OnEnemyDead;
                    enemyHealthData.Add(enemyHealth);
                }
                enemyId = ++lastEnemyId;
            }
        }
    }

    public void SaveData(GameData data)
    {
        List<EnemyEntity> enemyEntities = new List<EnemyEntity>();
        foreach (var enemy in enemyHealthData)
        {
            EnemyEntity entity = new EnemyEntity()
            {
                id = enemy.enemyId,
                currentHealth = enemy.CurrentHealth,
                position = new SerializableVector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z),
                rotation = new SerializableVector3(enemy.transform.rotation.x, enemy.transform.rotation.y, enemy.transform.rotation.z) 
                //position = enemy.transform.position,
                //rotation = enemy.transform.rotation
            };

            enemyEntities.Add(entity);
        }
        enemyStackData.UpdateEntity(enemyType, enemyEntities);

        data.enemyDictData[enemyType] = enemyStackData.FetchEntites(enemyType);
    }

    public void ClearData(GameData data)
    {
        enemyId = 0;
        enemyHealthData.ForEach(x=>Destroy(x.gameObject));
        enemyStackData.ClearEntity(enemyType);
    }

    //private void OnApplicationQuit()
    //{
    //    DataPersistenceManager.instance.SaveGame();
    //}
}

[System.Serializable]
public enum EnemyType { Zombear, Zombunny, Hellephant }
