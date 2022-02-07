using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static Color GetColorByResourceType(ResourceTypes type)
    {
        switch(type)
        {
            case ResourceTypes.Resource1:
                return Color.red;
            case ResourceTypes.Resource2:
                return Color.yellow;
            case ResourceTypes.Resource3:
                return Color.blue;
            default:
                return Color.gray;
        }
    }
}

public enum FactoryStates
{
    Stopped,
    Starting,
    Producing,
    Finished,
    PausedStorageFull,
    PausedLowRequiredResources,
    PausedLowResStorageFull
}

public enum TranslationDirections
{
    ToPlayer,
    ToFactory
}

public enum TranslationStates
{
    NotStarted,
    InProgress,
    Finished,
    Aborting,
    Aborted
}