using UnityEngine;
using System.Collections;

public class BasicProjectile : MonoBehaviour {

    private ProjType type = ProjType.BASIC;
    private bool lethal = true;
    private int bounces = 6;
}
