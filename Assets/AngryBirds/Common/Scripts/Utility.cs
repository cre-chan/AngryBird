using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility{

    //convert the camera's viewport to a world space rect. The camera cannot be null!
    static public  Rect GetCamViewRect(Camera cam) {
        var rect = cam.rect;

        var left_bottom = new Vector3(rect.xMin, rect.yMax, 0f);
        var right_top = new Vector3(rect.xMax, rect.yMin, 0f);

        left_bottom = cam.ViewportToWorldPoint(left_bottom);
        right_top = cam.ViewportToWorldPoint(right_top);

       
        return new Rect(
            left_bottom.x,
            left_bottom.y,
            right_top.x - left_bottom.x,
            right_top.y-left_bottom.y
            ); ;
    }

    //convert a bound to a rect, do not change the cordinate
    static public Rect BoundToRect(Bounds bounds) {

        return new Rect(
            bounds.center.x-bounds.extents.x,
            bounds.center.y - bounds.extents.y,
            bounds.size.x,
            bounds.size.y
            );
    }
}
