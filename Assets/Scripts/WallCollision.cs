using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.name == "Ball") {
            GameObject.Find("Camera").GetComponent<Main>().RestartGame();
        }
    }
}
