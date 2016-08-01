using UnityEngine;
using System.Collections;

public class CastingExperiment : MonoBehaviour {

    public GameObject eg; //Casting with an obj doesnt work as obj is prolly the top of the hierachy.
    public MonoBehaviour a;

    string example;
    int lol;
    float lol1 = 2;

    void Start() {
        //a = (AIBase)eg;
    }

    void Update() {
        if (a is AIBase)
            Debug.Log("Working");

        //a = eg as AIBase; //as operator
        // a = (AIBase) eg; //casting
       // if (eg is MonoBehaviour) {
           // Debug.Log("Working");
       // } else
            //Debug.Log("Not working");
       /// a.Health++;

       // Debug.Log(a.Health);
    }
}
