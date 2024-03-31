using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{

    float timer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        timer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(0).GetComponent<SpriteRenderer>().color.r, transform.GetChild(0).GetComponent<SpriteRenderer>().color.g, transform.GetChild(0).GetComponent<SpriteRenderer>().color.b, Mathf.Lerp(transform.GetChild(0).GetComponent<SpriteRenderer>().color.a, 0, 0.002f));
        transform.GetComponent<Rigidbody2D>().gravityScale = Mathf.Lerp(transform.GetComponent<Rigidbody2D>().gravityScale, 10f, 0.002f);

        if(timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
