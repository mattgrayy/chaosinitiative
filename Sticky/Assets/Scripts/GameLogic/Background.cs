using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{
    [SerializeField] private ScrollingBackground[] backgrounds = null;

    private void Update()
    {
        foreach(ScrollingBackground background in backgrounds)
        {
            background.trans.position += (Vector3.down * background.movementSpeed * Time.deltaTime);
        }
    }
}
