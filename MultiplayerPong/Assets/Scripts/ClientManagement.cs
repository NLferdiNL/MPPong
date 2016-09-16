using UnityEngine;

public class ClientManagement : MonoBehaviour {

    NetworkView netView;

    // Might become it's own class.
    Transform ball;

    [SerializeField]
    PadMovement player;
    
    [SerializeField]
    PadMovement opponent;

    void Start() {
        netView = GetComponent<NetworkView>();
        netView.RPC("ClientConnected", RPCMode.All, "I HAS CONNECTED MASTER"); 
    }

    [RPC]
    void UpdatePad(float padPos, float padVel) { 
        // Updates the opponents pad.

        opponent.Position = -padPos;
        opponent.PadVel = -padVel;

    }

    [RPC]
    void UpdateBall(Vector2 ballPos, Vector2 ballVel) {
        // ballVel currently has no impact but has the
        // potential to be used for client-side prediction
        // in the event of latency.

        // Since the player is always left, mirror the ball's position to reflect
        // its position on the host's view.
        Vector2 mirroredBallPos = new Vector2();

        mirroredBallPos.x = -ballPos.x;
        mirroredBallPos.y = -ballPos.y;
    }

    [RPC]
    void UpdateScore(int p1, int p2) {

    }
}
