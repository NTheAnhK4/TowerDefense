
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public MeleeEnemyData Data;
    public LevelData LevelData;
    public int currentWay = -1;

    private void OnEnable()
    {
        ObserverManager.Attach(EventId.SpawnNextWay, _ => SpawnWay());
    }

    private void OnDisable()
    {
        ObserverManager.Detach(EventId.SpawnNextWay, _ => SpawnWay());
    }


    private void SpawnWay()
    {
        var ways = LevelData.Levels[GameManager.Instance.Level].Ways;
        currentWay++;
        if (currentWay == ways.Count) return;

        ObserverManager.Notify(EventId.SpawnWay, currentWay + 1);

        StartCoroutine(SpawnAllMiniWays(ways[currentWay].MiniWays));
    }

    private IEnumerator SpawnAllMiniWays(List<MiniWayParam> miniWays)
    {
        List<Coroutine> runningCoroutines = new List<Coroutine>();

        //Run all MiniWays in parallel
        foreach (var miniWay in miniWays)
        {
            runningCoroutines.Add(StartCoroutine(SpawnMiniWay(miniWay)));
        }

        // Wait for all MiniWays to complete
        foreach (var coroutine in runningCoroutines)
        {
            yield return coroutine;
        }

        // When all MiniWays have spawned, send an event
        if (currentWay + 1 < LevelData.Levels[GameManager.Instance.Level].Ways.Count)
            ObserverManager.Notify(EventId.SpawnedEnemies, currentWay);
    }

    private IEnumerator SpawnMiniWay(MiniWayParam miniWayParam)
    {
        foreach (var enemyInfor in miniWayParam.EnemyInfors)
        {
            SpawnEnemy(miniWayParam.PathId, enemyInfor);
            yield return new WaitForSeconds(2);
        }
    }


    private void SpawnEnemy(int pathId, EnemyInfor enemyInfor)
    {
        if (enemyInfor.EnemyType != EnemyType.MeleeAttack) return;
        if (enemyInfor.EnemyId < 0 || enemyInfor.EnemyId >= Data.MeleeEnemys.Count) return;

        var enemyPrefab = Data.MeleeEnemys[enemyInfor.EnemyId].EnemyPrefab;
        if (enemyPrefab == null) return;

        var spawnPosition = LevelData.Levels[GameManager.Instance.Level].Paths[pathId].Positions[0];
        PoolingManager.Spawn(enemyPrefab, spawnPosition, default, transform);
    }
}