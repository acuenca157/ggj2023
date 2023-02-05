using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private GameObject[] enemies;


    public void Spawn (float count)
    {
        for (int i = 0; i < count; i++) {
            var pointN = Random.Range(0, points.Length);
            var enemyN = Random.Range(0, enemies.Length);
            Instantiate(enemies[enemyN], points[pointN].position, points[pointN].rotation);
        }
    }

}
