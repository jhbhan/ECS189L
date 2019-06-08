using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obscura
{
    public class FourWaySpeedupPushZoneCameraController : AbstractCameraController
    {
        [SerializeField] private Vector2 TopLeft;
        [SerializeField] private Vector2 BottomRight;
        [SerializeField] private float PushRatio;
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
            var playerDirection = this.Target.GetComponent<PlayerController>().GetMovementDirection();
            var playerSpeed = this.Target.GetComponent<PlayerController>().GetCurrentSpeed();
            var multiplier = Time.deltaTime * playerSpeed * PushRatio;

            cameraPosition = new Vector3(cameraPosition.x + (playerDirection.x * multiplier), cameraPosition.y + (playerDirection.y * multiplier), cameraPosition.z);

            if (targetPosition.y >= cameraPosition.y + TopLeft.y)
            {
                cameraPosition = new Vector3(cameraPosition.x, targetPosition.y - TopLeft.y, cameraPosition.z);
            }
            if (targetPosition.y <= cameraPosition.y + BottomRight.y)
            {
                cameraPosition = new Vector3(cameraPosition.x, targetPosition.y - BottomRight.y, cameraPosition.z);
            }
            if (targetPosition.x >= cameraPosition.x + BottomRight.x)
            {
                cameraPosition = new Vector3(targetPosition.x - BottomRight.x, cameraPosition.y, cameraPosition.z);
            }
            if (targetPosition.x <= cameraPosition.x + TopLeft.x)
            {
                cameraPosition = new Vector3(targetPosition.x - TopLeft.x, cameraPosition.y, cameraPosition.z);
            }

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
