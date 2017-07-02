using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour {

    public static GazeGestureManager instance { get; private set; }

    public GameObject FocusedObject { get; private set; }
    private GestureRecognizer recognizer;

	// Use this for initialization
	void Start () {
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>{
            if (FocusedObject != null){
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };
        recognizer.StartCapturingGestures();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject oldObject = FocusedObject;
        var position = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;
        RaycastHit hitInfo;
        if(Physics.Raycast(position, gazeDirection, out hitInfo)){
            FocusedObject = hitInfo.collider.gameObject;
        }
        else{
            FocusedObject = null;
        }

        if(oldObject != FocusedObject){
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}
