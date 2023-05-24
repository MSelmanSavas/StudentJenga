using UnityEngine;

[System.Serializable]
public class SubjectInfo
{
    public int ID;
    public string Subject;
    public string Grade;
    public MasteryLevel Mastery;
    public string DomainID;
    public string Domain;
    public string Cluster;
    public string StandardID;
    public string StandardDescription;

    public void CopyFrom(SubjectInfo otherInfo)
    {
        ID = otherInfo.ID;
        Subject = otherInfo.Subject;
        Grade = otherInfo.Grade;
        Mastery = otherInfo.Mastery;
        DomainID = otherInfo.DomainID;
        Domain = otherInfo.Domain;
        Cluster = otherInfo.Cluster;
        StandardID = otherInfo.StandardID;
        StandardDescription = otherInfo.StandardDescription;
    }
}
