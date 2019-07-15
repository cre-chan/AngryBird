using UnityEngine;

namespace Utility
{

    public class CamUtility
    {

        //convert the camera's viewport to a world space rect. The camera cannot be null!
        static public Rect GetCamViewRect(Camera cam)
        {
            //assert the cam not being null.
            Debug.Assert(cam != null, "Utility::GetCamViewRect: Cannot transform a null-camera into its rect!");
            var rect = cam.rect;

            var left_bottom = new Vector3(rect.xMin, rect.yMax, 0f);
            var right_top = new Vector3(rect.xMax, rect.yMin, 0f);

            left_bottom = cam.ViewportToWorldPoint(left_bottom);
            right_top = cam.ViewportToWorldPoint(right_top);


            return new Rect(
                left_bottom.x,
                left_bottom.y,
                right_top.x - left_bottom.x,
                right_top.y - left_bottom.y
                ); ;
        }


        //convert a bound to a rect, do not change the cordinate
        static public Rect BoundToRect(Bounds bounds)
        {

            return new Rect(
                bounds.center.x - bounds.extents.x,
                bounds.center.y - bounds.extents.y,
                bounds.size.x,
                bounds.size.y
                );
        }

    }


    //this is a structure that give the constraint content must exist
    public struct Existing<T>
        where T : class
    {
        private T content;//ensures this to be existence


        public Existing(T r)
        {
            if (r == null)
                throw new UnassignedReferenceException("Existence:Null reference detected");
            content = r;
        }

        public T Unwrap()
        {
            return content;
        }

        public Existing<U> To<U>()
            where U : class, T
        {
            return new Existing<U>((U)this.Unwrap());
        }
    }


    public delegate void Action();
    public delegate R Func<R>();

    //The datastructure will only call once. Say, two call will only results in one. can be used in competing situation.
    public class InvokerOnce
    {

        private bool called;

        public InvokerOnce()
        {
            called = false;
        }

        public void call(Action func)
        {
            if (!called)
                func();

            this.called = true;
        }
    }


    public class FnOnce<R>
    {
        //always non-nullable when func not called
        private Func<R> func;

        public FnOnce(Existing<Func<R>> func)
        {
            this.func = func.Unwrap();
        }

        public R call()
        {
            if (func != null)
            {
                var a = func();
                func = null;
                return a;
            }
            else
            {
                throw new UnassignedReferenceException("");
            }
        }
    }

}


