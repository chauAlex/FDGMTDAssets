using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Game_Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5.5f;
    private float countdown = 2f;
    private int currLevel;
    
    private int waveNumber = 0;
    public float waveFactor = 1;
    public int waveOffset = 0;
    private ObjectPooler op;
    public TextAsset jsonFile;
    public EnemyDataHolder edh;

    private void Start()
    {
        op = ObjectPooler.Instance;
        currLevel = FindObjectOfType<GameManager>().nextLevelIndex - 1;

        edh = JsonUtility.FromJson<EnemyDataHolder>(jsonFile.text);
    }

    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        if(!AudioManager.instance.paused)
            countdown -= Time.deltaTime;

        //waveCountdownText.text = Mathf.Round(countdown).ToString(CultureInfo.InvariantCulture);
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        int numPerWave = (int)(waveFactor * waveNumber) + waveOffset;
        for (int i = 0; i < numPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemy()
    {
        //currently a 20/80 split of enemies, to be changed soon
        Random gen = new Random();
        double num = gen.NextDouble();
        for (int i = edh.enemylist.Length-1; i >= 0; i--)
        {
            if (num >= edh.enemylist[i].prob)
            {
                op.SpawnFromPool(edh.enemylist[i].name, spawnPoint.position, spawnPoint.rotation);
                break;
            }
        }
    }
}
