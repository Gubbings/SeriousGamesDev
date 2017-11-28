using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour {

    public GameObject problemObject;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown() {
        if (problemObject.GetComponent<ConcreteProblem>() != null) {
            problemObject.GetComponent<ConcreteProblem>().playProblem();
        }

        if (problemObject.GetComponent<ProblemGenerator>() != null) {

        }
    }
}
