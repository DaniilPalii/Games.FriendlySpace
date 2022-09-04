using UnityEngine;

namespace FriendlySpace.GameObjects
{
    public class Player : MonoBehaviour
    {
        public float walkSpeed = 5;
        public float flightSpeed = 5;
        public float gravityModifier = 30;

        private new Rigidbody2D rigidbody;
        private Vector2? gravityPoint;

        protected void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        protected void Update()
        {
            // var hit = Physics2D.Raycast(transform.position, -transform.up, crawlerRadius, obstacles);
            // Debug.DrawRay(frontPoint, -up, Color.red);

            SetVelocityByInput();
        }

        protected void FixedUpdate()
        {
            AddGravity();
        }

        protected void OnTriggerEnter2D(Collider2D enteredCollider)
        {
            if (enteredCollider.CompareTag(Tags.MagneticField))
                gravityPoint = enteredCollider.transform.position;
        }

        protected void OnTriggerExit2D(Collider2D enteredCollider)
        {
            if (enteredCollider.CompareTag(Tags.MagneticField))
                gravityPoint = null;
        }

        private void SetVelocityByInput()
        {
            rigidbody.velocity = new Vector2(
                Input.GetAxisRaw(Axis.Horizontal) * walkSpeed,
                Input.GetAxisRaw(Axis.Vertical) * walkSpeed);
        }

        private void AddGravity()
        {
            if (gravityPoint == null)
                return;

            var gravityVector = gravityPoint.Value - (Vector2)transform.position;
            gravityVector.Normalize();
            gravityVector *= gravityModifier;
            rigidbody.AddForce(gravityVector);
        }
    }
}