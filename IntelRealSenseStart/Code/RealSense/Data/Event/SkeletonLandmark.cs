using JointType = PXCMPersonTrackingData.PersonJoints.JointType;
 
namespace IntelRealSenseStart.Code.RealSense.Data.Event
{
    public enum SkeletonLandmark
    {
        // The body trunk joints: head, neck, shoulder, spine and hip.
        HEAD = JointType.JOINT_HEAD,
        NECK = JointType.JOINT_NECK,
        SHOULDER_LEFT = JointType.JOINT_SHOULDER_LEFT,
        SHOULDER_RIGHT = JointType.JOINT_SHOULDER_RIGHT,
        SPINE_BASE = JointType.JOINT_SPINE_BASE,
        SPINE_MID = JointType.JOINT_SPINE_MID,
        SPINE_SHOULDER = JointType.JOINT_SPINE_SHOULDER,
        HIP_LEFT = JointType.JOINT_HIP_LEFT,
        HIP_RIGHT = JointType.JOINT_HIP_RIGHT,
    
        // The arm joints: wrist and elbow.
        WRIST_LEFT = JointType.JOINT_WRIST_LEFT,
        WRIST_RIGHT = JointType.JOINT_WRIST_RIGHT,
        ELBOW_LEFT = JointType.JOINT_ELBOW_LEFT,
        ELBOW_RIGHT = JointType.JOINT_ELBOW_RIGHT,

        // The hand joints: palm center, finger tip and thumb.
        HAND_LEFT = JointType.JOINT_HAND_LEFT,
        HAND_RIGHT = JointType.JOINT_HAND_RIGHT,
        HAND_TIP_LEFT = JointType.JOINT_HAND_TIP_LEFT,
        HAND_TIP_RIGHT = JointType.JOINT_HAND_TIP_RIGHT,
        THUMB_LEFT = JointType.JOINT_THUMB_LEFT,
        THUMB_RIGHT = JointType.JOINT_THUMB_RIGHT,

        // The foot joints: knee, ankle and foot.
        KNEE_LEFT = JointType.JOINT_KNEE_LEFT,
        KNEE_RIGHT = JointType.JOINT_KNEE_RIGHT,
        ANKLE_LEFT = JointType.JOINT_ANKLE_LEFT,
        ANKLE_RIGHT = JointType.JOINT_ANKLE_RIGHT,
        FOOT_LEFT = JointType.JOINT_FOOT_LEFT,
        FOOT_RIGHT = JointType.JOINT_FOOT_RIGHT
    }
}