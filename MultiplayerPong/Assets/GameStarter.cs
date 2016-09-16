using System;
using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour {

    //NetworkManager nm;

    Animator anim;

    [SerializeField]
    Text ipaddressInput;
    
    [SerializeField]
    Text portInput;

    bool connected;

    void Start() {
        //nm = GetComponent<NetworkManager>();
        anim = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    public void OnHost() {
        if (CheckIfInputIsEmpty()) {
            return;
        }

        int portNumber;

        if (!CheckInputIsValid(out portNumber)) {
            return;
        }

        /*ConnectionConfig config = new ConnectionConfig();

        
        nm.networkAddress = ipaddressInput.text;
        nm.networkPort = portNumber;

        nm.StartHost(config, 1);*/

        Network.InitializeServer(1, portNumber, false);

        SceneManager.LoadScene("game");
    }

    [RPC]
    void Connected() {

    }

    public void OnJoin() {
        if (CheckIfInputIsEmpty()) {
            return;
        }

        int portNumber = -1;

        if (!CheckInputIsValid(out portNumber)) {
            return;
        }

        /*nm.networkAddress = ipaddressInput.text;
        nm.networkPort = portNumber;

        nm.StartClient();*/

        Network.Connect(ipaddressInput.text, portNumber);
    }

    IEnumerator ConnectionTest() {
        int tries = 5;

        while (tries > 0) {
            yield return new WaitForSeconds(1);
            if (connected)
                break;

            tries--;
        }

        if (connected) {

            SceneManager.LoadScene("game");

            Scene game = SceneManager.GetActiveScene();

            GameObject[] objects = game.GetRootGameObjects();
        } else {
            // failed
        }
    }

    bool CheckIfInputIsEmpty() {
        if (ipaddressInput.text == "" && portInput.text == "") {
            anim.SetTrigger("bothWrong");
            return true;
        }else if (ipaddressInput.text == "") {
            anim.SetTrigger("wrongIP");
            return true;
        }else if (portInput.text == "") {
            anim.SetTrigger("wrongPort");
            return true;
        }

        return false;
    }

    bool CheckInputIsValid(out int port) {
        IPAddress address;
        port = -1;

        if (ipaddressInput.text == "" && portInput.text == "") {
            anim.SetTrigger("bothWrong");
            return true;
        } else if (ipaddressInput.text == "") {
            anim.SetTrigger("wrongIP");
            return true;
        } else if (portInput.text == "") {
            anim.SetTrigger("wrongPort");
            return true;
        }

        if (!IPAddress.TryParse(ipaddressInput.text, out address)) {
            if (ipaddressInput.text != "localhost") {
                anim.SetTrigger("wrongIP");
                return false;
            }
        }

        int.TryParse(portInput.text, out port);

        if (port <= 0 || port >= 65536 || port == -1) {
            anim.SetTrigger("wrongPort");
            port = -1;
            return false;
        }

        return true;
    }
}
