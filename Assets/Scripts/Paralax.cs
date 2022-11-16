using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public float speed;
    public float spriteWidth;
    // Start is called before the first frame update
    void Start()
    {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        if(transform.position.x < -spriteWidth) {
            transform.Translate(spriteWidth*2,0,0);
        }
    }
}
