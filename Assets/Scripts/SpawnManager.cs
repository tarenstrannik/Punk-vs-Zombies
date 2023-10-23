using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int powerupCount = 0;
    private int waveNumber = 0;

    private bool waitingForPowerUp = false;

    [SerializeField] private int minSoftObstacles = 0;
    [SerializeField] private int maxSoftObstacles = 6;

    [SerializeField] private int minHardObstacles = 0;
    [SerializeField] private int maxHardObstacles = 6;

    // Start is called before the first frame update
    void Start()
    {
        planeMeshRenderer = plane.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            DestroyObstacles();
            GenerateObstacles();
            GenerateBackground();

        }
        powerupCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
        if(powerupCount==0 && !waitingForPowerUp)
        {
            waitingForPowerUp = true;
            float powerupTimeout = Random.Range(powerupSpawnDelayMin, powerupSpawnDelayMax);
            Invoke("PowerupGeneration", powerupTimeout);
        }

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
        Instantiate(enemiesPrefabs[enemyIndex], GenerateSpawnPosition(enemySpawnRangeX, enemySpawnRangeZ, enemiesPrefabs[enemyIndex].transform.localScale.y / 2) , enemiesPrefabs[enemyIndex].transform.rotation);
    }
    private void DestroyObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach( GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    private void GenerateObstacles()
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
        GameObject obstTemp = Instantiate(obstaclesPrefabs[obstacleIndex], GenerateSpawnPosition(powerupAndObstacleSpawnRangeX, powerupAndObstacleSpawnRangeZ, obstaclesPrefabs[obstacleIndex].transform.localScale.y / 2) , obstaclesPrefabs[obstacleIndex].transform.rotation);
        obstTemp.transform.Rotate(Vector3.up, randYRot);

    }


    private Vector3 GenerateSpawnPosition(float rangeX,float rangeZ, float deltaY)
    {
        float spawnPosX = Random.Range(-rangeX, rangeX);
        float spawnPosZ = Random.Range(-rangeZ, rangeZ);
        Vector3 spawnPos = new Vector3(spawnPosX, deltaY, spawnPosZ);
        return spawnPos;
    }
}
