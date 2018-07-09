using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class SpriteController : MonoBehaviour
    {
        [SerializeField]
        private Transform PlayerCamera;

        private void Start()
        {
            CameraController.onPreCull += (x) => {

                //Transform cam = x.transform;
                //Vector3 lookPoint = /*transform.position -*/ -cam.position;
                //lookPoint.y = cam.position.y;
                //Debug.Log(transform.position -lookPoint  );
                //transform.LookAt(lookPoint);

                Vector3 fwd = x.transform.forward;
                fwd.y = 0;
                transform.rotation = Quaternion.LookRotation(fwd);
            };
        }
    }
}
