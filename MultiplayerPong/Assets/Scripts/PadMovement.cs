using System.Collections;
using UnityEngine;

[RequireComponent(typeof(KeyInput))]
public class PadMovement : MonoBehaviour {

    Transform tf;
    KeyInput keyInput;

    [SerializeField]
    float position;

    [SerializeField]
    float maxPositionValue = 10;

    float onePercent;

    [SerializeField]
    float maxTransformUp = 5.16f;

    [SerializeField]
    float padVel;
    
    [SerializeField]
    float maxVel = 2;

    bool up, down = false;

    public bool Opponent = false;

    public float Position {
        get { return position; }
        set {
            position = value;
            if (position > maxPositionValue) {
                position = maxPositionValue;
            } else if (position < -maxPositionValue) {
                position = -maxPositionValue;
            }
        }
    }

    public float MaxPostionValue {
        get { return maxPositionValue; }
    }

    public float PadVel {
        get { return padVel; }
        set {
            if (value > maxVel) {
                padVel = maxVel;
            } else if (value < -maxVel) {
                padVel = -maxVel;
            } else {
                padVel = value;
            }
        }
    }

    void Start() {
        tf = GetComponent<Transform>();
        keyInput = GetComponent<KeyInput>();
        onePercent = maxTransformUp / maxPositionValue;
    }

    public void UpdateMovementVars() {
        up = keyInput.Up;
        down = keyInput.Down;
    }

    void Update() {
        UpdateMovementVars();

        // Does not need to be done for local players.
        if (Opponent) {
            StartCoroutine(SimulateMovement(padVel));
        } else {
            if (up) {
                padVel += maxVel / 20;
            } else if (down) {
                padVel -= maxVel / 20;
            } else {
                //if (padVel > 0) {
                    padVel *= 0.8f;
                //} else if(padVel < 0) {
                //    padVel /= 0.5f;
                //}
            }

            if (padVel > maxVel) {
                padVel = maxVel;
            } else if (padVel < -maxVel) {
                padVel = -maxVel;
            }

            position += padVel;
        }

        UpdatePosition();
    }

    IEnumerator SimulateMovement(float oldVel) {
        yield return new WaitForSeconds(1f);
        if (oldVel == padVel) {
            // A newer vel hasn't arrived or is still
            // equal and can be used again to simula
            // -te movement.

            position += padVel;

            UpdatePosition();
        }
    }

    void UpdatePosition() {
        if (position > maxPositionValue) {
            position = maxPositionValue;
            padVel = 0;
        } else if (position < -maxPositionValue) {
            position = -maxPositionValue;
            padVel = 0;
        }

        tf.position = new Vector3(tf.position.x, position * onePercent, 0);
    }
}
