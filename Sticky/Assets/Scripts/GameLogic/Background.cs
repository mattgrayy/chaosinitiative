using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{
    [SerializeField] private ScrollingBackground[] backgrounds = null;
    private Vector3 screenHeight;

    private void Start()
    {
        screenHeight = new Vector3(0.0f,Camera.main.ViewportToWorldPoint(Vector3.one).y,0.0f);
    }

    private void Update()
    {
        foreach(ScrollingBackground background in backgrounds)
        {
            background.trans.position += (Vector3.down * background.movementSpeed * Time.deltaTime);
            if(background.trans.position.y < -12.0f)
            {
                background.trans.position = new Vector3(background.trans.position.x, background.trans.position.y + 12.0f, background.trans.position.z);
            }
        }
    }
}
