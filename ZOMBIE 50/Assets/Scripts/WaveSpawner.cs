using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public GameObject zombie;
    public Transform[] spawners;
    public TMP_Text levelNo;
    public TMP_Text status;
    List <GameObject> activeZombies = new List <GameObject> {};
    private int waveNumber;
    private int spawned;
    private int numWaveZombies;
    bool spawningNewWave = false;

    private void Start()
    {
        spawned = 0;
        numWaveZombies = 50;
        StartCoroutine(SpawnWave());
    }

    private void Update() 
    {
        if (activeZombies.Count == 0 && !spawningNewWave)
        {
            StartCoroutine(NextWave());
        }

        activeZombies.RemoveAll(z => z == null);
    }

    IEnumerator SpawnWave()
    {
        spawned = 0;
        levelNo.text = (waveNumber+1).ToString();
        status.text = "Spawning Wave";
        while (spawned < numWaveZombies)
        {
            GameObject z = Instantiate(zombie, spawners[Random.Range(0, spawners.Length)]);
            activeZombies.Add(z);

            spawned++;
            yield return new WaitForSeconds(2f);
        }

        status.text = "Waiting To Finish Wave";
        spawningNewWave = false;
    }

    IEnumerator NextWave()
    {
        spawningNewWave = true;
        Debug.Log("Wave Complete");
        waveNumber += 1;
        numWaveZombies += 10*waveNumber;

        status.text = "Next Wave In";
        yield return new WaitForSeconds(5f);

        float timer = 15;
        while (timer > 0)
        {
            status.text = Mathf.FloorToInt(timer).ToString();
            timer -= Time.deltaTime;
            yield return null; 
        }

        StartCoroutine(SpawnWave());
    }
}
