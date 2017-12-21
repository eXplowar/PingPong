using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    int randomDirection;
    int randomColor;
    int randomSize;
    int randomSpeed;
    Rigidbody2D rd;
    List<Color> colorList;

    // Use this for initialization
    void Start() {
        Invoke("PushBall", 2);
    }
	
	// Update is called once per frame
	void Update() {
		
	}

    public void PushBall() {
        rd = GetComponent<Rigidbody2D>();

        randomDirection = Random.Range(0, 1);
        randomSpeed = Random.Range(1, 4);

        if (randomDirection == 0)
            rd.AddForce(new Vector2(Random.Range(-2, 2), randomSpeed * -10));
        else if (randomDirection == 1)
            rd.AddForce(new Vector2(Random.Range(-2, 2), randomSpeed * 10));
    }

    public void ChangeColor() {
        colorList = new List<Color> { Color.red, Color.white, Color.yellow, Color.black, Color.green, Color.cyan };
        colorList.Remove(GetComponent<SpriteRenderer>().color);
        randomColor = Random.Range(0, 4);
        GetComponent<SpriteRenderer>().color = colorList[randomColor];
    }

    public void ChangeSize() {
        randomSize = Random.Range(1, 3);
        transform.localScale = new Vector3(randomSize, randomSize, randomSize);
    }

    public void SetBallToCenter() {
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = Vector3.zero;
        transform.position = new Vector2(0, 1);
    }
}
