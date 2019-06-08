
using UnityEngine;
using UnityEngine.AI;

public class MiniController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private ColorBind ColorBindings;
    private PublisherManager PublisherManager;
    [SerializeField]
    private ColorWatcher ColorWatcher;

    private float UpdateTime;
    private float Throttle;
    private float DeleteTime;
    private float DeleteThrottle;
    private int GroupID = 1;
    
    void Awake()
    {
        this.PublisherManager = GameObject.FindGameObjectWithTag("Script Home").GetComponent<PublisherManager>();
        this.RandomizeBody();
        this.RandomizeThrottle();
        this.GroupID = Random.Range(1, 4);
        this.DeleteThrottle = Random.Range(10f, 40f);
        this.PublisherManager.Register(GroupID, OnMoveMessage);

        switch (this.GroupID)
        {
            case 1:
                ColorWatcher = new ColorWatcher(ColorBindings.GetGroup1Color, this.ChangeColor);
                break;
            case 2:
                ColorWatcher = new ColorWatcher(ColorBindings.GetGroup2Color, this.ChangeColor);
                break;
            case 3:
                ColorWatcher = new ColorWatcher(ColorBindings.GetGroup3Color, this.ChangeColor);
                break;
        }
    }

    private void Update()
    {
        this.UpdateTime += Time.deltaTime;
        if (this.Throttle < this.UpdateTime)
        { 
            ColorWatcher.Watch();
            this.UpdateTime = 0;
        }
        this.DeleteTime += Time.deltaTime;
        if (this.DeleteTime >= this.DeleteThrottle)
        {
            this.PublisherManager.Unregister(this.GroupID, OnMoveMessage);
            Destroy(this.gameObject);

        }
    }

    void OnMouseDown()
    {
        this.PublisherManager.Unregister(GroupID, OnMoveMessage);
        this.GroupID = (this.GroupID % this.PublisherManager.GroupCount) + 1;
        this.PublisherManager.Register(GroupID, OnMoveMessage);
    }
    
    private void ChangeColor(Color color)
    {
        foreach (Transform child in transform)
        {
            Renderer rend = child.GetComponent<Renderer>();
            rend.material.SetColor("_Color", color);
        }
    }

    private void RandomizeBody()
    {
        var ranScale = Random.Range(0.5f, 1.0f);
        foreach (Transform child in transform)
        {
            child.localScale *= ranScale;
        }
    }

    private void RandomizeThrottle()
    {
        Throttle = Random.Range(0.0f, 10.0f);   
    }

    public void OnMoveMessage(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}