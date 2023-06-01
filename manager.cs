using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject spider;
    Vector3 spawnPoint;

    int finishEnemyCount = 0;
    int finishCanEnemyCount = 5;

    public TMPro.TextMeshProUGUI finishEnemyCountText;

    void Start()
    {
        InvokeRepeating("Spawn", 0, 5);
    }

    public void EnemyFinishWin()
    {
        finishEnemyCount++;
        finishEnemyCountText.text = finishEnemyCount + " / " + finishCanEnemyCount;

        if (finishEnemyCount == finishCanEnemyCount)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void Spawn()
    {
        int randomNumber = Random.Range(1, 4);

        spawnPoint = GameObject.Find(randomNumber.ToString()).transform.position;

        GameObject newSpider = Instantiate(spider, spawnPoint, Quaternion.identity);
    }
}
