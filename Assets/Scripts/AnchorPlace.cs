using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.ARCoreExtensions;
using UnityEngine.XR.ARFoundation;

public class AnchorPlace : MonoBehaviour
{
    public GameObject MarkerPrefab;
    public GameObject Player;

    private ARAnchorManager _arAnchorManager;
    private bool _isOk = false;

    private void Awake()
    {
        _arAnchorManager = GetComponent<ARAnchorManager>();
    }

    private void Update()
    {
        AREarthManager arEarthManager = new AREarthManager();

        if (!_isOk)
        {
            Debug.Log(Player.transform.position);

            GeospatialPose geospatialPose = arEarthManager.Convert(new Pose(Player.transform.position, Player.transform.rotation));
            
            Debug.Log(geospatialPose);

            GeoAnchorPlace(37.5398185, 127.1229427, geospatialPose.Altitude, geospatialPose.EunRotation);
            GeoAnchorPlace(37.5397658, 127.1230607, geospatialPose.Altitude, geospatialPose.EunRotation);
            GeoAnchorPlace(37.5397393, 127.1231278, geospatialPose.Altitude, geospatialPose.EunRotation);

            _isOk = true;
        }
    }

    private void GeoAnchorPlace(double latitude, double longitude, double altitude, Quaternion eunRotation)
    {
        ARGeospatialAnchor anchor = _arAnchorManager.AddAnchor(latitude, longitude, altitude, eunRotation);
        GameObject marker = Instantiate(MarkerPrefab, anchor.transform);
    }
}
