
using System.Collections.Generic;

public class SubjectInfosPerGrade
{
    public string Grade { get; private set; }
    public List<SubjectInfo> SubjectInfos = new();

    public SubjectInfosPerGrade(string grade)
    {
        Grade = grade;
    }
}