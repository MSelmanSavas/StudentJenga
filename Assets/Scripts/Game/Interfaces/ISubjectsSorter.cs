using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubjectsSorter
{
    List<SubjectInfo> Sort(List<SubjectInfo> subjectInfos);
}
