using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public int layer { get; private set; }

    public static EnemiesController Instance;

    [SerializeField]
    private Transform targetEnemies;

    [SerializeField]
    private float maxTimeToSpawnEnemy = 3f;
    [SerializeField]
    private float minTimeToSpawnEnemy = 1f;
    private float timeToSpawnEnemy;

    [SerializeField]
    private float minHorizontalDistanceMovement = 1f;

    [Header("Enemie Properties")]
    [Space]
    [Header("Health")]
    [SerializeField]
    private int minHealth = 1;
    [SerializeField]
    private int maxHealth = 20;
    [Header("Speed")]
    [SerializeField]
    private float minSpeed = 0.2f;
    [SerializeField]
    private float maxSpeed = 10f;
    [Header("Vision Distance")]
    [SerializeField]
    private float minVisionDistance = 2.0f;
    [SerializeField]
    private float maxVisionDistance = 10.0f;
    [Header("Step Length")]
    [SerializeField]
    private float minStepLength = 0.1f;
    [SerializeField]
    private float maxStepLength = 1f;
    [Header("Time Between Steps")]
    [SerializeField]
    private float minTimeBetweenSteps = 0.1f;
    [SerializeField]
    private float maxTimeBetweenSteps = 0.7f;
    [Header("Reward")]
    [SerializeField]
    private int minReward = 3;
    [SerializeField]
    private int maxReward = 15;
    [Header("Sprites")]
    [SerializeField]
    private List<Sprite> spritesEnemies;

    private float minPosY;
    private float maxPosY;
    private float minPosX;
    private float maxPosX;

    private ObjectPooler pooler;
    private GameObject enemySpawned;
    private EnemyController enemySpawnedControl;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        pooler = ObjectPooler.Instance;

        layer = this.gameObject.layer;
        minPosY = 3.0f;
        maxPosY = ScreenController.maxPosY + 1f;
        minPosX = ScreenController.minPosX + 1f; //for boundaries
        maxPosX = ScreenController.maxPosX - 1f;

        Restart();
        StartCoroutine("SpawnNewEnemy");
    }

    public void Restart()
    {
        timeToSpawnEnemy = maxTimeToSpawnEnemy;
    }

    private IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawnEnemy);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minPosX, maxPosX), Random.Range(minPosY, maxPosY));
        enemySpawned = pooler.SpawnFromPool(TypeOfPool.Enemies, randomPosition, Quaternion.identity);

        enemySpawnedControl = enemySpawned.GetComponent<EnemyController>();
        if(enemySpawned != null)
        {
            enemySpawnedControl.target = targetEnemies;

            SetRandomEnemyProperties();
        }

        if(timeToSpawnEnemy > minTimeToSpawnEnemy) //the more u play the more enemies u get
            timeToSpawnEnemy -= 0.05f;
    }

    private void SetRandomEnemyProperties()
    {
        enemySpawnedControl.health = Random.Range(minHealth, maxHealth);
        enemySpawnedControl.speed = Random.Range(minSpeed, maxSpeed);
        enemySpawnedControl.visionDistance = Random.Range(minVisionDistance, maxVisionDistance);
        enemySpawnedControl.stepLength = Random.Range(minStepLength, maxStepLength);
        enemySpawnedControl.timeBetweenSteps = Random.Range(minTimeBetweenSteps, maxTimeBetweenSteps);
        enemySpawnedControl.reward = Random.Range(minReward, maxReward);

        SetHorizontalMovement();

        enemySpawnedControl.SetSprite(spritesEnemies[Random.Range(0, spritesEnemies.Count)]);
    }

    private void SetHorizontalMovement()
    {
        float bound1 = Random.Range(minPosX, maxPosX);
        float bound2 = Random.Range(minPosX, maxPosX);

        if(bound1 == bound2)
        {
            Shuffle();
            return;
        }
        
        if(Mathf.Abs(bound1-bound2) < minHorizontalDistanceMovement)
        {
            if (bound1 > bound2)
                bound1 += minHorizontalDistanceMovement;
            else
                bound2 += minHorizontalDistanceMovement;
        }
       
        enemySpawnedControl.pos1X = bound1;
        enemySpawnedControl.pos2X = bound2;

    }

    private void Shuffle()
    {
        SetHorizontalMovement();
    }



}
