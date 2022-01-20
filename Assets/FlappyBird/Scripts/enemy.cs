using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private GameObject bird;

    // Start is called before the first frame update
    void Start()
    {
        bird = GameObject.Find("bird");
    }

    // Update is called once per frame
    void Update()
    {
        DestroySelf();
    }

    void DestroySelf()
    {
        float disToBird = transform.position.x - bird.transform.position.x;
        if (disToBird < -8f)
        {
            Destroy(gameObject);
        }
    }
}
