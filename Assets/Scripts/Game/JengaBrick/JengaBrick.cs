using System.Collections;
using System.Collections.Generic;
using SimpleAtoms.Events;
using UnityEngine;
using UnityEngine.EventSystems;

public class JengaBrick : MonoBehaviour, IJengaBrick, IPointerClickHandler
{
    Vector3 _startPosition;
    Vector3 _startRotation;
    SubjectInfo _subjectInfo = new();

    [SerializeField]
    MeshRenderer _meshRenderer;

    [SerializeField]
    Rigidbody _rigidBody;

    [SerializeField]
    Collider _collider;

    [SerializeField]
    IJengaBrickEvent _onJengaBrickClicked;

    IJengaStacker _connectedStacker;

    public JengaBrick SetStartPosition(Vector3 pos)
    {
        _startPosition = pos;
        return this;
    }

    public Vector3 GetStartPosition() => _startPosition;


    public JengaBrick SetStartRotation(Vector3 rotation)
    {
        _startRotation = rotation;
        return this;
    }

    public Vector3 GetStartRotation() => _startRotation;


    public JengaBrick SetStartPositionAndCurrentPosition(Vector3 pos)
    {
        transform.position = pos;
        SetStartPosition(pos);
        return this;
    }

    public JengaBrick SetStartRotationAndCurrentRotation(Vector3 rotation)
    {
        transform.rotation = Quaternion.Euler(rotation);
        SetStartRotation(rotation);
        return this;
    }

    public JengaBrick SetSubjectInfo(SubjectInfo info)
    {
        _subjectInfo.CopyFrom(info);
        return this;
    }

    public SubjectInfo GetSubjectInfo() => _subjectInfo;


    public JengaBrick ApplyMaterial(Material material)
    {
        _meshRenderer.material = material;
        return this;
    }

    public JengaBrick ApplyPhysicsMaterial(PhysicMaterial physicMaterial)
    {
        _collider.material = physicMaterial;
        return this;
    }

    public Rigidbody GetRigidbody() => _rigidBody;
    public Transform GetTransform() => transform;

    public void SetJengaStacker(IJengaStacker stacker) => _connectedStacker = stacker;

    public IJengaStacker GetJengaStacker() => _connectedStacker;

    public void OnPointerClick(PointerEventData eventData)
    {
        _onJengaBrickClicked?.Raise(this);
    }

}
