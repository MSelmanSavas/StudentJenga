using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface ISubjectsLoader
{
    List<SubjectInfo> GetAllSubjectInfos();
    Task<bool> TryLoadSubjects(string apiURI);
    bool Sort(ISubjectsSorter sorter);
    bool AreSubjectInfosLoaded();
}

public interface ISubjectsLoaderPerGrade : ISubjectsLoader
{
    Dictionary<string, List<SubjectInfo>> GetSubjectInfosPerGrade();

    List<string> GetGrades();
    bool TryGetSubjectInfosForGrade(string grade, List<SubjectInfo> subjectInfos);
}
