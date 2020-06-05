using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBringers : MonoBehaviour
{
    private float speed = 8f;
    private float deathPosX = -8f;

    void Start()
    {
        StartCoroutine(Bounce());
    }

    IEnumerator Bounce()
    {
        float t = 0.6f;
        while(transform.position.x > deathPosX)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (t > 0.4f)
                transform.Translate(Vector2.up * 1f * Time.deltaTime);
            else if(t > 0.2f)
                transform.Translate(Vector2.down * 1f * Time.deltaTime);
            else if(t <= 0f)
                t += 0.6f;
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
