using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaController : MonoBehaviour
{
    private GameObject quests;
    private TextMeshProUGUI missionName;
    private TextMeshProUGUI missionAmount;

    [SerializeField] private List<GameObject> SpawnedEnemies;
    [SerializeField] private int _needToDefeat;
    [SerializeField] private int _defeatEnemyCount;
    [SerializeField] private int moneyFromEnemy;

    private EnemySpawner _spawner;
    private PlayerController _player;

    private void Start()
    {
        SpawnedEnemies = new List<GameObject>();
        _spawner = GetComponent<EnemySpawner>();
        _player = FindObjectOfType<PlayerController>();

        quests = GameObject.Find("Quest");
        missionName = quests.transform.Find("TaskName").GetComponent<TextMeshProUGUI>();
        missionAmount = quests.transform.Find("TaskAmount").GetComponent<TextMeshProUGUI>();

        quests.SetActive(true);
        missionAmount.text = _defeatEnemyCount.ToString() + " / " + _needToDefeat.ToString();
    }

    private void Update()
    {
        if (_needToDefeat != _defeatEnemyCount && SpawnedEnemies.Count == 0)
        {
            var enemyIndex = ChoseRandomEnemy();
            GameObject spawnedEnemy = _spawner.SpawnEnemy(enemyIndex);
            SpawnedEnemies.Add(spawnedEnemy);
            foreach (var enemyPrefab in SpawnedEnemies)
            {
                var enemyHealth = enemyPrefab.GetComponent<EnemyHealth>();
                enemyHealth.isDead += HandleEnemyDeath;
            }
        }
        else if (_needToDefeat == _defeatEnemyCount)
        {
            quests.SetActive(false);
            PlayerWin();
        }
        if (_player.IsDestroyed()) 
        {
            PlayerLose();
        }
    }

    private int ChoseRandomEnemy()
    {
        return Random.Range(0, _spawner.enemyPrefabs.Length);
    }

    private void HandleEnemyDeath(bool isDead)
    {
        if (isDead)
        {
            foreach (var enemyPrefab in SpawnedEnemies)
            {
                var enemyHealth = enemyPrefab.GetComponent<EnemyHealth>();
                enemyHealth.isDead -= HandleEnemyDeath;
            }
            SpawnedEnemies.Clear();
            _defeatEnemyCount++;
            enemyDefeated();
        }
    }

    private void enemyDefeated()
    {
        _player.getCoins(moneyFromEnemy);
        MissionDisplay();
    }

    private void MissionDisplay() 
    {
        missionAmount.text = _defeatEnemyCount.ToString() + " / " + _needToDefeat.ToString();
    }

    private void PlayerWin()
    {
        SceneManager.LoadScene(1);
    }

    private void PlayerLose()
    {
        SceneManager.LoadScene(0);
    }
}
