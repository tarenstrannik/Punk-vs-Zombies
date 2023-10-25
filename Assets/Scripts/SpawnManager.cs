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

    
    [SerializeField] private int maxPowerupCount = 1;


    

    [SerializeField] private int minSoftObstacles = 0;
    [SerializeField] private int maxSoftObstacles = 6;


    [SerializeField] private int minHardObstacles = 0;
    [SerializeField] private int maxHardObstacles = 6;


   
    // Start is called before the first frame update

    public static SpawnManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        planeMeshRenderer = plane.GetComponent<MeshRenderer>();
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
            ObjectPooler.SharedInstance.PushObjectToPool(gameObj, 0);
        }
        foreach (GameObject gameObj in powerupsPrefabs)
        {
            ObjectPooler.SharedInstance.PushObjectToPool(gameObj, maxPowerupCount);
        }
        MainManager.Instance.isGameActive =true;
        GameManager.Instance.SendMessage("StartRound");
    }

    // Update is called once per frame




    public void GenerateBackground()
    {
        int indexBG = Random.Range(0, backgroundTextures.Count);
        planeMeshRenderer.material = backgroundTextures[indexBG];
    }

    public void InitPowerupGeneration()
    {
        float powerupTimeout = Random.Range(powerupSpawnDelayMin, powerupSpawnDelayMax);
        Invoke("PowerupGeneration", powerupTimeout);
    }
    private void PowerupGeneration()
    {
        int powerupIndex = Random.Range(0, powerupsPrefabs.Length);

        GameObject pooledPowerup = ObjectPooler.SharedInstance.GetPooledObject(powerupsPrefabs[powerupIndex]);
        if (pooledPowerup != null)
        {
            pooledPowerup.SetActive(true); // activate it

            pooledPowerup.transform.position = GenerateSpawnPosition(powerupAndObstacleSpawnRangeX, powerupAndObstacleSpawnRangeZ, powerupsPrefabs[powerupIndex].transform.localScale.y / 2);
        }
        GameManager.Instance.SetPowerupWaiting(false);
        
    }
    public void SpawnEnemyWave(int enemiesToSpawn)
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
            pooledEnemy.GetComponent<PersonController>().Revive();
            pooledEnemy.transform.position = GenerateSpawnPosition(enemySpawnRangeX, enemySpawnRangeZ, enemiesPrefabs[enemyIndex].transform.localScale.y / 2);
        }

    }
    public void DestroyObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach( GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }
    }

    public void GenerateObstacles()
    {
        int randSoftObstacles = Random.Range(minSoftObstacles, maxSoftObstacles);
        int randHardObstacles = Random.Range(minHardObstacles, maxHardObstacles);
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
