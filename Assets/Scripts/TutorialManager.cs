using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {


    public Text textObject1;
    public Text textObject2;

    public GameObject Tut1_Panel;
    public GameObject Tut2_Panel;
    public GameObject staff;

    public GameObject signatureButton;
    public GameObject signaturePanel;

    public GameObject chordTypeButton;
    public GameObject chordTypePanel;

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
                textObject1.text = "Classical harmony has many aspects but we will focus on cord progressions. " +
                    "We can label chords of a key numerically starting from the base note of the key. For example in C major chord 1 is C and chord 2 is D.";
                break;

            case 2:
                textObject1.text = "This means you are going to need to know your key signatures!\n" +
                    "We will focus on simple major keys.";                  
                break;

            case 3:            
                Tut1_Panel.SetActive(false);
                staff.SetActive(true);
                Tut2_Panel.SetActive(true);

                textObject2.text = "The key signature will be shown on the left of the staff next to the treble cleff. We will use examples with at most 5 sharps shown here.\n" +
                    "Lets take a look at the key signatures for the keys that we will focus on.";
                break;

            case 4:
                Tut1_Panel.SetActive(false);
                staff.SetActive(true);
                Tut2_Panel.SetActive(true);

                textObject2.text = "C Major - no sharps\n" +
                    "D Major - F# C#\n " +
                    "E Major - F# C# G# D#\n " +
                    "F Major - B♭\n" +                    
                    "G Major - F#\n" +
                    "A Major - F# C# G#\n" +
                    "B Major-  F# C# G# D# A#";
                break;

            case 5:
                signatureButton.SetActive(true);

                textObject2.text = "Well keep those tucked away to the left. You can click the arrow on the left to open the key signature list.";
                break;

            case 6:  
                textObject2.text = "Now onto the real problem. Chord progression. A harmonic progression consists of the following sequence of chord classifications:\n" +
                    "Tonic - Predominant - Dominant - Tonic.";
                break;

            case 7:
                textObject2.text = "Each cord classification represents a set of chords based on the key.\n" +
                    "Tonic has chords 1 and 6.\n" +
                    "Predominant has chords 2, 3, 4 and 6.\n" +
                    "Dominant has chords 5 and 7.";
                break;

            case 8:
                chordTypeButton.SetActive(true);

                textObject2.text = "Anyone can read the notes but you will need to identify it by ear. To help you out we will keep the chord classification available on the right.\n" +
                    "You can click the arrow on the right to open the chord classification list.";
                break;

            case 9:                
                textObject2.text = "Now that you have everything you need I will give you a sequence of 8 notes. Like this.";
                break;

            case 10:
                textObject2.text = "But some of the notes will be missing. You need to listen carefully to decide what those missing notes are. Then play them on this piano.";
                break;
        }
    }


    public void ToggleSignaturePanel() {
        if (signaturePanel.activeSelf) {
            signaturePanel.SetActive(false);
        }
        else {
            signaturePanel.SetActive(true);
        }
    }


    public void ToggleChordTypePanel() {
        if (chordTypePanel.activeSelf) {
            chordTypePanel.SetActive(false);
        }
        else {
            chordTypePanel.SetActive(true);
        }
    }
}
