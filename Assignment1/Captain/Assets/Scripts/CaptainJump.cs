using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Captain.Command;

namespace Captain.Command
{
    public class CaptainJump : ScriptableObject, ICaptainCommand
    {

        private float MAX_Y_AXIS = 7.0f;
        private float jump_height;
        private float jumpForce = 10.0f;
        private BoxCollider2D CaptainCollider;
        private bool isGrounded = true;

        void Update()
        {

        }

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            CaptainCollider = gameObject.GetComponent<BoxCollider2D>();

            if (rigidBody != null)
            {
                if (isGrounded)
                {
                    rigidBody.velocity += Vector2.up * jumpForce;
                    isGrounded = false;

                }
                else {
                    var contacts = new Collider2D[32];
                    this.CaptainCollider.GetContacts(contacts);
                    foreach (var col in contacts)
                    {

                        if (col != null && col.gameObject != null && col.gameObject.tag == "Ground")
                        {
                            this.isGrounded = true;
                        }
                        break;
                    }
                }
            }
        }

    }
}