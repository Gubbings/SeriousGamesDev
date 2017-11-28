using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConcreteProblem : MonoBehaviour {

    [System.Serializable]
    public struct ProblemNote {
        public PianoKey.notes note;
        public int octaveShift;
    }

    public int pointsToAward = 10;

    public GameObject[] noteObjectSets;
    public GameObject keySignatureObject;

    public float noteDelay = 1.5f;

    public ProblemGenerator.difficulty difficulty;

    public ProblemGenerator.keys key;

    public List<int> missingNoteIndexes;
    public ProblemNote[] problemSequence;

    public Material defaultMat;
    public Material correctMat;
    public Material incorrectMat;
    public Material barMat;

    public GameObject revealKeyButton;
    public GameObject replayFirstNoteButton;

    public GameObject possiblePointsText;
    public Text scoreText;

    public GameObject piano;
    public GameObject speechPanel;
    public Text SpeechText;    

    [HideInInspector]
    public int currentMissingNote;

    [HideInInspector]
    public bool currentlyPlaying = false;

    [HideInInspector]
    public int questionAttempts = 1;

    private AudioSource audioSource;
    private bool revealedKey = false;    
    private int score;
    private bool mistake = false;

    private IEnumerator routine;

    // Use this for initialization
    void Start() {

        score = PlayerPrefs.GetInt("score");
        scoreText.text = "Score: " + score;

        possiblePointsText.GetComponentInChildren<Text>().text = "Possible Points: " + pointsToAward;

        if (missingNoteIndexes.Count != 0) {
            currentMissingNote = missingNoteIndexes[0];
        }

        currentlyPlaying = false;
        audioSource = this.GetComponent<AudioSource>();

        if (difficulty != ProblemGenerator.difficulty.TUTORIAL && problemSequence.Length != 0) {
            setupProbblemVisuals();
            playProblem();
        }
    }

	// Update is called once per frame
	void Update () {
       
    }

    public void playProblem() {
        if (currentlyPlaying) {
            return;
        }

        currentlyPlaying = true;

        routine = playProblem(problemSequence);
        StartCoroutine(routine);
    }


    public void setupProbblemVisuals() {

        mistake = false;
        possiblePointsText.GetComponentInChildren<Text>().text = "Possible Points: " + pointsToAward;
        score = PlayerPrefs.GetInt("score");
        scoreText.text = "Score: " + score;

        if (missingNoteIndexes.Count != 0) {
            currentMissingNote = missingNoteIndexes[0];
        }

        //hide all sharps/flats
        for (int i = 0; i < keySignatureObject.transform.childCount; i++) {
            keySignatureObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        //show appropriate key signature
        showKeySignature(key);

        //hide all notes
        for (int i = 0; i < problemSequence.Length; i++) {
            for (int j = 0; j < noteObjectSets[i].transform.childCount; j++) {

                if (noteObjectSets[i].transform.GetChild(j).name == "MissingNoteBar") {
                    noteObjectSets[i].transform.GetChild(j).GetComponentInChildren<Renderer>().material = barMat;
                }
                else { 
                    noteObjectSets[i].transform.GetChild(j).GetComponentInChildren<Renderer>().material = defaultMat;                    
                }

                noteObjectSets[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }

        //show some of the notes and hide the given amount for the difficulty
        for (int i = 0; i < problemSequence.Length; i++) {

            if (missingNoteIndexes.Contains(i)) {
                noteObjectSets[i].transform.Find("MissingNoteBar").gameObject.SetActive(true);
                continue;
            }

            showNote(i, problemSequence[i].note, problemSequence[i].octaveShift, defaultMat);
        }
    }

    /*
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
                continue;
            }

            showNote(i, problemSequence[i].note, problemSequence[i].octaveShift, defaultMat);           
        }
    }
    */

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
            noteObjectSets[noteIndex].transform.GetChild(i).GetComponentInChildren<Renderer>().material = mat;            
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


    public bool submitAnswer(PianoKey.notes note, int octaveShift) {
        
        if (currentMissingNote > missingNoteIndexes[missingNoteIndexes.Count - 1]) {
            currentMissingNote = missingNoteIndexes[0];            
        }

        for (int i = 0; i < noteObjectSets[currentMissingNote].transform.childCount; i++) {
            noteObjectSets[currentMissingNote].transform.GetChild(i).GetComponentInChildren<Renderer>().material = defaultMat;
            noteObjectSets[currentMissingNote].transform.GetChild(i).gameObject.SetActive(false);
        }

        if (note == problemSequence[currentMissingNote].note && octaveShift == problemSequence[currentMissingNote].octaveShift) {
            showNote(currentMissingNote, note, octaveShift, correctMat);


            if (currentMissingNote == missingNoteIndexes[missingNoteIndexes.Count - 1] && !mistake) {
                score += pointsToAward;
                PlayerPrefs.SetInt("score", score);
                scoreText.text = "Score: " + score;

                piano.SetActive(false);
                congradulate();

                return true;
            }
            else if (currentMissingNote == missingNoteIndexes[missingNoteIndexes.Count - 1]) {
                questionAttempts++;
            }
        }
        else {
            mistake = true;
            showNote(currentMissingNote, note, octaveShift, incorrectMat);            

            if (currentMissingNote == missingNoteIndexes[missingNoteIndexes.Count - 1]) {
                questionAttempts++;
                pointsToAward -= 2;

                IEnumerator enumerator = showIncorrect();
                StartCoroutine(enumerator);                
            }

            
        }

        if (questionAttempts >= 3) {
            piano.SetActive(false);
            showAnswer();
        }

        currentMissingNote++;
        return false;
    }

    public IEnumerator showIncorrect() {
        yield return new WaitForSeconds(1);
        setupProbblemVisuals();
    }

    public void congradulate() {
        currentlyPlaying = false;
        StopAllCoroutines();
        piano.SetActive(false);
        speechPanel.SetActive(true);

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "PracticeLevel9") {
            SpeechText.text = "You have completed all of the practice levels! You can play more levels in endless mode.";
        }
        else {
            SpeechText.text = "Well done Maestro! Onto the next level.";
        }        
    }

    public void showAnswer() {
        currentlyPlaying = false;
        StopAllCoroutines();
        missingNoteIndexes.Clear();
        setupProbblemVisuals();

        piano.SetActive(false);
        speechPanel.SetActive(true);
        SpeechText.text = "Good try. You can see the answer above. Try again on the next level or in endless mode.";
    }

    public void nextLevel() {
        string sceneName = SceneManager.GetActiveScene().name;


        if (sceneName == "PracticeLevel9") {
            SceneManager.LoadScene("LevelSelectionScene");
        }
        else {
            SceneManager.LoadScene("PracticeLevel" + (int.Parse(sceneName.Substring(sceneName.Length - 1, 1)) + 1));
        }        
    }

    public void revealKey() {

        if (revealedKey) {
            return;
        }

        if (difficulty != ProblemGenerator.difficulty.EASY || difficulty != ProblemGenerator.difficulty.TUTORIAL) {
            pointsToAward -= 2;
        }

        revealKeyButton.GetComponentInChildren<Text>().text = ProblemGenerator.keyNames[(int)key];

        revealedKey = true;
    }

    public void replayFirstMissingNote() {
        if (currentlyPlaying) {
            return;
        }

        audioSource.pitch = PianoKey.calcPitch((int)problemSequence[missingNoteIndexes[0]].note, problemSequence[missingNoteIndexes[0]].octaveShift);
        audioSource.Play();
    }

    public void ToggleSignaturePanel() {

        GameObject signaturePanel = GameObject.Find("Canvas").transform.Find("SignaturePanel").gameObject;

        if (signaturePanel.activeSelf) {
            signaturePanel.SetActive(false);
        }
        else {
            signaturePanel.SetActive(true);
        }
    }


    public void ToggleChordTypePanel() {
        GameObject chordTypePanel = GameObject.Find("Canvas").transform.Find("ChordTypePanel").gameObject;

        if (chordTypePanel.activeSelf) {
            chordTypePanel.SetActive(false);
        }
        else {
            chordTypePanel.SetActive(true);
        }
    }
}
