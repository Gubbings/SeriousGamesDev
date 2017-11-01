using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour {

    AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
        int  note = -1;        

        if (Input.GetKeyDown("a")) note = 0;  // C
        if (Input.GetKeyDown("s")) note = 2;  // D
        if (Input.GetKeyDown("d")) note = 4;  // E
        if (Input.GetKeyDown("f")) note = 5;  // F
        if (Input.GetKeyDown("g")) note = 7;  // G
        if (Input.GetKeyDown("h")) note = 9;  // A
        if (Input.GetKeyDown("j")) note = 11; // B
        if (Input.GetKeyDown("k")) note = 12; // C
        if (Input.GetKeyDown("l")) note = 14; // D


        //NOTE: movement of 1/12 is 1 semitone        
        //B to C is already 1 semitone
        //E to F is already 1 semitone
        //Any other combination of X to Y where the distance is in the alphabet is 1, the distance is 2 semitones
        //NoteX to sharp or flat of noteX is 1 semitone

        if (note >= 0) { 
            audio.pitch = Mathf.Pow(2, (note) / 12.0f);
            audio.Play();
        }
    }
}
