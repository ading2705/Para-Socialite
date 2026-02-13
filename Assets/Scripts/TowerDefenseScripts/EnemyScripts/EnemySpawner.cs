
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 10f;
    [SerializeField] private GameObject countdownBox;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private GameObject waveOverlay;
    [SerializeField] private TMP_Text waveText;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; //enemies per second
    private bool isSpawning = false;
    public GameObject deactivateSpawner1;
    public GameObject deactivateSpawner2;
    public GameObject deactivateSpawner3;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }



    private void Start()
    {
        StartCoroutine(StartWave());
        waveOverlay.SetActive(false);
    }


    private void Update()
    {
        if (currentWave >= 3)
        {
            SceneManager.LoadScene("Win");
        } 
        if(!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            deactivateSpawner1.SetActive(false);
            deactivateSpawner2.SetActive(false);
            deactivateSpawner3.SetActive(false);
            SpawnEnemy();
            Debug.Log("Spawn Enemy");
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
            deactivateSpawner1.SetActive(true);
            deactivateSpawner2.SetActive(true);
            deactivateSpawner3.SetActive(true);
        }

    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    private IEnumerator StartWave()
    {
        // yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(Countdown(timeBetweenWaves));
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
        StartCoroutine(DisplayInfo("Incoming Enemies: " + enemiesLeftToSpawn));
    }
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }


    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    IEnumerator DisplayInfo(string info)
    {
        waveText.text = info;
        for (int i = 0; i < 5; i++)
        {
            waveOverlay.SetActive(true);
            yield return new WaitForSeconds((float)i * 0.3f);
        }
        waveOverlay.SetActive(false);
    }

    IEnumerator Countdown(float time)
    {
        countdownBox.SetActive(true);
        for (int i = (int)time; i > 0; i--)
        {
            countdownText.text = "" + i;
            yield return new WaitForSeconds(1);
        }
        countdownBox.SetActive(false);
    }

}
