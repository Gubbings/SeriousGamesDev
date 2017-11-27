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

    public GameObject sharpPrefab;

    public GameObject[] sharps;


    AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        GenerateProblem();
	}

	// Update is called once per frame
	void Update () {
		
	}

    void GenerateProblem() {

        for (int i = 0; i < 5; i++) {
            sharps[i].SetActive(false);
        }


        keys key = (keys)Random.Range(0, 6);
        List<PianoKey.notes> keySignature = keySignatures[(int)key];

        for (int i = 0; i < keySignature.Count; i++) {
            sharps[i].SetActive(true);
        }

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



        //generate the actual problem of 8 notes
        PianoKey.notes[] problemNotes = new PianoKey.notes[8];

        problemNotes[0] = tonicNotes[Random.Range(0, 2)];
        problemNotes[1] = predominantNotes[Random.Range(0, predominantNotes.Count - 1)];
        problemNotes[2] = dominantNotes[Random.Range(0, dominantNotes.Count - 1)];
        problemNotes[3] = tonicNotes[Random.Range(0, tonicNotes.Count - 1)];

        problemNotes[4] = tonicNotes[Random.Range(0, tonicNotes.Count - 1)];
        problemNotes[5] = predominantNotes[Random.Range(0, predominantNotes.Count - 1)];
        problemNotes[6] = dominantNotes[Random.Range(0, dominantNotes.Count - 1)];
        problemNotes[7] = tonicNotes[Random.Range(0, 2)];

        /*
        problemNotes = new PianoKey.notes[3];
        problemNotes[0] = PianoKey.notes.E;
        problemNotes[1] = PianoKey.notes.A;
        problemNotes[2] = PianoKey.notes.B;
        */

        //IEnumerator routine = playProblem(problemNotes);
        //StartCoroutine(routine);
    }

    public IEnumerator playProblem(PianoKey.notes[] problem) {

        for (int i = 0; i < problem.Length; i++) {
            audioSource.pitch = Mathf.Pow(2, (calcPitch((int)problem[i])) / 12.0f);
            audioSource.Play();
            yield return new WaitForSeconds(1);
        }        
    }

    private float calcPitch(int note) {       

        float offset = 0;

        for (int i = (int)PianoKey.notes.C; i < note; i++) {
            offset += 1f;
        }

        offset /= 12f;

        return Mathf.Pow(2, offset);
    }
}
