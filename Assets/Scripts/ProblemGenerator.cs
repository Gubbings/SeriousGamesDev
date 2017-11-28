using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemGenerator : MonoBehaviour {

    public enum difficulty {
        TUTORIAL,
        EASY,
        NORMAL,
        HARD
    };

    public enum keys {
        C_MAJOR,
        D_MAJOR,
        E_MAJOR,
        F_MAJOR,
        G_MAJOR,
        A_MAJOR,
        B_MAJOR
    };

    public static string[] keyNames = { "C Major", "D Major", "E Major", "F Major", "G Major", "A Major", "B Major" };

    public List<PianoKey.notes>[] keySignatures = {
        new List<PianoKey.notes>() {},
        new List<PianoKey.notes>() { PianoKey.notes.Fsharp, PianoKey.notes.Csharp},
        new List<PianoKey.notes>() { PianoKey.notes.Fsharp, PianoKey.notes.Csharp, PianoKey.notes.Gsharp, PianoKey.notes.Dsharp},
        new List<PianoKey.notes>() { PianoKey.notes.Asharp },
        new List<PianoKey.notes>() { PianoKey.notes.Fsharp },
        new List<PianoKey.notes>() { PianoKey.notes.Fsharp, PianoKey.notes.Csharp, PianoKey.notes.Gsharp },
        new List<PianoKey.notes>() { PianoKey.notes.Fsharp, PianoKey.notes.Csharp, PianoKey.notes.Gsharp, PianoKey.notes.Dsharp, PianoKey.notes.Asharp }
    };

    private enum harmoicFunctions {
        TONIC,
        PREDOMINANT,
        DOMINANT
    };
    private List<int>[] harmonicFunctionCords = {
        new List<int>() { 1, 6 },
        new List<int>() { 2, 4, 6 },
        new List<int>() { 5, 7 }
    };

    private ConcreteProblem cp;

    // Use this for initialization
    void Start() {
        cp = GetComponent<ConcreteProblem>();
        GenerateProblem();
    }

    // Update is called once per frame
    void Update() {

    }

    public void GenerateProblem() {
        cp.speechPanel.SetActive(false);
        cp.piano.SetActive(true);

        keys key = (keys)Random.Range(0, 6);
        List<PianoKey.notes> keySignature = keySignatures[(int)key];

        List<PianoKey.notes> basicNotes = new List<PianoKey.notes>();
        for (int i = 0; i < 12; i++) {
            PianoKey.notes currNote = (PianoKey.notes)i;

            if (currNote == PianoKey.notes.Asharp ||
                currNote == PianoKey.notes.Csharp ||
                currNote == PianoKey.notes.Dsharp ||
                currNote == PianoKey.notes.Fsharp ||
                currNote == PianoKey.notes.Gsharp) {

                if (!keySignature.Contains(currNote)) {
                    continue;
                }
            }

            if (currNote == PianoKey.notes.C && keySignature.Contains(PianoKey.notes.Csharp) ||
               currNote == PianoKey.notes.D && keySignature.Contains(PianoKey.notes.Dsharp) ||
               currNote == PianoKey.notes.F && keySignature.Contains(PianoKey.notes.Fsharp) ||
               currNote == PianoKey.notes.A && keySignature.Contains(PianoKey.notes.Asharp) ||
               currNote == PianoKey.notes.G && keySignature.Contains(PianoKey.notes.Gsharp)) {

                continue;
            }


            basicNotes.Add(currNote);
        }



        int keyStartingNote = (int)key;

        List<PianoKey.notes> tonicNotes = new List<PianoKey.notes>();

        for (int i = 0; i < harmonicFunctionCords[(int)harmoicFunctions.TONIC].Count; i++) {

            int cordStartingNote = (keyStartingNote + harmonicFunctionCords[(int)harmoicFunctions.TONIC][i] - 1) % basicNotes.Count;

            for (int j = 0; j < 3; j++) {

                if (!tonicNotes.Contains(basicNotes[(cordStartingNote + j * 2) % basicNotes.Count])) {
                    tonicNotes.Add(basicNotes[(cordStartingNote + j * 2) % basicNotes.Count]);
                }
            }
        }



        List<PianoKey.notes> predominantNotes = new List<PianoKey.notes>();

        for (int i = 0; i < harmonicFunctionCords[(int)harmoicFunctions.PREDOMINANT].Count; i++) {

            int cordStartingNote = (keyStartingNote + harmonicFunctionCords[(int)harmoicFunctions.PREDOMINANT][i] - 1) % basicNotes.Count;

            for (int j = 0; j < 3; j++) {

                if (!predominantNotes.Contains(basicNotes[(cordStartingNote + j * 2) % basicNotes.Count])) {
                    predominantNotes.Add(basicNotes[(cordStartingNote + j * 2) % basicNotes.Count]);
                }
            }
        }



        List<PianoKey.notes> dominantNotes = new List<PianoKey.notes>();

        for (int i = 0; i < harmonicFunctionCords[(int)harmoicFunctions.DOMINANT].Count; i++) {

            int cordStartingNote = (keyStartingNote + harmonicFunctionCords[(int)harmoicFunctions.DOMINANT][i] - 1) % basicNotes.Count;

            for (int j = 0; j < 3; j++) {

                if (!dominantNotes.Contains(basicNotes[(cordStartingNote + j * 2) % basicNotes.Count])) {
                    dominantNotes.Add(basicNotes[(cordStartingNote + j * 2) % basicNotes.Count]);
                }
            }
        }



        //generate the actual problem of 8 notes
        ConcreteProblem.ProblemNote[] problemNotes = new ConcreteProblem.ProblemNote[8];

        problemNotes[0].note = tonicNotes[Random.Range(0, 2)];
        problemNotes[1].note = predominantNotes[Random.Range(0, predominantNotes.Count - 1)];
        problemNotes[2].note = dominantNotes[Random.Range(0, dominantNotes.Count - 1)];
        problemNotes[3].note = tonicNotes[Random.Range(0, tonicNotes.Count - 1)];

        problemNotes[4].note = tonicNotes[Random.Range(0, tonicNotes.Count - 1)];
        problemNotes[5].note = predominantNotes[Random.Range(0, predominantNotes.Count - 1)];
        problemNotes[6].note = dominantNotes[Random.Range(0, dominantNotes.Count - 1)];
        problemNotes[7].note = tonicNotes[Random.Range(0, 2)];

        for (int i = 0; i < problemNotes.Length; i++) {
            problemNotes[i].octaveShift = 0;
        }


        for (int i = 0; i < problemNotes.Length; i++) {
            if (problemNotes[i].note == PianoKey.notes.A || problemNotes[i].note == PianoKey.notes.Asharp || problemNotes[i].note == PianoKey.notes.B) {
                problemNotes[i].octaveShift = -1;
            }
        }
        
        cp.key = key;
        cp.pointsToAward = 10;
        cp.questionAttempts = 0;
        cp.problemSequence = problemNotes;

        cp.missingNoteIndexes = new List<int>();
        switch (cp.difficulty) {

            case difficulty.EASY:

                int missingNoteIndex = Random.Range(1, 7);
                cp.missingNoteIndexes.Add(missingNoteIndex);

                break;

            case difficulty.NORMAL:

                int firstMissingNoteIndex = Random.Range(1, 5);

                for (int i = 0; i < 3; i++) {
                    cp.missingNoteIndexes.Add(firstMissingNoteIndex + i);
                }

                break;

            case difficulty.HARD:

                for (int i = 1; i < 8; i++) {
                    cp.missingNoteIndexes.Add(i);
                }

                break;
        }
        
        cp.setupProbblemVisuals();
        cp.playProblem();
    }

    public void submitAnswer(PianoKey.notes note, int octaveShift) {        
        if (cp.submitAnswer(note, octaveShift)) {
            cp.piano.SetActive(false);
            cp.speechPanel.SetActive(true);            
            cp.SpeechText.text = "Well done Maestro! Continue to play again.";
        }
        else if (cp.questionAttempts >= 3) {
            cp.piano.SetActive(false);
            cp.speechPanel.SetActive(true);
            cp.SpeechText.text = "This was a tough one. Take a look at the answer before trying a new question.";            
        }       
    }
}
