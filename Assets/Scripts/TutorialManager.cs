using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {


    public Text textObject;

    private int tutorialStage = 0;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateTutorialText() {

        tutorialStage++;

        switch (tutorialStage) {
            case 1:
                textObject.text = "Classical harmony has many aspects but we will focus on cord progressions. " +
                    "We can label cords of a key numerical starting from the base note of the key. For example in C major cord 1 is C and cord 2 is D.";
                break;
            case 2:
                textObject.text = "This means you are going to need to know your key signatures!\n" +
                    "We will focus on regular major keys. " +
                    "Lets take a look at the key signatures for the regular note major keys.";
                break;
            case 3:
                textObject.text = "C Major - no sharps\n" +
                    "D Major - F# C#\n " +
                    "E Major - F# C# G# D#\n " +
                    "F Major - A#\n" +
                    "G Major - F#\n" +
                    "A Major - F# C# G#\n" +
                    "B Major-  F# C# G# D# A#";
                break;
        }
    }

}
