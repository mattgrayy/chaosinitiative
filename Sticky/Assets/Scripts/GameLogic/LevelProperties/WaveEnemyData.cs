using UnityEngine;

[System.Serializable]
public struct WaveEnemyData
{
    public int enemyID; //Enemy ID to spawn
    [Range(0, 20)] public int locationID; //Spawn location along the top of the screen 0 being far left and 20 being far right
}
