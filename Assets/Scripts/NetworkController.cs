using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkController : NetworkManager {

    [SerializeField] GameObject topRacket;

    public override void OnServerConnect(NetworkConnection conn) {
        Debug.Log("Player Connected");
        DestroyObject(GameObject.FindGameObjectWithTag("Bot"));
    }

    public override void OnServerDisconnect(NetworkConnection conn) {
        Debug.Log("Player Disonnected");
        
        DestroyObject(GameObject.Find("Player2"));
        NetworkServer.DestroyPlayersForConnection(conn);

        GameObject racket = Instantiate(topRacket, new Vector3(0, 1.76f, 0), Quaternion.identity);
        NetworkServer.Spawn(racket);
    }
}
