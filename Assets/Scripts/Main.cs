using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Main : NetworkBehaviour {

    public GameObject ballPrefab;
    [SerializeField] GameObject ballRespawnPosition;
    [SerializeField] GameObject topRacket;

    // Use this for initialization
    void Start() {
        if (isServer) {
            GameObject ball = Instantiate(ballPrefab, ballRespawnPosition.transform.position, Quaternion.identity);
            NetworkServer.Spawn(ball);
            GameObject.Find("SpawnRacket").transform.position = new Vector3(0, 1.76f, 0);
        }
    }

    // Update is called once per frame
    void Update() {
        // Рестарт
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.R)) {
            CmdRespawnBall();
        }
    }

    [Command] // Метод может запустить только сервер
    public void CmdRespawnBall() {
        GameObject.FindWithTag("Player").GetComponent<PlayerRacketController>().SetRacketToCenter();
        GameObject.Find("TopRacket").GetComponent<TopRacketController>().SetRacketToCenter(); //

        if (GameObject.FindGameObjectWithTag("Ball")) {
            NetworkServer.Destroy(GameObject.FindGameObjectWithTag("Ball"));
        }
        GameObject ball = Instantiate(ballPrefab, ballRespawnPosition.transform.position, Quaternion.identity);
        NetworkServer.Spawn(ball);
    }
}