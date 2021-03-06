﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class PlaneManager : MonoBehaviour
{
    public ARPlaneManager planemanager;
    public ARRaycastManager raycastmanager;
    

    public GameObject Player;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector2 touchPos = Input.GetTouch(0).position;
                if(raycastmanager.Raycast(touchPos, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose hitpos = hits[0].pose;

                    Instantiate(Player, hitpos.position, hitpos.rotation);
                    return;
                }
            }
        }
        
    }

}
