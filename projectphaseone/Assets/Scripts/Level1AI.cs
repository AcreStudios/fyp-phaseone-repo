using UnityEngine;
using System.Collections;

public class Level1AI : AIBase {

    enum AIStates {
        Idle,
        Attacking
    }

    AIStates currentState;

    void Start() {
        currentState = AIStates.Idle;
    }

    void Update() {
        switch (currentState) {
            case AIStates.Idle:
                LookAround();

                if (target != null) {
                    AlertOtherTroops();
                    currentState = AIStates.Attacking;
                }
                break;

            case AIStates.Attacking:
                if (target != null) {
                    transform.LookAt(target);
                    if (Shooting()) {

                    } else {
                        target = null;
                    }

                } else {
                    currentState = AIStates.Idle;
                }
                break;
        }
    }
}
