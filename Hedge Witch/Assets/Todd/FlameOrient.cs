using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FlameOrient : MonoBehaviour {
    private Camera mainCam;
    // Start is called before the first frame update
    void Start() {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        if (mainCam != null) {
            mainCam = view.camera;
            Vector3 camPos = mainCam.transform.position;
            camPos.y = transform.position.y;
            transform.LookAt(camPos);
        }
    }
}
