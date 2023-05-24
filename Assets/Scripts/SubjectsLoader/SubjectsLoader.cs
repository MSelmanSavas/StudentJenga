using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class SubjectsLoader : ISubjectsLoaderPerGrade
{
    List<SubjectInfo> _subjectInfos;
    List<string> _grades = new();
    Dictionary<string, List<SubjectInfo>> _subjectInfosPerGrade = new();

    bool _isLoaded = false;
    public bool AreSubjectInfosLoaded() => _isLoaded;

    public List<SubjectInfo> GetAllSubjectInfos() => _subjectInfos;

    public List<string> GetGrades() => _grades;

    public Dictionary<string, List<SubjectInfo>> GetSubjectInfosPerGrade() => _subjectInfosPerGrade;

    public bool Sort(ISubjectsSorter sorter)
    {
        try
        {
            _subjectInfos = sorter.Sort(_subjectInfos);

            foreach (var subjectInfosPerGradeKV in _subjectInfosPerGrade)
            {
                string grade = subjectInfosPerGradeKV.Key;

                List<SubjectInfo> sortedSubjects = sorter.Sort(subjectInfosPerGradeKV.Value);

                _subjectInfosPerGrade[grade].Clear();
                _subjectInfosPerGrade[grade].AddRange(sortedSubjects);
            }

        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error while sorting subject infos! Error : {e}");
            return false;
        }

        return true;
    }

    public bool TryGetSubjectInfosForGrade(string grade, List<SubjectInfo> subjectInfos)
    {
        if (!_subjectInfosPerGrade.ContainsKey(grade))
            return false;

        subjectInfos = _subjectInfosPerGrade[grade];
        return true;
    }

    public async Task<bool> TryLoadSubjects(string apiURI)
    {
        _isLoaded = false;

        var response = await ApiRequestHelper.RequestApiData(apiURI);

        if (string.IsNullOrEmpty(response))
        {
            Debug.LogError("No meaningfull data has been received! Cannot spawn student data...");
            return false;
        }

        try
        {
            _subjectInfos = JsonConvert.DeserializeObject(response, typeof(List<SubjectInfo>)) as List<SubjectInfo>;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error while trying to load student subject data! Aborting spawning student jenga datas... Error : {e}");
            return false;
        }

        if (_subjectInfos == null || _subjectInfos.Count <= 0)
        {
            Debug.LogError($"Student subject datas are empty! Aborting spawning student jenga datas...");
            return false;
        }


        LoadSubjectsPerGrade();
        _isLoaded = true;
        return true;
    }

    void LoadSubjectsPerGrade()
    {
        _grades.Clear();
        _subjectInfosPerGrade.Clear();

        foreach (var subjectInfo in _subjectInfos)
        {
            if (!_subjectInfosPerGrade.ContainsKey(subjectInfo.Grade))
            {
                _grades.Add(subjectInfo.Grade);
                _subjectInfosPerGrade.Add(subjectInfo.Grade, new List<SubjectInfo>());
            }

            _subjectInfosPerGrade[subjectInfo.Grade].Add(subjectInfo);
        }
    }
}
