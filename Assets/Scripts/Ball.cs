using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball : NetworkBehaviour {
    
    float randomDirection;
    int randomColor;
    float randomSize;
    int randomSpeed;
    [SerializeField] float minSizeScale = 0.26f;
    [SerializeField] float maxSizeScale = 0.78f;
    Rigidbody2D rb;
    List<Color> colorList;

    [SyncVar]
    Color syncColor;
    [SyncVar]
    Vector3 syncSize;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = syncColor;
        transform.localScale = syncSize;
        InitColor();
        InitSize();
        Invoke("PushBall", 2);
    }

    void PushBall() {
        randomDirection = Random.Range(1, 10);
        randomSpeed = Random.Range(1, 4);

        if (randomDirection % 2 == 0)
            rb.AddForce(new Vector2(Random.Range(-2, 2), randomSpeed * 10));
        else 
            rb.AddForce(new Vector2(Random.Range(-2, 2), randomSpeed * -10));
    }

    // Генерация нового цвета и установка на стороне сервера
    void InitColor() {
        colorList = new List<Color> { Color.red, Color.white, Color.yellow, Color.black, Color.green, Color.blue };
        colorList.Remove(GetComponent<SpriteRenderer>().color);
        randomColor = Random.Range(0, 5);
        syncColor = colorList[randomColor];
        CmdChangeColorOnServer(syncColor); // Установка нового значения на сервере
    }

    void InitSize() {
        randomSize = Random.Range(minSizeScale, maxSizeScale);
        syncSize = new Vector3(randomSize, randomSize, 1f);
        CmdChangeSizeOnServer(syncSize);
    }

    [Command] // Установка нового значения на сервере
    void CmdChangeColorOnServer(Color clr) {
        GetComponent<SpriteRenderer>().color = clr;
    }

    [Command]
    void CmdChangeSizeOnServer(Vector3 size) {
        transform.localScale = size;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Изменение угла отскока от ракетки
        if (collision.transform.tag == "Player") {
            float distanceFromRacketCenter = transform.position.x - collision.transform.position.x;
            float newXVelocity = distanceFromRacketCenter / collision.collider.bounds.size.x;
            rb.velocity = new Vector2(newXVelocity, rb.velocity.y);
        }
    }
}
