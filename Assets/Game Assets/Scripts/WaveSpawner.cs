using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5.5f;
    private float countdown = 2f;
    
    private int waveNumber = 0;
    private ObjectPooler op;

    private void Start()
    {
        op = ObjectPooler.Instance;
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
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemy()
    {
        op.SpawnFromPool("Enemy", spawnPoint.position, spawnPoint.rotation);
    }
}
