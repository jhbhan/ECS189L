using System;
using System.Collections;
using System.Collections.Generic;
using Pikmini;
using UnityEngine;

public class Publisher : Pikmini.IPublisher
{
    List<Action<Vector3>> collector;

    public Publisher()
    {
        collector = new List<Action<Vector3>>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void IPublisher.Unregister(Action<Vector3> notifier)
    {
        this.collector.Remove(notifier);
    }

    void IPublisher.Register(Action<Vector3> notifier)
    {
        this.collector.Add(notifier);
    }

    void IPublisher.Notify(Vector3 transform)
    {
        int i = 0;
        while (this.collector.Count > i)
        {
            this.collector[i](transform);
            i++;
        }
    }
}
