using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
        }
    }
}
