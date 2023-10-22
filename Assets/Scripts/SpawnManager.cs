using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] hardObstaclesPrefabs;
    public GameObject[] softObstaclesPrefabs;
    public GameObject[] enemiesPrefabs;
    public GameObject[] powerupsPrefabs;

    public List<Material> backgroundTextures;
    public GameObject plane;
    private MeshRenderer planeMeshRenderer;
    private float powerupAndObstacleSpawnRangeX = 14f;
    private float powerupAndObstacleSpawnRangeZ = 7.5f;

    public float powerupSpawnDelayMin = 3f;
    public float powerupSpawnDelayMax = 10f;
        
    private float enemySpawnRange = 16;

    private int enemyCount = 0;
    private int powerupCount = 0;
    private int waveNumber = 0;

    private bool waitingForPowerUp = false;

    private int minSoftObstacles = 0;
    private int maxSoftObstacles = 6;

    private int minHardObstacles = 0;
    private int maxHardObstacles = 6;

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
        Instantiate(powerupsPrefabs[powerupIndex], GeneratePAndOSpawnPosition() + new Vector3(0, powerupsPrefabs[powerupIndex].transform.localScale.y / 2, 0), powerupsPrefabs[powerupIndex].transform.rotation);
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
        Instantiate(enemiesPrefabs[enemyIndex], GenerateEnemySpawnPosition()+new Vector3(0, enemiesPrefabs[enemyIndex].transform.localScale.y / 2, 0), enemiesPrefabs[enemyIndex].transform.rotation);
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
        GameObject obstTemp = Instantiate(obstaclesPrefabs[obstacleIndex], GeneratePAndOSpawnPosition()+new Vector3(0, obstaclesPrefabs[obstacleIndex].transform.localScale.y/2,0), obstaclesPrefabs[obstacleIndex].transform.rotation);
        obstTemp.transform.Rotate(Vector3.up, randYRot);

    }

    private Vector3 GeneratePAndOSpawnPosition()
    {
        float spawnPosX = Random.Range(-powerupAndObstacleSpawnRangeX, powerupAndObstacleSpawnRangeX);
        float spawnPosZ = Random.Range(-powerupAndObstacleSpawnRangeZ, powerupAndObstacleSpawnRangeZ);
        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPos;
    }
    private Vector3 GenerateEnemySpawnPosition()
    {
        float spawnPosX = Random.Range(-enemySpawnRange, enemySpawnRange);
        float spawnPosZ = Random.Range(-enemySpawnRange, enemySpawnRange);
        Vector3 spawnPos = new Vector3(spawnPosX, 0.5f, spawnPosZ);
        return spawnPos;
    }
}
