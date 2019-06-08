using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obscura
{
    public class TargetFocusLerpCameraController : AbstractCameraController
    {
        private Camera ManagedCamera;
        private LineRenderer CameraLineRenderer;
        [SerializeField] private float LerpDuration;
        [SerializeField] private float LeadSpeed;
        private float time;
        private Vector3 lerpPosition;
        private Vector3 lerpCamPostion;
        private bool start;

        private void Awake()
        {
            this.ManagedCamera = this.gameObject.GetComponent<Camera>();
            this.CameraLineRenderer = this.gameObject.GetComponent<LineRenderer>();
            this.lerpPosition = new Vector3(this.Target.transform.position.x, this.Target.transform.position.y, this.ManagedCamera.transform.position.z);
        }

        //Use the LateUpdate message to avoid setting the camera's position before
        //GameObject locations are finalized.

        private void Start()
        {
             this.lerpCamPostion = this.ManagedCamera.transform.position;
        }

        void LateUpdate()
        {
            if (start)
            {
                this.ManagedCamera.transform.position = new Vector3(this.Target.transform.position.x, this.Target.transform.position.y, this.ManagedCamera.transform.position.z);
                start = false;
            }
            var targetPosition = this.Target.transform.position;
            var cameraPosition = this.ManagedCamera.transform.position;

            var playerDirection = this.Target.GetComponent<PlayerController>().GetMovementDirection();
            var playerSpeed = this.Target.GetComponent<PlayerController>().GetCurrentSpeed();

            this.ManagedCamera.transform.Translate(playerDirection * Time.deltaTime * (playerSpeed*LeadSpeed));

            if (playerDirection == new Vector3(0, 0, 0))
            {
                time += Time.deltaTime / this.LerpDuration;
                transform.position = Vector3.Lerp(lerpCamPostion, lerpPosition, time);
            }

            if (playerDirection != new Vector3(0, 0, 0))
            {
                lerpPosition = new Vector3(targetPosition.x, targetPosition.y, cameraPosition.z);
                lerpCamPostion = cameraPosition;
                time = 0;
            }

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
            this.CameraLineRenderer.SetPosition(0, new Vector3(0, 5, 85));
            this.CameraLineRenderer.SetPosition(1, new Vector3(0, -5, 85));
            this.CameraLineRenderer.SetPosition(2, new Vector3(0, 0, 85));
            this.CameraLineRenderer.SetPosition(3, new Vector3(-5, 0, 85));
            this.CameraLineRenderer.SetPosition(4, new Vector3(5, 0, 85));
        }
    }
}
