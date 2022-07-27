using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PoseDetector : MonoBehaviour
{
    [System.Serializable]
    public struct Gesture
    {
        public string nameGesture;
        public List<Vector3> fingerPos;
    }
    Transform[] bones;
    /// <summary>An array of all the finger proximal joint transforms</summary>
    public Transform[] proximals { get; protected set; }

    /// <summary>An array of all the finger middle joint transforms</summary>
    public Transform[] middles { get; protected set; }

    /// <summary>An array of all the finger distal joint transforms</summary>
    public Transform[] distals { get; protected set; }

    /// <summary>An array of all the finger tip transforms</summary>
    public Transform[] tips { get; protected set; }

    /// <summary>An array of all the finger aux transforms</summary>
    public Transform[] auxs { get; protected set; }

    public Transform root { get { return bones[SteamVR_Skeleton_JointIndexes.root]; } }
    public Transform wrist { get { return bones[SteamVR_Skeleton_JointIndexes.wrist]; } }
    public Transform indexMetacarpal { get { return bones[SteamVR_Skeleton_JointIndexes.indexMetacarpal]; } }
    public Transform indexProximal { get { return bones[SteamVR_Skeleton_JointIndexes.indexProximal]; } }
    public Transform indexMiddle { get { return bones[SteamVR_Skeleton_JointIndexes.indexMiddle]; } }
    public Transform indexDistal { get { return bones[SteamVR_Skeleton_JointIndexes.indexDistal]; } }
    public Transform indexTip { get { return bones[SteamVR_Skeleton_JointIndexes.indexTip]; } }
    public Transform middleMetacarpal { get { return bones[SteamVR_Skeleton_JointIndexes.middleMetacarpal]; } }
    public Transform middleProximal { get { return bones[SteamVR_Skeleton_JointIndexes.middleProximal]; } }
    public Transform middleMiddle { get { return bones[SteamVR_Skeleton_JointIndexes.middleMiddle]; } }
    public Transform middleDistal { get { return bones[SteamVR_Skeleton_JointIndexes.middleDistal]; } }
    public Transform middleTip { get { return bones[SteamVR_Skeleton_JointIndexes.middleTip]; } }
    public Transform pinkyMetacarpal { get { return bones[SteamVR_Skeleton_JointIndexes.pinkyMetacarpal]; } }
    public Transform pinkyProximal { get { return bones[SteamVR_Skeleton_JointIndexes.pinkyProximal]; } }
    public Transform pinkyMiddle { get { return bones[SteamVR_Skeleton_JointIndexes.pinkyMiddle]; } }
    public Transform pinkyDistal { get { return bones[SteamVR_Skeleton_JointIndexes.pinkyDistal]; } }
    public Transform pinkyTip { get { return bones[SteamVR_Skeleton_JointIndexes.pinkyTip]; } }
    public Transform ringMetacarpal { get { return bones[SteamVR_Skeleton_JointIndexes.ringMetacarpal]; } }
    public Transform ringProximal { get { return bones[SteamVR_Skeleton_JointIndexes.ringProximal]; } }
    public Transform ringMiddle { get { return bones[SteamVR_Skeleton_JointIndexes.ringMiddle]; } }
    public Transform ringDistal { get { return bones[SteamVR_Skeleton_JointIndexes.ringDistal]; } }
    public Transform ringTip { get { return bones[SteamVR_Skeleton_JointIndexes.ringTip]; } }
    public Transform thumbMetacarpal { get { return bones[SteamVR_Skeleton_JointIndexes.thumbMetacarpal]; } } //doesn't exist - mapped to proximal
    public Transform thumbProximal { get { return bones[SteamVR_Skeleton_JointIndexes.thumbProximal]; } }
    public Transform thumbMiddle { get { return bones[SteamVR_Skeleton_JointIndexes.thumbMiddle]; } }
    public Transform thumbDistal { get { return bones[SteamVR_Skeleton_JointIndexes.thumbDistal]; } }
    public Transform thumbTip { get { return bones[SteamVR_Skeleton_JointIndexes.thumbTip]; } }
    public Transform thumbAux { get { return bones[SteamVR_Skeleton_JointIndexes.thumbAux]; } }
    public Transform indexAux { get { return bones[SteamVR_Skeleton_JointIndexes.indexAux]; } }
    public Transform middleAux { get { return bones[SteamVR_Skeleton_JointIndexes.middleAux]; } }
    public Transform ringAux { get { return bones[SteamVR_Skeleton_JointIndexes.ringAux]; } }
    public Transform pinkyAux { get { return bones[SteamVR_Skeleton_JointIndexes.pinkyAux]; } }
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {

        SteamVR_Behaviour_Skeleton skeleton = (SteamVR_Behaviour_Skeleton)GameObject.Find("vr_glove_left_model_slim(Clone)").GetComponent("SteamVR_Behaviour_Skeleton");
        Transform root = skeleton.root;
        bones = root.GetComponentsInChildren<Transform>();
        proximals = new Transform[] { thumbProximal, indexProximal, middleProximal, ringProximal, pinkyProximal };
        middles = new Transform[] { thumbMiddle, indexMiddle, middleMiddle, ringMiddle, pinkyMiddle };
        distals = new Transform[] { thumbDistal, indexDistal, middleDistal, ringDistal, pinkyDistal };
        tips = new Transform[] { thumbTip, indexTip, middleTip, ringTip, pinkyTip };
        auxs = new Transform[] { thumbAux, indexAux, middleAux, ringAux, pinkyAux };

        List<Vector3> data = new List<Vector3>();
        foreach (var finger_info in auxs)
        {
            data.Add(root.transform.InverseTransformPoint(finger_info.transform.position));
        }
        int count = 0;
        foreach (var finger_info in data)
        {
           
        }



    }

    void Save()
    {
        Gesture g = new Gesture();
        g.nameGesture = "Closed Fist";
        List<Vector3> data = new List<Vector3>();
        foreach (var finger_info in auxs)
        {
            data.Add(finger_info.transform.InverseTransformPoint(finger_info.transform.position));
        }
        
    }
}
