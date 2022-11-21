//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnablePointer : MonoBehaviour
//{
//    public FastMovementManager fastMovementManagerScript;
//    public GameObject pointer;
//    CursorCircleTracking cursorCircleTrackingScript;

//    float recentreZoneColliderDelay = 2;
//    bool delayFinished = false;
//    // Start is called before the first frame update
//    void Start()
//    {
//        fastMovementManagerScript.EnablePointer();
//        //fastMovementManagerScript.DisableRecentreZoneCollider();
//        fastMovementManagerScript.EnableRecenterZone();
//        cursorCircleTrackingScript = pointer.GetComponent<CursorCircleTracking>();
//        fastMovementManagerScript.SetCanUserProgressToNextStageWithTrigger(false);
//    }

//    void Update()
//    {
//        if (!delayFinished)
//        {
//            recentreZoneColliderDelay -= Time.deltaTime;
//            if(recentreZoneColliderDelay < 0)
//            {
//               // fastMovementManagerScript.EnableRecentreZoneCollider();
//                delayFinished = true;
//            }
//        } else
//        {
//            if (cursorCircleTrackingScript.IsCursorOverCube && delayFinished)
//            {
//                fastMovementManagerScript.FastMovementSceneNextStage();
//            }
//        }

//    }
//}
