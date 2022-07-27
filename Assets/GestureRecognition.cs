using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using PDollarGestureRecognizer;
using System.IO;
using UnityEngine.Events;

public class GestureRecognition : MonoBehaviour
{
    // Start is called before the first frame update
    public SteamVR_Action_Boolean gestureRecognition;
    public UnityEngine.XR.XRNode inputSource;
    private Valve.VR.InteractionSystem.Hand hand;
    public float inputThreshold = 0.1f;
    public Transform movementSource;

    public bool creationMode = true;
    public string newGestureName;


    public float recognitionThreshold = 0.8f;
    public float newPositionThresholdDistance = 0.05f;

    [System.Serializable]
    public class UnityStringEvent : UnityEvent<string> { }
    public UnityStringEvent OnRecognized;
    public GameObject debugCubePrefab;

    private List<Gesture> trainingSet = new List<Gesture>();
    private bool isMoving = false;
    private List<Vector3> positionsList = new List<Vector3>();

    private void OnEnable()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();

    }
    // Start is called before the first frame update
    void Start()
    {
        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (var item in gestureFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions.Gesture.StartGesture[hand.handType].state && !isMoving)
        {
            startMovement();
        }
        else if (SteamVR_Actions.Gesture.StartGesture[hand.handType].state && isMoving)
        {
            updateMovement();
        }
        else if (!SteamVR_Actions.Gesture.StartGesture[hand.handType].state && isMoving)
        {
            endMovement();
        }
            

    }

    void startMovement()
    {
        isMoving = true;
        Debug.Log("start mov");
        positionsList.Clear();
        positionsList.Add(movementSource.position);

        if (debugCubePrefab)
            Destroy(Instantiate(debugCubePrefab, movementSource.position, Quaternion.identity), 3);

    }
    void endMovement()
    {
        Debug.Log("end mov");
        isMoving = false;

        //Create gesture from pos list
        Point[] pointArray = new Point[positionsList.Count];
        for (int i = 0; i < positionsList.Count; i++)
        {
            Vector2 screenpoint = Camera.main.WorldToScreenPoint(positionsList[i]);
            pointArray[i] = new Point(screenpoint.x,screenpoint.y,0);
        }

        Gesture newGesture = new Gesture(pointArray);
        if (creationMode)
        {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);
            string fileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
        } else //recognize
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log(result.GestureClass + result.Score);

            if (result.Score > recognitionThreshold)
            {
                OnRecognized.Invoke(result.GestureClass);
            }
            
        }
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
