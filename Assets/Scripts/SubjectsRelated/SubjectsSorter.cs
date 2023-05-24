using System.Collections.Generic;
using System.Linq;

public class SubjectsSorter : ISubjectsSorter
{
    public List<SubjectInfo> Sort(List<SubjectInfo> subjectInfos)
    {
        subjectInfos = subjectInfos.OrderBy(x => x.Domain).ThenBy(x => x.Cluster).ThenBy(x => x.StandardID).ToList();
        return subjectInfos;
    }
}
