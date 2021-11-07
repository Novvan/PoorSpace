using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    Enemy[] _enemies;
    public Vector2 posMin;
    public Vector2 posMax;
    public int maxEnemies = 5;
    public float timeSpawn = 3;
    int _currentEnemies;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(WaitToSpawn(timeSpawn));
        }
    }

    public Enemy GetPokemonRandom(Enemy[] enemies)
    {
        return enemies[Random.Range(0, enemies.Length - 1)];
    }

    public Vector2 GetRandomPos(Vector2 min, Vector3 max)
    {
        return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }
    IEnumerator WaitToSpawn(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            if (_currentEnemies < maxEnemies)
            {
                Spawn();
            }
        }
    }
    public void Spawn()
    {
        Enemy enemy = GetPokemonRandom(_enemies);
        Vector2 pos = GetRandomPos(posMin, posMax);

        GameObject obj = PhotonNetwork.Instantiate(enemy.name, pos, Quaternion.identity);
        Enemy newEnemy = obj.GetComponent<Enemy>();
        newEnemy.OnDestroyPoke += OnEnemyDestroy;
        _currentEnemies++;
    }
    void OnEnemyDestroy(Enemy poke)
    {
        _currentEnemies--;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(posMin, posMax);
    }
}
