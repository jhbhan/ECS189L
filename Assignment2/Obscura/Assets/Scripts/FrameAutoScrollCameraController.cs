using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obscura
{
    public class FrameAutoScrollCameraController : AbstractCameraController
    {
        [SerializeField] private Vector2 TopLeft;
        [SerializeField] private Vector2 BottomRight;
        private Camera ManagedCamera;
        private LineRenderer CameraLineRenderer;
        [SerializeField] private float AutoScrollSpeed;

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

            if (this.TopLeft.x + cameraPosition.x > targetPosition.x)
            {
                targetPosition.x = this.TopLeft.x + cameraPosition.x;
            }
            if (this.BottomRight.x + cameraPosition.x < targetPosition.x)
            {
                targetPosition.x = this.BottomRight.x + cameraPosition.x;
            }
            if (this.TopLeft.y + cameraPosition.y < targetPosition.y)
            {
                targetPosition.y = cameraPosition.y + this.TopLeft.y;
            }
            if (this.BottomRight.y + cameraPosition.y > targetPosition.y)
            {
                targetPosition.y = cameraPosition.y + this.BottomRight.y;
            }

            targetPosition.x = targetPosition.x + AutoScrollSpeed;

            this.Target.transform.position = targetPosition;

            cameraPosition = new Vector3(cameraPosition.x + AutoScrollSpeed, cameraPosition.y, cameraPosition.z);

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
            this.CameraLineRenderer.SetPosition(0, new Vector3(TopLeft.x, TopLeft.y, 85));
            this.CameraLineRenderer.SetPosition(1, new Vector3(BottomRight.x, TopLeft.y, 85));
            this.CameraLineRenderer.SetPosition(2, new Vector3(BottomRight.x, BottomRight.y, 85));
            this.CameraLineRenderer.SetPosition(3, new Vector3(TopLeft.x, BottomRight.y, 85));
            this.CameraLineRenderer.SetPosition(4, new Vector3(TopLeft.x, TopLeft.y, 85));
        }
    }
}
