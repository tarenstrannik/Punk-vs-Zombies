using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] hardObstaclesPrefabs;
    [SerializeField] private GameObject[] softObstaclesPrefabs;
    [SerializeField] private GameObject[] enemiesPrefabs;
    [SerializeField] private GameObject[] powerupsPrefabs;

    

    [SerializeField] private List<Material> backgroundTextures;
    [SerializeField] private GameObject plane;
    private MeshRenderer planeMeshRenderer;
    [SerializeField] private float powerupAndObstacleSpawnRangeX = 14f;
    [SerializeField] private float powerupAndObstacleSpawnRangeZ = 7.5f;

    [SerializeField] private float powerupSpawnDelayMin = 3f;
    [SerializeField] private float powerupSpawnDelayMax = 10f;

    [SerializeField] private float enemySpawnRangeX = 16;
    [SerializeField] private float enemySpawnRangeZ = 10;

    private int enemyCount = 0;

    [SerializeField] private int maxPowerupCount = 1;
    private int waveNumber = 0;

    private bool waitingForPowerUp = false;

    [SerializeField] private int minSoftObstacles = 0;
    [SerializeField] private int maxSoftObstacles = 6;
    public int MaxSoftObstacles
    {
        get
        {
            return maxSoftObstacles;
        }
    }

    [SerializeField] private int minHardObstacles = 0;
    [SerializeField] private int maxHardObstacles = 6;
    public int MaxHardObstacles
    {
        get
        {
            return MaxHardObstacles;
        }
    }

    private bool isStarted = false;
    // Start is called before the first frame update

    private void Awake()
    {
        foreach (GameObject gameObj in hardObstaclesPrefabs)
        {
            ObjectPooler.SharedInstance.PushObjectToPool(gameObj, maxHardObstacles);
        }
        foreach (GameObject gameObj in softObstaclesPrefabs)
        {
            ObjectPooler.SharedInstance.PushObjectToPool(gameObj, maxSoftObstacles);
        }
        foreach (GameObject gameObj in enemiesPrefabs)
        {
            ObjectPooler.SharedInstance.PushObjectToPool(gameObj,0);
        }
        foreach (GameObject gameObj in powerupsPrefabs)
        {
            ObjectPooler.SharedInstance.PushObjectToPool(gameObj, maxPowerupCount);
        }
    }

    void Start()
    {
        planeMeshRenderer = plane.GetComponent<MeshRenderer>();

        StartCoroutine(WaitForPooling());

    }
    public int MaxObjectCount(GameObject gameObject)
    {
        foreach (GameObject gameObj in hardObstaclesPrefabs)
        {
            if(gameObj== gameObject)
            {
                return maxHardObstacles;
            }
        }

        foreach (GameObject gameObj in softObstaclesPrefabs)
        {
            if (gameObj == gameObject)
            {
                return maxSoftObstacles;
            }
        }

        return 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        { 
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyCount == 0)
            {
                StartRound();
            }
            powerupCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
            if (powerupCount == 0 && !waitingForPowerUp)
            {
                waitingForPowerUp = true;
                float powerupTimeout = Random.Range(powerupSpawnDelayMin, powerupSpawnDelayMax);
                Invoke("PowerupGeneration", powerupTimeout);
            }
        }
    }
    IEnumerator WaitForPooling()
    {
        while (true)
        {
            if (ObjectPooler.SharedInstance.isPoolingFinished)
            {
                StartRound();
                yield break;
            }
            yield return null;
        }
    }
    private void StartRound()
    {
        isStarted = true;
        waveNumber++;
        SpawnEnemyWave(waveNumber);
        DestroyObstacles();
        GenerateObstacles();
        GenerateBackground();
    }



    private void GenerateBackground()
    {
        int indexBG = Random.Range(0, backgroundTextures.Count);
        planeMeshRenderer.material = backgroundTextures[indexBG];
    }
    private void PowerupGeneration()
    {
        int powerupIndex = Random.Range(0, powerupsPrefabs.Length);
        Instantiate(powerupsPrefabs[powerupIndex], GenerateSpawnPosition(powerupAndObstacleSpawnRangeX, powerupAndObstacleSpawnRangeZ, powerupsPrefabs[powerupIndex].transform.localScale.y / 2) , powerupsPrefabs[powerupIndex].transform.rotation);
        waitingForPowerUp = false;
    }
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (var i = 0; i < enemiesToSpawn; i++)
        {
            EnemiesGeneration();
        }
    }
    private void EnemiesGeneration()
    {
        int enemyIndex = Random.Range(0, enemiesPrefabs.Length);
        GameObject pooledEnemy = ObjectPooler.SharedInstance.GetPooledObject(enemiesPrefabs[enemyIndex]);
        if (pooledEnemy != null)
        {
            pooledEnemy.SetActive(true); // activate it
            pooledEnemy.GetComponent<PersonController>().DefaultHealth();
            pooledEnemy.transform.position = GenerateSpawnPosition(enemySpawnRangeX, enemySpawnRangeZ, enemiesPrefabs[enemyIndex].transform.localScale.y / 2);
        }

    }
    private void DestroyObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach( GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }
    }

    private void GenerateObstacles()
    {
        int randSoftObstacles = Random.Range(minSoftObstacles, MaxSoftObstacles);
        int randHardObstacles = Random.Range(minHardObstacles, MaxHardObstacles);
        for (var i = 0; i < randSoftObstacles; i++)
        {
            ObstaclesGeneration(softObstaclesPrefabs);
        }
        for (var i = 0; i < randHardObstacles; i++)
        {
            ObstaclesGeneration(hardObstaclesPrefabs);
        }

    }
    private void ObstaclesGeneration(GameObject[] obstaclesPrefabs)
    {
        int obstacleIndex = Random.Range(0, obstaclesPrefabs.Length);
        float randYRot = Random.Range(0, 360);

        GameObject pooledObstacle = ObjectPooler.SharedInstance.GetPooledObject(obstaclesPrefabs[obstacleIndex]);
        if (pooledObstacle != null)
        {

            pooledObstacle.SetActive(true); // activate it
            pooledObstacle.transform.Rotate(Vector3.up, randYRot);
            pooledObstacle.transform.position = GenerateSpawnPosition(powerupAndObstacleSpawnRangeX, powerupAndObstacleSpawnRangeZ, obstaclesPrefabs[obstacleIndex].transform.localScale.y / 2);
        }
    }


    private Vector3 GenerateSpawnPosition(float rangeX,float rangeZ, float deltaY)
    {
        float spawnPosX = Random.Range(-rangeX, rangeX);
        float spawnPosZ = Random.Range(-rangeZ, rangeZ);
        Vector3 spawnPos = new Vector3(spawnPosX, deltaY, spawnPosZ);
        return spawnPos;
    }
}
