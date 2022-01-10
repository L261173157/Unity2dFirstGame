using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float moveRate;
    public Transform Camera;
    private float startPoint;

    // Start is called before the first frame update
    private void Start()
    {
        startPoint = transform.position.x;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(startPoint + Camera.position.x * moveRate, transform.position.y, 0);
    }
}