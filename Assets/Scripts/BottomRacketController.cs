using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomRacketController : MonoBehaviour {

    public float speed = 8f;
    Rigidbody2D rb;
    Vector2 velocity;
    public float margin = -1.76f;
    float sliderValue;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2();
    }
	
	// Update is called once per frame
	void Update() {
        // Управление стрелками
        if (Input.GetKey(KeyCode.LeftArrow))
            velocity.x = -speed;
        else if (Input.GetKey(KeyCode.RightArrow))
            velocity.x = speed;
        else
            velocity.x = 0;
        rb.velocity = velocity;
    }

    void OnMouseDrag() {
        float distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
        float sizeOfTable = GameObject.Find("Table").GetComponent<SpriteRenderer>().size.x;
        float sizeOfRacket = GameObject.Find("BottomRacket").GetComponent<SpriteRenderer>().size.x;
        // Новое значение положения ракетки (значение положения курсора мышки) не должно быть больше чем свободное место
        float HalfFreeSpace = sizeOfTable / 2 - sizeOfRacket / 2;
        if (newPosition.x < HalfFreeSpace && newPosition.x > -HalfFreeSpace)
            transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
    }

    public void SetRacketToCenter() {
        transform.position = new Vector2(0, margin);
    }

    public void OnSliderDrag(UnityEngine.UI.Slider slider) {
        transform.position = new Vector3(slider.value, transform.position.y, transform.position.z);
    }
}