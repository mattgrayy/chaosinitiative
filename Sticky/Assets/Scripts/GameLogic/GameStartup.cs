using UnityEngine;
using System.Collections;

public class GameStartup : MonoBehaviour
{
    public static GameStartup instance { get; private set; }

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
