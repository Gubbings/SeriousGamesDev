using System.Collections;
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

    /*
    [HideInInspector]
    public static notes globalNote;
    */

    public notes note;
    public int octaveShfit = 0;
    
	// Use this for initialization
	void Start () {
        audioGenerator = GameObject.Find("SoundGenerator").GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown() {
        audioGenerator.pitch = calcPitch((int)note, octaveShfit);
        audioGenerator.Play();
    }

    private float calcPitch(int note, int octaveShift) {
        if (audioGenerator == null) {
            return 0;
        }

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
