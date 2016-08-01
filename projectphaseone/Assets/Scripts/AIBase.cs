using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour {

    Transform[] guns = new Transform[2];
    public Vector2 rotationRange;
    public float shootInterval;
    public float gunSprayValue;
    public float range;

    public float Health { get; set; }

    protected Transform target;
    const float lerpAdditionValue = 0.03f;
    float lerpValue;
    float targetHeadRotation;

    float prevHeadRotation;
    float shootingTime;
    float rotateTime;
    bool ableToRotate;
    protected Vector3 startingPoint;

    void Awake() {
        Health = 100;
        ableToRotate = true;
        gameObject.tag = "Enemy";

        guns[0] = transform.Find("Hanna_GunL");
        guns[1] = transform.Find("Hanna_GunR");
    }

    public virtual void DamageRecieved(float damage) {
        Health -= damage;
        if (Health <= 0) {
            Destroy(gameObject);
        }
    }

    public void LookAround() {
        if (rotateTime < Time.time) {
            if (lerpValue >= 1 || prevHeadRotation == targetHeadRotation) {
                lerpValue = 0;

                prevHeadRotation = targetHeadRotation;
                targetHeadRotation = Random.Range(rotationRange.x, rotationRange.y);
            } else {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Lerp(prevHeadRotation, targetHeadRotation, lerpValue), transform.eulerAngles.z);
                lerpValue += lerpAdditionValue;
                if (lerpValue >= 1 || prevHeadRotation == targetHeadRotation) {
                    rotateTime = Time.time + Random.Range(3, 6);
                }
            }
        }
    }

    public void AlertOtherTroops() {
        Collider[] troops;

        troops = Physics.OverlapSphere(transform.position, 20);
        foreach (Collider troop in troops) {
            if (troop.tag == "Enemy")
                troop.transform.gameObject.GetComponent<AIBase>().target = target;
        }
    }

    public void Triggered(Transform player) {
        target = player;
    }

    public void GroupWithAllies() {
        GameObject[] troops = GameObject.FindGameObjectsWithTag("Enemy");
        float dist;
        dist = Mathf.Infinity;

        foreach (GameObject troop in troops) {
            if (troop != gameObject) {
                float tempDist;

                tempDist = (troop.transform.position - startingPoint).magnitude;

                if (dist > tempDist) {
                    dist = tempDist;
                    startingPoint = troop.transform.position;
                }
            }
        }
    }

    public bool Shooting() {
        if (Time.time > shootingTime) {
            if ((target.position - transform.position).magnitude < range) {
                Vector3 offset;
                AlertOtherTroops();

                offset = new Vector3(Random.Range(-gunSprayValue, gunSprayValue), Random.Range(-gunSprayValue, gunSprayValue), 0);
                foreach (Transform gun in guns) {
                    gun.LookAt(target);
                    Debug.DrawRay(gun.position, gun.TransformDirection(0, 0, 20) + offset, Color.blue, 0.5f);
                }
                shootingTime = Time.time + shootInterval;
                return true;
            } else {
                return false;
            }
        }
        return true;
    }
}
