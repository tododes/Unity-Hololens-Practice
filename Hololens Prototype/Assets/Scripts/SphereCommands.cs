using UnityEngine;
using System.Collections;

public class SphereCommands : MonoBehaviour {

    private Vector3 originalPosition;
	// Use this for initialization
	void Start () {
        originalPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnSelect(){
        if (!GetComponent<Rigidbody>()){
            Rigidbody body = gameObject.AddComponent<Rigidbody>();
            body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    void OnReset(){
        Rigidbody body = GetComponent<Rigidbody>();
        if (body)
            DestroyImmediate(body);
        transform.localPosition = originalPosition;
    }

    void OnDrop(){
        OnSelect();
    }
}
