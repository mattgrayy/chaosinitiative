using UnityEngine;

//Wave struct has data on enemies to spawn and time till the next wave should begin
[System.Serializable]
public struct Wave
{
    public WaveEnemyData[] enemyData;
    [Range(1.0f, 5.0f)] public float nextWaveTime;
}