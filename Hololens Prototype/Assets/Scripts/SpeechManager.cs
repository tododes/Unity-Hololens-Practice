using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour {

    private KeywordRecognizer recognizer;
    private Dictionary<string, System.Action> speechData;
	// Use this for initialization
	void Start () {
        speechData.Add("Drop Sphere", () => {
            GameObject focusedObject = GazeGestureManager.instance.FocusedObject;
            if (focusedObject){
                focusedObject.SendMessage("OnDrop");
            }
        });

        speechData.Add("Reset World", () => {
            BroadcastMessage("OnReset");
        });
        recognizer = new KeywordRecognizer(speechData.Keys.ToArray());
        recognizer.OnPhraseRecognized += OnKeyPhraseRecognized;
        recognizer.Start();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnKeyPhraseRecognized(PhraseRecognizedEventArgs args){
        System.Action action;
        if (speechData.TryGetValue(args.text, out action)){
            action.Invoke();
        }
    }
}
