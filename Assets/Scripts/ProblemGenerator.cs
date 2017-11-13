using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemGenerator : MonoBehaviour {

    public enum keys {        
        C_MAJOR,
        D_MAJOR,
        E_MAJOR,
        F_MAJOR,
        G_MAJOR,
        A_MAJOR,
        B_MAJOR
    };
   
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

    // Use this for initialization
    void Start () {
        GenerateProblem();
	}

	// Update is called once per frame
	void Update () {
		
	}

    //key = C
    /*
        1 = C
        2 = D
        3 = E

    key = D
        1 = D
        2 = E
    */
    
    //C D E F G A B

    void GenerateProblem() {
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
               currNote == PianoKey.notes.G && keySignature.Contains(PianoKey.notes.Gsharp)){

                continue;
            }
            

            basicNotes.Add(currNote);           
        }



        int keyStartingNote = (int)key;

        List<PianoKey.notes> tonicNotes = new List<PianoKey.notes>();

        for (int i = 0; i < harmonicFunctionCords[(int)harmoicFunctions.TONIC].Count; i++) {

            int cordStartingNote = (keyStartingNote + harmonicFunctionCords[(int)harmoicFunctions.TONIC][i] - 1) % basicNotes.Count;

            for (int j = 0; j < 3; j++) {

                if(!tonicNotes.Contains(basicNotes[(cordStartingNote + j * 2) % basicNotes.Count])) { 
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
    }
}
