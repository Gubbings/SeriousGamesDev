using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialProblem : MonoBehaviour {

    [System.Serializable]
    public struct ProblemNote {
        public PianoKey.notes note;
        public int octaveShift;
    }


    public GameObject[] noteObjectSets;
    public GameObject keySignatureObject;

    public float noteDelay = 1.5f;

    public ProblemGenerator.difficulty difficulty;

    public ProblemGenerator.keys key;

    public ProblemNote[] problemSequence;

    public Material defaultMat;
    public Material correctMat;
    public Material incorrectMat;

    public GameObject revealKeyButton;
    public GameObject replayFirstNoteButton;


    [HideInInspector]
    public int currentMissingNote;

    [HideInInspector]
    public bool currentlyPlaying = false;

    private AudioSource audioSource;

    private bool revealedKey = false;

    // Use this for initialization
    void Start() {
        currentlyPlaying = false;
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void playProblem() {
        if (currentlyPlaying) {
            return;
        }

        currentlyPlaying = true;

        IEnumerator routine = playProblem(problemSequence);
        StartCoroutine(routine);
    }


    public void setupTutorialProbblem(int hiddenNoteIndex) {

        currentMissingNote = hiddenNoteIndex;

        //hide all sharps/flats
        for (int i = 0; i < keySignatureObject.transform.childCount; i++) {
            keySignatureObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        //show appropriate key signature
        showKeySignature(key);

        //hide all notes
        for (int i = 0; i < problemSequence.Length; i++) {
            for (int j = 0; j < noteObjectSets[i].transform.childCount; j++) {
                noteObjectSets[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }

        //show some of the notes and hide the given amount for the difficulty
        for (int i = 0; i < problemSequence.Length; i++) {

            if (i == hiddenNoteIndex) {
                noteObjectSets[i].transform.Find("MissingNoteBar").gameObject.SetActive(true);
                continue;
            }

            showNote(i, problemSequence[i].note, problemSequence[i].octaveShift, defaultMat);
        }
    }

    public IEnumerator playProblem(ProblemNote[] problem) {

        for (int i = 0; i < problem.Length; i++) {
            audioSource.pitch = PianoKey.calcPitch((int)problem[i].note, problem[i].octaveShift);
            audioSource.Play();
            yield return new WaitForSeconds(noteDelay);
        }

        currentlyPlaying = false;
    }

    public void showNote(int noteIndex, PianoKey.notes note, int octaveShift, Material mat) {

        for (int i = 0; i < noteObjectSets[noteIndex].transform.childCount; i++) {
            if (noteObjectSets[noteIndex].transform.GetChild(i).name != "MissingNoteBar") {
                noteObjectSets[noteIndex].transform.GetChild(i).GetComponentInChildren<Renderer>().material = mat;
            }
        }

        switch (note) {

            case PianoKey.notes.A:
            case PianoKey.notes.Asharp:
                noteObjectSets[noteIndex].transform.Find("A_Note").gameObject.SetActive(true);
                break;

            case PianoKey.notes.B:
                noteObjectSets[noteIndex].transform.Find("B_Note").gameObject.SetActive(true);
                break;

            case PianoKey.notes.C:
            case PianoKey.notes.Csharp:
                if (octaveShift == 1) {
                    noteObjectSets[noteIndex].transform.Find("C_Note_OctaveUp").gameObject.SetActive(true);
                }
                else if (octaveShift == -1) {
                    noteObjectSets[noteIndex].transform.Find("C_Note_OctaveDown").gameObject.SetActive(true);
                }
                else {
                    noteObjectSets[noteIndex].transform.Find("C_Note").gameObject.SetActive(true);
                }

                break;

            case PianoKey.notes.D:
            case PianoKey.notes.Dsharp:
                if (octaveShift == -1) {
                    noteObjectSets[noteIndex].transform.Find("D_Note_OctaveDown").gameObject.SetActive(true);
                }
                else {
                    noteObjectSets[noteIndex].transform.Find("D_Note").gameObject.SetActive(true);
                }
                break;

            case PianoKey.notes.E:
                if (octaveShift == -1) {
                    noteObjectSets[noteIndex].transform.Find("E_Note_OctaveDown").gameObject.SetActive(true);
                }
                else {
                    noteObjectSets[noteIndex].transform.Find("E_Note").gameObject.SetActive(true);
                }

                break;

            case PianoKey.notes.F:
            case PianoKey.notes.Fsharp:
                if (octaveShift == -1) {
                    noteObjectSets[noteIndex].transform.Find("F_Note_OctaveDown").gameObject.SetActive(true);
                }
                else {
                    noteObjectSets[noteIndex].transform.Find("F_Note").gameObject.SetActive(true);
                }
                break;

            case PianoKey.notes.G:
            case PianoKey.notes.Gsharp:
                if (octaveShift == -1) {
                    noteObjectSets[noteIndex].transform.Find("G_Note_OctaveDown").gameObject.SetActive(true);
                }
                else {
                    noteObjectSets[noteIndex].transform.Find("G_Note").gameObject.SetActive(true);
                }

                break;
        }
    }


    public void showKeySignature(ProblemGenerator.keys key) {

        switch (key) {

            case ProblemGenerator.keys.A_MAJOR:
                keySignatureObject.transform.Find("F-Sharp").gameObject.SetActive(true);
                keySignatureObject.transform.Find("C-Sharp").gameObject.SetActive(true);
                keySignatureObject.transform.Find("G-Sharp").gameObject.SetActive(true);
                break;

            case ProblemGenerator.keys.B_MAJOR:
                keySignatureObject.transform.Find("F-Sharp").gameObject.SetActive(true);
                keySignatureObject.transform.Find("C-Sharp").gameObject.SetActive(true);
                keySignatureObject.transform.Find("G-Sharp").gameObject.SetActive(true);
                keySignatureObject.transform.Find("D-Sharp").gameObject.SetActive(true);
                keySignatureObject.transform.Find("A-Sharp").gameObject.SetActive(true);
                break;

            case ProblemGenerator.keys.C_MAJOR:
                break;

            case ProblemGenerator.keys.D_MAJOR:
                keySignatureObject.transform.Find("F-Sharp").gameObject.SetActive(true);
                keySignatureObject.transform.Find("C-Sharp").gameObject.SetActive(true);
                break;

            case ProblemGenerator.keys.E_MAJOR:
                keySignatureObject.transform.Find("F-Sharp").gameObject.SetActive(true);
                keySignatureObject.transform.Find("C-Sharp").gameObject.SetActive(true);
                keySignatureObject.transform.Find("G-Sharp").gameObject.SetActive(true);
                keySignatureObject.transform.Find("D-Sharp").gameObject.SetActive(true);
                break;

            case ProblemGenerator.keys.F_MAJOR:
                keySignatureObject.transform.Find("B-Flat").gameObject.SetActive(true);
                break;

            case ProblemGenerator.keys.G_MAJOR:
                keySignatureObject.transform.Find("F-Sharp").gameObject.SetActive(true);
                break;
        }
    }


    public void submitAnswer(PianoKey.notes note, int octaveShift) {

        for (int i = 0; i < noteObjectSets[currentMissingNote].transform.childCount; i++) {

            if(noteObjectSets[currentMissingNote].transform.GetChild(i).name != "MissingNoteBar") { 
                noteObjectSets[currentMissingNote].transform.GetChild(i).GetComponentInChildren<Renderer>().material = defaultMat;
            }

            noteObjectSets[currentMissingNote].transform.GetChild(i).gameObject.SetActive(false);
        }

        if (note == problemSequence[currentMissingNote].note && octaveShift == problemSequence[currentMissingNote].octaveShift) {
            showNote(currentMissingNote, note, octaveShift, correctMat);
        }
        else {
            showNote(currentMissingNote, note, octaveShift, incorrectMat);
        }
    }

    public void revealKey() {

        if (revealedKey) {
            return;
        }

        revealKeyButton.GetComponentInChildren<Text>().text = ProblemGenerator.keyNames[(int)key];

        revealedKey = true;
    }

    public void replayFirstMissingNote() {
        if (currentlyPlaying) {
            return;
        }

        audioSource.pitch = PianoKey.calcPitch((int)problemSequence[currentMissingNote].note, problemSequence[currentMissingNote].octaveShift);
        audioSource.Play();
    }
}
