using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.ARCoreExtensions;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AnchorPlace : MonoBehaviour
{
    public GameObject MarkerPrefab;
    public GameObject Player;
    public AREarthManager AREarthManager;

    private ARAnchorManager _arAnchorManager;

    public void Create()
    {
        if (AREarthManager.EarthTrackingState != TrackingState.Tracking)
        {
            Debug.Log("Tracking 상태가 아닙니다. 잠시만 기다려주세요.");
        }

        // 사용자 기기와 유사한 고도
        //GeospatialPose geospatialPose = AREarthManager.Convert(new Pose(Player.transform.position, Player.transform.rotation));
        // 카메라를 이용한 고도
        GeospatialPose geospatialPose = AREarthManager.CameraGeospatialPose;

        GeoAnchorPlace(37.5398185, 127.1229427, geospatialPose.Altitude, geospatialPose.EunRotation);
        GeoAnchorPlace(37.5397658, 127.1230607, geospatialPose.Altitude, geospatialPose.EunRotation);
        GeoAnchorPlace(37.5397393, 127.1231278, geospatialPose.Altitude, geospatialPose.EunRotation);
    }

    private void GeoAnchorPlace(double latitude, double longitude, double altitude, Quaternion eunRotation)
    {
        ARGeospatialAnchor anchor = _arAnchorManager.AddAnchor(latitude, longitude, altitude, eunRotation);
        GameObject marker = Instantiate(MarkerPrefab, anchor.transform);
    }
}
