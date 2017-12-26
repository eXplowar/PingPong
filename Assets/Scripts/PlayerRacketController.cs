using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRacketController : NetworkBehaviour {

    [SerializeField] float speed = 8f;
    Rigidbody2D rb;
    Vector2 velocity;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2();
        if (isLocalPlayer) 
            transform.name = "Player1";
        else
            transform.name = "Player2";
    }
	
	// Update is called once per frame
	void Update() {
        if (isLocalPlayer) {
            // Управление ракеткой
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                velocity.x = -speed;
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                velocity.x = speed;
            else
                velocity.x = 0;
            rb.velocity = velocity;
        }
    }

    void OnMouseDrag() {
        if (isLocalPlayer) {
            float distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
            float sizeOfTable = GameObject.Find("Table").GetComponent<SpriteRenderer>().size.x;
            float sizeOfRacket = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().size.x;//
            // Новое значение положения ракетки (значение положения курсора мышки) не должно быть больше чем свободное место
            float HalfFreeSpace = sizeOfTable / 2 - sizeOfRacket / 2;
            if (newPosition.x < HalfFreeSpace && newPosition.x > -HalfFreeSpace)
                transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
        }
    }

    public override void OnStartLocalPlayer() {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void SetRacketToCenter() {
        transform.position = new Vector2(0, transform.position.y);
    }

    public void OnSliderDrag(UnityEngine.UI.Slider slider) {
        if (isClient && GameObject.Find("Player2") != null) {
            if (transform.name == "Player2") {
                GameObject.Find("Player2").transform.position = new Vector3(slider.value, GameObject.Find("Player2").transform.position.y, transform.position.z);
            }
        } else {
            GameObject.Find("Player1").transform.position = new Vector3(slider.value, GameObject.Find("Player1").transform.position.y, transform.position.z);

            if (GameObject.FindWithTag("Bot") != null) {
                GameObject.FindWithTag("Bot").transform.position = new Vector3(slider.value, GameObject.FindWithTag("Bot").transform.position.y, transform.position.z);
            }
        }
    }
}