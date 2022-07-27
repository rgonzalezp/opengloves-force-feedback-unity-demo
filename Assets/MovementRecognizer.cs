using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MovementRecognizer : MonoBehaviour
{
    public XRNode inputSource;
    private SteamVR_Input_Sources hand;
    public float inputThreshold = 0.1f;
    public Transform movementSource;

    public float newPositionThresholdDistance = 0.05f;
    public GameObject debugCubePrefab;

    private bool isMoving = false;
    private List<Vector3> positionsList = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        positionsList.Add(movementSource.position);
    }

    // Update is called once per frame
    void Update()
    {
     
        
        
            updateMovement();
        
    }

    void startMovement()
    {
        isMoving = true;
        Debug.Log("start mov");
        positionsList.Clear();
        positionsList.Add(movementSource.position);

        if(debugCubePrefab)
            Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);

    }
    void endMovement()
    {
        Debug.Log("end mov");
        isMoving = false;
    }

    void updateMovement()
    {
        Debug.Log("updating mov");
        Vector3 lastPosition = positionsList[positionsList.Count - 1];
        if (Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance)
        {
            positionsList.Add(movementSource.position);
            if (debugCubePrefab)
                Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);
        }
    }
}
