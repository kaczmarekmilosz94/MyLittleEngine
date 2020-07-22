using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLittleRendererV8;

namespace MyLittleEngineV8
{
    public class Transform : Component
    {

        private Vector3 _position;
        public Vector3 position { get => _position; set { SetPosition(value); _position = value; } }

        private Vector3 _rotation;
        public Vector3 rotation { get => _rotation; set { SetRotation(value); _rotation = value; } }

        private Vector3 _scale;
        public Vector3 scale { get => _scale; set { SetScale(value); _scale = value; } }

        internal Model model;
        internal Camera camera;

        public Transform()
        {
            _position = Vector3.Zero;
            _rotation = Vector3.Zero;
            _scale = new Vector3(1,1,1);
        }

        public void Translate(Vector3 direct, bool byLocal = false)
        {
            if (byLocal)            
                direct = direct.RotateAroundPoint(_rotation * -0.0174532925f, Vector3.Zero);

            _position += direct;

            if (gameObject.GetComponent<BoxColliderComponent>() != null)
            {
                gameObject.GetComponent<BoxColliderComponent>().boxCollider.SetPosition(_position);

                if (gameObject.GetComponent<BoxColliderComponent>().boxCollider.OnPositionChange())
                {
                    _position -= direct;
                    return;
                }
            }

            if (model != null)
            {
                model.Move(direct);
            }

            if (camera != null)
            {
                camera.Move(direct);
            }
        }

        public void RotateAroundPoint(Vector3 point, Vector3 eulerAngles)
        {
            Vector3 newPos = _position.RotateAroundPoint(eulerAngles * 0.0174532925f, point);
            SetPosition(newPos);
            _position = newPos;
        }

        public void Rotate(Vector3 eulerAngles)
        {
            SetPosition(Vector3.Zero);
            Vector3 temp = _position;
            _position = Vector3.Zero;

            _rotation += eulerAngles;

            if (model != null)
                model.Rotate(eulerAngles);
            if (camera != null)
                camera.eulerAngles += eulerAngles * 0.0174532925f;

            Translate(temp);
            _position = temp;
        }

        public void ChangeScale(Vector3 scale)
        {
            _scale.x = scale.x;
            _scale.y = scale.y;
            _scale.z = scale.z;

            //model = this.gameObject.GetComponent<MeshRenderer>().Model;
            if (model != null) model.Scale(scale);
        }



        private void SetPosition(Vector3 targetPosition)
        {
            if (model != null)            
                model.Move(targetPosition - _position);
            if (camera != null)
                camera.Move(targetPosition - _position);
        }
        private void SetRotation(Vector3 targetRotation)
        {
            SetPosition(Vector3.Zero);
            Vector3 temp = _position;
            _position = Vector3.Zero;

            if (model != null)
                model.Rotate(targetRotation - _rotation);
            if (camera != null)
                camera.eulerAngles +=(targetRotation - _rotation) * 0.0174532925f; 

            Translate(temp);
            _position = temp;
        }
        private void SetScale(Vector3 dscale)
        {
            //model = this.gameObject.GetComponent<MeshRenderer>().Model;
            if (model != null)
                model.Scale(dscale - _scale);
        }
    }
}
