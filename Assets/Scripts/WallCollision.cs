using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WallCollision : NetworkBehaviour {

    [SerializeField] GameObject ballObject;

    private void OnCollisionEnter2D(Collision2D collision) {
        var ball = Instantiate(ballObject, GameObject.Find("SpawnBall").GetComponent<Transform>().transform.position, Quaternion.identity);
        Destroy(collision.gameObject);
        NetworkServer.Spawn(ball);
    }
}
