using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCameraController : MonoBehaviour
{
    Vector3 _orbitCenterPosition;

    [SerializeField]
    Vector3 _orbitCenterLookAtOffset = new Vector3(0f, 5f, 0f);

    [SerializeField]
    float _orbitSpeed;

    [SerializeField]
    Vector2 _orbitDistanceMinMax;

    [SerializeField]
    float _scrollSpeed = 3f;

    Vector2 _previousMousePosition;
    Vector2 _currentMousePosition;

    void LateUpdate()
    {
        TryUpdateOrbitAngle();
        TryUpdateOrbitDistance();
    }

    void TryUpdateOrbitAngle()
    {
        TryUpdateOrbitLookPosition();
        TryUpdateOrbitStartPosition();
        TryUpdateOrbitCurrentPosition();

        void TryUpdateOrbitLookPosition()
        {
            transform.LookAt(_orbitCenterPosition + _orbitCenterLookAtOffset, Vector3.up);
        }

        void TryUpdateOrbitStartPosition()
        {
            if (!Input.GetKeyDown(KeyCode.Mouse0))
                return;

            _previousMousePosition = Input.mousePosition;
            _currentMousePosition = _previousMousePosition;
        }

        void TryUpdateOrbitCurrentPosition()
        {
            if (!Input.GetKey(KeyCode.Mouse0))
                return;

            _currentMousePosition = Input.mousePosition;

            float xDifference = _currentMousePosition.x - _previousMousePosition.x;

            transform.RotateAround(_orbitCenterPosition, Vector3.up, xDifference * _orbitSpeed);

            _previousMousePosition = _currentMousePosition;
        }
    }



    void TryUpdateOrbitDistance()
    {
        Vector2 scrollAmount = -Input.mouseScrollDelta;

        scrollAmount *= _scrollSpeed;

        float currentDistance = Vector3.Distance(_orbitCenterPosition, transform.position);
        float newDistance = currentDistance + scrollAmount.y;

        newDistance = Mathf.Clamp(newDistance, _orbitDistanceMinMax.x, _orbitDistanceMinMax.y);

        Vector3 distanceUnitVector = transform.position - _orbitCenterPosition;
        distanceUnitVector.Normalize();

        transform.position = _orbitCenterPosition + (distanceUnitVector * newDistance);
    }

    void UpdateOrbitCenterPosition(Vector3 newOrbitCenter)
    {
        _orbitCenterPosition = newOrbitCenter;
    }

    public void OnJengaBrickClicked(IJengaBrick jengaBrick)
    {
        Vector3 newOrbitPosition = jengaBrick.GetJengaStacker().GetStackPosition();
        Vector3 positionDifference = newOrbitPosition - _orbitCenterPosition;

        transform.position += positionDifference;
        _orbitCenterPosition = newOrbitPosition;
    }
}
