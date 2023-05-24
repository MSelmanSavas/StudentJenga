using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJengaBrickFactory
{
    bool TrySetJengaInfos(ScriptableJengaPieceInfo jengaPieceInfos);
    bool TryGetJengaBrick(JengaFactoryData data, out IJengaBrick jengaBrick);
    bool TryGetJengaBrickInfo(JengaFactoryData data, out JengaBrickInfo jengaBrick);
}

[System.Serializable]
public struct JengaFactoryData
{
    public MasteryLevel MasteryLevel;
}
