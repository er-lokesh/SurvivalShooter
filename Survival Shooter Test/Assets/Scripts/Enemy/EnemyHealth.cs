using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDataPersistence
{
    public int startingHealth = 100;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    int currentHealth;

    public EnemyType enemyType;

    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (Vector3.down * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead) return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }

    public void LoadData(GameData data)
    {

    }

    public void SaveData(GameData data)
    {
        EnemyData enemyData = new EnemyData()
        {
            currentHealth = this.currentHealth,
            position = transform.position,
            rotation = transform.rotation
        };

        if (!data.enemyDictData.ContainsKey(enemyType))
            data.enemyDictData.Add(enemyType, new List<EnemyData>());
        
        data.enemyDictData[enemyType].Add(enemyData);
    }
}
