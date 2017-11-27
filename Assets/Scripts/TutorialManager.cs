using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {


    public Text textObject1;
    public Text textObject2;

    public GameObject Tut1_Panel;
    public GameObject Tut2_Panel;
    public GameObject staff;
    public GameObject piano;

    public GameObject signatureButton;
    public GameObject signaturePanel;

    public GameObject chordTypeButton;
    public GameObject chordTypePanel;

    public GameObject problemObject;
    public GameObject replayObject;

    public GameObject correctPianoKey;
    public Material correctKeyMat;

    public GameObject revealKeyButton;
    public GameObject replayFirstNoteButton;

    private int tutorialStage = 0;
    

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateTutorialText() {

        if (problemObject.GetComponent<ConcreteProblem>().currentlyPlaying) {
            //return;
        }

        if (problemObject.GetComponent<TutorialProblem>().currentlyPlaying) {
            //return;
        }

        tutorialStage++;

        switch (tutorialStage) {
            case 1:
                textObject1.text = "Classical melodies are based on classical harmony which has many aspects but we will focus on cord progressions. " +
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

                textObject2.text = "The key signature will be shown on the left of the staff next to the treble clef. We will use examples with at most 5 sharps shown here.\n" +
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

                //problemObject.GetComponent<ConcreteProblem>().setupTutorialProbblem(-1);
                problemObject.GetComponent<TutorialProblem>().setupTutorialProbblem(-1);

                textObject2.text = "Now that you have everything you need I will give you a sequence of 8 notes representing a pair of chord progresions. This example is in the key of C Major.";
                break;

            case 10:

                //problemObject.GetComponent<ConcreteProblem>().setupTutorialProbblem(3);
                problemObject.GetComponent<TutorialProblem>().setupTutorialProbblem(3);

                textObject2.text = "But some of the notes will be missing. You need to listen carefully to decide what those missing notes are. Missing note positions are marked with a blue bar.";
                break;

            case 11:

                //problemObject.GetComponent<ConcreteProblem>().playProblem();
                problemObject.GetComponent<TutorialProblem>().playProblem();
                replayObject.SetActive(true);

                textObject2.text = "Now you will use this piano to play the correct note. If you need to hear the sequence again click on the replay button to the right of the staff. " +
                    "Dont worry if you make a mistake you will get three chances before I reveal the answer.";

                Tut2_Panel.transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "Ok Got It";
               
                break;

            case 12:

                piano.SetActive(true);
                Tut2_Panel.SetActive(false);

                correctPianoKey.GetComponent<Renderer>().material = correctKeyMat;

                break;

            case 13:

                piano.SetActive(false);
                Tut2_Panel.SetActive(true);

                revealKeyButton.SetActive(true);
                replayFirstNoteButton.SetActive(true);

                textObject2.text = "Great you get the idea. If you get stuck I'll also give you some help. You can reveal the key or replay the first missing note.";
                Tut2_Panel.transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "Continue";

                break;

            case 14:
                textObject2.text = "Alright thats everything. Now you can start practicing where you can earn points depending on your performance. " +
                    "Incorrect answers will reduce your points so make sure you are confident with your answer.";
                Tut2_Panel.transform.Find("Button").transform.Find("Text").GetComponent<Text>().text = "Take Me To Practice";

                break;

            case 15:
                PlayerPrefs.SetInt("CompletedTutorial", 1);
                SceneManager.LoadScene("PracticeMode");
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
