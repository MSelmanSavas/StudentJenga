using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJengaStacker
{
    void SetStackPosition(Vector3 position);
    Vector3 GetStackPosition();
    List<IJengaBrick> GetJengaBricks();
    List<SubjectInfo> GetSubjectInfos();
    bool TrySetJengaFactory(IJengaBrickFactory factory);
    bool TrySetSubjectInfo(List<SubjectInfo> subjectInfos);
    bool TrySetConfig(ScriptableJengaStackerConfig config);
    bool TryBuildJengaStack();
}
