using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obscura
{
    public class PositionLockLerpCameraController : AbstractCameraController
    {
        private Camera ManagedCamera;
        private LineRenderer CameraLineRenderer;
        [SerializeField] private float LerpDuration;
        private float time;
        private Vector3 lerpPosition;
        private Vector3 lerpCamPostion;

        private void Awake()
        {
            this.ManagedCamera = this.gameObject.GetComponent<Camera>();
            this.CameraLineRenderer = this.gameObject.GetComponent<LineRenderer>();
            this.lerpPosition = this.Target.transform.position;
            this.lerpCamPostion = this.Target.transform.position;
        }

        //Use the LateUpdate message to avoid setting the camera's position before
        //GameObject locations are finalized.

        private void Start()
        {
            this.ManagedCamera.transform.position = new Vector3(this.Target.transform.position.x, this.Target.transform.position.y,this.ManagedCamera.transform.position.z);
        }

        void LateUpdate()
        {
            var targetPosition = this.Target.transform.position;
            var cameraPosition = this.ManagedCamera.transform.position;
            time += Time.deltaTime/this.LerpDuration;
            transform.position = Vector3.Lerp(lerpCamPostion, lerpPosition, time);
            var direction = this.Target.GetComponent<PlayerController>().GetMovementDirection();

            if (direction != new Vector3(0, 0, 0))
            {
                lerpPosition = targetPosition;
                lerpCamPostion = cameraPosition;
                time = 0;
            }

           

            this.ManagedCamera.transform.position = new Vector3(transform.position.x, transform.position.y, cameraPosition.z);

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
