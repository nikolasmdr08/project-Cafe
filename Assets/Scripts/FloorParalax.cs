using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorParalax : MonoBehaviour
{
    [SerializeField] private float limit, rePos, speed;
    private void Update()
    {
        FloorMovement();
    }

    private void FloorMovement()
    {
        gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (gameObject.transform.position.x<limit)
        {
            gameObject.transform.position = new Vector2(rePos, transform.position.y);
        }
    }
}
