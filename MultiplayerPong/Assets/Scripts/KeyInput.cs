using UnityEngine;

public class KeyInput : MonoBehaviour {

    bool up, down = false;

    public bool Up {
        get { return up; }
    }

    public bool Down {
        get { return down; }
    }

    void Update() {
        up = Input.GetKey(KeyCode.A)         || Input.GetKey(KeyCode.W) ||
             Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow);

        down = Input.GetKey(KeyCode.D)          || Input.GetKey(KeyCode.S) ||
               Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow);

        if (up && down) {
            up = down = false;
        }
    }

}
