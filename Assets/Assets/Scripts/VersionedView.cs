using UnityEngine;
using System.Collections;
using System;

public class VersionedView : MonoBehaviour, IVersioned
{
    ulong cachedVersion = 0;
    ulong version = 0;

    public ulong Version
    {
        get
        {
            return version;
        }

        set
        {
            version = value;
        }
    }

    public virtual void DirtyUpdate()
    {

    }

    public void MarkDirty()
    {
        Version++;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(cachedVersion != Version)
        {
            cachedVersion = version;
            DirtyUpdate();
        }
    }
}
