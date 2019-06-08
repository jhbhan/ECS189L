using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obscura
{
    public class PositionLockCameraController : AbstractCameraController
    {
        private Camera ManagedCamera;
        private LineRenderer CameraLineRenderer;

        private void Awake()
        {
            this.ManagedCamera = this.gameObject.GetComponent<Camera>();
            this.CameraLineRenderer = this.gameObject.GetComponent<LineRenderer>();
        }

        //Use the LateUpdate message to avoid setting the camera's position before
        //GameObject locations are finalized.
        void LateUpdate()
        {
            var targetPosition = this.Target.transform.position;
            var cameraPosition = this.ManagedCamera.transform.position;

            cameraPosition = new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z);

            this.ManagedCamera.transform.position = cameraPosition;

            if (this.DrawLogic)
            {
                this.CameraLineRenderer.enabled = true;
                this.DrawCameraLogic();
            }
            else
            {
                this.CameraLineRenderer.enabled = false;
            }
        }

        public override void DrawCameraLogic()
        {
            this.CameraLineRenderer.positionCount = 5;
            this.CameraLineRenderer.useWorldSpace = false;
            this.CameraLineRenderer.SetPosition(0, new Vector3(0,5,85));
            this.CameraLineRenderer.SetPosition(1, new Vector3(0, -5, 85));
            this.CameraLineRenderer.SetPosition(2, new Vector3(0, 0, 85));
            this.CameraLineRenderer.SetPosition(3, new Vector3(-5, 0, 85));
            this.CameraLineRenderer.SetPosition(4, new Vector3(5, 0, 85));
        }
    }
}
