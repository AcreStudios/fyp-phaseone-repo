using UnityEngine;
using System.Collections;

public class Killurself : MonoBehaviour {

    public Canvas fadeObj;
	// Use this for initialization
	void Start () {
        StartCoroutine(fadeOuttahere());
	}

    IEnumerator fadeOuttahere() {
        yield return new WaitForSeconds(8);
        fadeObj.enabled = false;
    }
}
