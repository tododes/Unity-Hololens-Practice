using UnityEngine;
using System.Collections;

public class TapToPlaceParent : MonoBehaviour {
    private bool placing = false;

    void OnSelect(){
        placing = !placing;
        SpatialMapping.Instance.DrawVisualMeshes = placing;
    }

    void Update(){
        if (placing){
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;
            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo, 30f, SpatialMapping.PhysicsRaycastMask)){
                transform.position = hitInfo.point;
                Quaternion toQuad = Camera.main.transform.localRotation;
                toQuad.x = toQuad.z = 0;
                transform.parent.rotation = toQuad;
            }
        }
    }
}
