using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.ARCoreExtensions;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class AnchorPlace : MonoBehaviour
{
    public GameObject MarkerPrefab;
    public GameObject Player;

    private ARAnchorManager _arAnchorManager;
    private ARGeospatialAnchor _arGeospatialAnchor;
    private AREarthManager _arEarthManager;

    private void Awake()
    {
        _arAnchorManager = GetComponent<ARAnchorManager>();
        _arGeospatialAnchor = GetComponent<ARGeospatialAnchor>();

        GeospatialPose geospatialPose = _arEarthManager.Convert(new Pose(Player.transform.position, Player.transform.rotation));

        GeoAnchorPlace(37.5398185, 127.1229427, geospatialPose.Altitude, geospatialPose.EunRotation);
        GeoAnchorPlace(37.5397658, 127.1230607, geospatialPose.Altitude, geospatialPose.EunRotation);
        GeoAnchorPlace(37.5397393, 127.1231278, geospatialPose.Altitude, geospatialPose.EunRotation);
    }

    private void GeoAnchorPlace(double latitude, double longitude, double altitude, Quaternion eunRotation)
    {
        if (_arGeospatialAnchor.trackingState == TrackingState.Tracking)
        {
            ARGeospatialAnchor anchor = ARAnchorManagerExtensions.AddAnchor(_arAnchorManager, latitude, longitude, altitude, eunRotation);
            GameObject marker = Instantiate(MarkerPrefab, anchor.transform);
        }
    }
}
