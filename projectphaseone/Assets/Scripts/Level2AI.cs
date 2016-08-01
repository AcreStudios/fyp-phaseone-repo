using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class Level2AI : AIBase {

    enum AIStates {
        Idle,
        Attacking
    }

    AIStates currentState;
    NavMeshAgent agent;

    void Start() {
        currentState = AIStates.Idle;
        agent = GetComponent<NavMeshAgent>();
        startingPoint = transform.position;
    }

    void Update() {
        switch (currentState) {
            case AIStates.Idle:
                if ((startingPoint - transform.position).magnitude < 5) {
                    agent.speed = 0;
                    LookAround();
                } else {
                    agent.destination = startingPoint;
                    agent.speed = 3.5f;
                }
                

                if (target != null) {
                    AlertOtherTroops();
                    currentState = AIStates.Attacking;
                }
                break;

            case AIStates.Attacking:
                if (target != null) {
                    if ((target.position - transform.position).magnitude < range / 2) {
                        agent.speed = 0;
                    } else {
                        agent.destination = target.position;
                        agent.speed = 3.5f;
                    }

                    transform.LookAt(target);

                    if (Shooting()) {

                    } else {
                        target = null;
                    }
                } else {
                    GroupWithAllies();
                    currentState = AIStates.Idle;
                }
                break;
        }
    }
}
