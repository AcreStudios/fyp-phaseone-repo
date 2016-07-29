using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour {

    Collider[] troops;

    
// Use this for initialization
    void Start () { 
	    
	}
	
	// Update is called once per frame
	void Update () {
        troops = Physics.OverlapSphere(transform.position, 1);
        
        foreach (Collider troop in troops) {
            if (troop.tag == "Vision")
            troop.transform.root.gameObject.GetComponent<AIBase>().Triggered(transform);
        }
    }

   /* void OnCollisionEnter(Collision collision) {
        if (collision.transform.gameObject.tag == "Vision")
            collision.transform.root.gameObject.GetComponent<AIBase>().Triggered(transform);
    }
    void OnCollisionExit(Collision collision) {
        if (collision.transform.gameObject.tag == "Vision")
            collision.transform.root.gameObject.GetComponent<AIBase>().Triggered(null);
    }*/
}
