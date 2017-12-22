using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    float randomDirection;
    int randomColor;
    float randomSize;
    public float minSizeScale = 0.26f;
    public float maxSizeScale = 0.78f;
    int randomSpeed;
    Rigidbody2D rb;
    List<Color> colorList;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        Invoke("PushBall", 2);
    }
	
	// Update is called once per frame
	void Update() {
		
	}

    public void PushBall() {
        randomDirection = Random.Range(1, 10);
        randomSpeed = Random.Range(1, 4);

        if (randomDirection % 2 == 0)
            rb.AddForce(new Vector2(Random.Range(-2, 2), randomSpeed * 10));
        else 
            rb.AddForce(new Vector2(Random.Range(-2, 2), randomSpeed * -10));
    }

    public void ChangeColor() {
        colorList = new List<Color> { Color.red, Color.white, Color.yellow, Color.black, Color.green, Color.cyan };
        colorList.Remove(GetComponent<SpriteRenderer>().color);
        randomColor = Random.Range(0, 4);
        GetComponent<SpriteRenderer>().color = colorList[randomColor];
    }

    public void ChangeSize() {
        randomSize = Random.Range(minSizeScale, maxSizeScale);
        transform.localScale = new Vector3(randomSize, randomSize, 1f);
    }

    public void SetBallToCenter() {
        rb.velocity = Vector3.zero;
        transform.position = new Vector2(0, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Изменение угла отскока от ракетки
        if (collision.transform.name == "BottomRacket") {
            float xVelocity = rb.velocity.x;
            float distanceFromRacketCenter = transform.position.x - collision.transform.position.x;
            float newXVelocity = xVelocity * 10 * distanceFromRacketCenter;
            rb.velocity = new Vector2(newXVelocity, rb.velocity.y);
        }

        if (collision.transform.name == "TopRacket") {
            float xVelocity = rb.velocity.x;
            float distanceFromRacketCenter = transform.position.x - collision.transform.position.x;
            float newXVelocity = xVelocity * 10 * distanceFromRacketCenter;
            rb.velocity = new Vector2(newXVelocity, rb.velocity.y);
        }
    }
}
