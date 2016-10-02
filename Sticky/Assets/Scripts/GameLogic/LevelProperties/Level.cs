using UnityEngine;
using System.Collections;

//Scriptable level object has a number of waves associated with it
[CreateAssetMenu(fileName = "Level", menuName = "Level", order = 1)]
public class Level : ScriptableObject
{
    [SerializeField] public Wave[] waves = null;
}
