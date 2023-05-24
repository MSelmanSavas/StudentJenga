using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJengaBrick
{
    public Transform GetTransform();

    public JengaBrick SetStartPosition(Vector3 pos);
    public Vector3 GetStartPosition();
    public JengaBrick SetStartRotation(Vector3 rotation);
    public Vector3 GetStartRotation();

    public JengaBrick SetStartPositionAndCurrentPosition(Vector3 pos);
    public JengaBrick SetStartRotationAndCurrentRotation(Vector3 rotation);

    public JengaBrick SetSubjectInfo(SubjectInfo info);
    public SubjectInfo GetSubjectInfo();

    public JengaBrick ApplyMaterial(Material material);
    public JengaBrick ApplyPhysicsMaterial(PhysicMaterial physicMaterial);
    public Rigidbody GetRigidbody();

    void SetJengaStacker(IJengaStacker stacker);
    IJengaStacker GetJengaStacker();

}
