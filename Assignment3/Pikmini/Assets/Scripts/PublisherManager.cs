using System;
using UnityEngine;
using Pikmini;
// change name to Publisher Manager
public class PublisherManager : MonoBehaviour
{

    public int GroupCount { get; } = 3;
    private IPublisher Group1Publisher = new Publisher();
    private IPublisher Group2Publisher = new Publisher();
    private IPublisher Group3Publisher = new Publisher();



    public void SendMessageWithPublisher(int group, Vector3 destination)
    {

        switch (group)
        {
            case 1:
                Group1Publisher.Notify(destination);
                break;
            case 2:
                Group2Publisher.Notify(destination);
                break;
            case 3:
                Group3Publisher.Notify(destination);
                break;
        }
    }
    public void Register(int group, Action<Vector3> callback)
    {
        switch (group)
        {
            case 1:
                Group1Publisher.Register(callback);
                break;
            case 2:
                Group2Publisher.Register(callback);
                break;
            case 3:
                Group3Publisher.Register(callback);
                break;
        }
    }
    public void Unregister(int group, Action<Vector3> callback)
    {
        switch (group)
        {
            case 1:
                Group1Publisher.Unregister(callback);
                break;
            case 2:
                Group2Publisher.Unregister(callback);
                break;
            case 3:
                Group3Publisher.Unregister(callback);
                break;
        }
    }
}
