using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    
    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.R)) {
            RestartGame();
        }
    }

    public void RestartGame() {
        GameObject.Find("BottomRacket").GetComponent<BottomRacketController>().SetRacketToCenter();
        GameObject.Find("TopRacket").GetComponent<TopRacketController>().SetRacketToCenter();
        GameObject.Find("Ball").transform.position = new Vector3(0, 0, 0);
        GameObject.Find("Ball").GetComponent<Ball>().ChangeColor();
        GameObject.Find("Ball").GetComponent<Ball>().ChangeSize();
        GameObject.Find("Ball").GetComponent<Ball>().SetBallToCenter();
        GameObject.Find("Ball").GetComponent<Ball>().Invoke("PushBall", 2);
    }
}