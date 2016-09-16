using UnityEngine;

public class HostManagment : MonoBehaviour {

    NetworkView netView;

    [SerializeField]
    Transform ball;

    [SerializeField]
    PadMovement player;

    [SerializeField]
    PadMovement opponent;

    void Start() {
        netView = GetComponent<NetworkView>();
    }

    void UpdateRemoteVars() {
        netView.RPC("UpdatePad", RPCMode.All, player.Position, player.PadVel);
        netView.RPC("UpdateBall", RPCMode.All, ball.position, Vector2.zero); 
    }

    [RPC]
    void ClientConnected(string msg) {
        print(msg);
    }

    void UpdateClientPad(float pos, float vel) {
        // It's not ideal to trust clients with this much power.
        // But for the proof of concept lets keep it simple.
        opponent.PadVel = vel;
        opponent.Position = pos;
    }
}
