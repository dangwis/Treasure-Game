using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float cameraDist;
    public float cameraRotation;
    public float cameraHeight;
    public float cameraWidth;

    Transform cameraPivot;
    Transform cameraHolder;


    void Start()
    {
        cameraPivot = new GameObject("CameraPivot").transform;
        cameraPivot.transform.parent = this.transform;
        cameraPivot.transform.localPosition = Vector3.zero;

        cameraHolder = new GameObject("CameraHolder").transform;
        cameraHolder.parent = cameraPivot;
        cameraHolder.localPosition = Vector3.back * cameraDist;

        Camera.main.transform.position = cameraHolder.position;
        Camera.main.transform.rotation = cameraHolder.rotation;
        
    }

    void Update()
    {
        cameraPivot.transform.position = new Vector3(cameraPivot.transform.position.x, cameraPivot.transform.position.y, 0);
        MoveRealCamera();
    }
    void LateUpdate()
    {
        cameraPivot.transform.position = new Vector3(cameraPivot.transform.position.x, cameraPivot.transform.position.y, 0);
    }

    void MoveRealCamera()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, cameraHolder.position);
        if (distance <= 0)
            return;
        float moveDistance = 5 * distance * Time.deltaTime;
        float fraction = moveDistance / distance;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraHolder.position, fraction);
    }
}
