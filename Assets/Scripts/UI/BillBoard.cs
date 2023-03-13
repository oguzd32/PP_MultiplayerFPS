using UnityEngine;

namespace UI
{
    public class BillBoard : MonoBehaviour
    {
        private Camera _camera;

        private void Update()
        {
            if (_camera == null)
            {
                _camera = FindObjectOfType<Camera>();
            }

            if (_camera == null) return;
        
            transform.LookAt(_camera.transform);
            transform.Rotate(Vector3.up * 180);
        }
    }
}
