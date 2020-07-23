using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunnerTT
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public float MovementSpeed;
        //[HideInInspector] public Vector3 MoveDirection;
        public int StartLaneIndex;
        public CoinView CoinPrefab;
        public ObstacleView ObstaclePrefab;
        public Vector3[] LanesPositions;
        public float SpawnDistance;
        [Range(.4f, 3)] public float ObstacleMinSpawnTime;
        [Range(3, 6)] public float ObstacleMaxSpawnTime;
        public float RandomTimeForSpawn => UnityEngine.Random.Range(ObstacleMinSpawnTime, ObstacleMaxSpawnTime);
        [Range(0, 100)] public int CoinGenerationChance;
        public float CoinSpawnCooldown;
        public float MaxSpeedUpCoins;
        public float PoolingBorderZCoordinate;
    }
}