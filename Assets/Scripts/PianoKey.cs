﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour {

    private AudioSource audioGenerator;

    public enum notes {
        C,
        Csharp, 
        D,
        Dsharp, 
        E,
        F,
        Fsharp, 
        G,
        Gsharp, 
        A,
        Asharp, 
        B        
    };

    public notes note;
    public int octaveShift = 0;
    
	// Use this for initialization
	void Start () {
        audioGenerator = GameObject.Find("SoundGenerator").GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown() {
        if (audioGenerator == null) {
            return;
        }

        TutorialProblem tp = GameObject.Find("Problem").GetComponent<TutorialProblem>();
        ConcreteProblem cp = GameObject.Find("Problem").GetComponent<ConcreteProblem>();
        ProblemGenerator pg = GameObject.Find("Problem").GetComponent<ProblemGenerator>();

        if (tp != null) {
            if (tp.currentlyPlaying) {
                return;
            }
            
            GameObject.Find("TutorialManager").GetComponent<TutorialManager>().UpdateTutorialText();
            tp.submitAnswer(note, octaveShift);
        }
        else if (cp != null) {

            if (cp.currentlyPlaying) {
                return;
            }

            if (cp.difficulty == ProblemGenerator.difficulty.TUTORIAL) {
                GameObject.Find("TutorialManager").GetComponent<TutorialManager>().UpdateTutorialText();
            }

            cp.submitAnswer(note, octaveShift);
        }
        else if(pg != null){
            if (cp.currentlyPlaying) {
                return;
            }

            if (cp.difficulty == ProblemGenerator.difficulty.TUTORIAL) {
                GameObject.Find("TutorialManager").GetComponent<TutorialManager>().UpdateTutorialText();
            }

            cp.submitAnswer(note, octaveShift);
        }

        audioGenerator.pitch = calcPitch((int)note, octaveShift);
        audioGenerator.Play();
    }

    public static float calcPitch(int note, int octaveShift) {
        float offset = 0;

        for (int i = (int)notes.C; i < note; i++) {
            offset += 1f;
        }       
        
        if(octaveShift != 0) {
            offset += octaveShift * 12;
        }

        offset /= 12f;

        return Mathf.Pow(2, offset);
    }
}
