using UnityEngine;

namespace InventorySystemShowcase
{
    public class Item : MonoBehaviour
    {
        public InventoryItemScriptableObjectData ItemData;

        private Rigidbody _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void ResetItemAndSetItsPosition(Vector3 position)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;

            transform.position = position;
        }
    }
}
