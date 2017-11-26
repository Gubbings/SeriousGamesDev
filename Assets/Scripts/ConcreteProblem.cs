using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteProblem : MonoBehaviour {

    [System.Serializable]
    public struct ProblemNote {
        public PianoKey.notes note;
        public int octaveShift;
    }

    public float noteDelay = 1.5f;
    public ProblemNote[] problemSequence;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = this.GetComponent<AudioSource>();

        IEnumerator routine = playProblem(problemSequence);
        StartCoroutine(routine);
    }

	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator playProblem(ProblemNote[] problem) {

        for (int i = 0; i < problem.Length; i++) {
            yield return new WaitForSeconds(noteDelay);
            audioSource.pitch = PianoKey.calcPitch((int)problem[i].note, problem[i].octaveShift);
            audioSource.Play();            
        }
    }
}
