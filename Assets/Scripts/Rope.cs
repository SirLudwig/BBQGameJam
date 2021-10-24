#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float Length
    {
        get { return length; }
        set { length = value > 0 ? value : 0;}
    }

    public float DistanceBetweenNodes
    {
        get => distanceBetweenNodes;
        set => distanceBetweenNodes = value > 0 ? value : 0;
    }

    public float RollSpeed
    {
        get => rollSpeed;
        set => rollSpeed = value > 0 ? value : 0;
    }

    [SerializeField] private GameObject ropePart;
    [SerializeField] bool autoAdjust;
    [Min(1)]
    [SerializeField] private float length;
    [SerializeField] private float distanceBetweenNodes;
    [SerializeField] private float rollSpeed;

    [SerializeField] private Rigidbody2D objectA;
    [SerializeField] private Rigidbody2D objectB;

    public List<DistanceJoint2D> nodes = new List<DistanceJoint2D>();
    private LineRenderer lineRenderer;
    private int nodeAmount;
    private bool wasGenerated = false;
    private bool isRolling = false;
    

    void Start()
    {
        if(autoAdjust)
        {
            length = Vector2.Distance(objectA.transform.position, objectB.transform.position);
        }
        nodeAmount = (int)(length / distanceBetweenNodes);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = nodeAmount;
        lineRenderer.startWidth = 1.3f;
        lineRenderer.endWidth = 1.3f;

        Generate();

        Connect(objectA, objectB);
    }

    void Update()
    {
        if (isRolling == false)
        {
            if ((int)(length / distanceBetweenNodes) < nodes.Count)
            {
                StartCoroutine(RollUp());
            }
            if ((int)(length / distanceBetweenNodes) > nodes.Count)
            {
                StartCoroutine(RollDown());
            }
        }
        UpdateLineRenderer();
    }

    public void Generate()
    {
        if (wasGenerated == true)
        {
            return;
        }

        DistanceJoint2D firstNode = Instantiate(ropePart, transform).GetComponent<DistanceJoint2D>();
        firstNode.transform.position = new Vector3(0, 0, 0);

        firstNode.autoConfigureDistance = false;
        firstNode.distance = distanceBetweenNodes;
        firstNode.maxDistanceOnly = true;
        firstNode.transform.position = objectA.transform.position;

        nodes.Add(firstNode);

        Vector2 direction = objectB.transform.position - objectA.transform.position;

        for (int i = 1; i < nodeAmount; i++)
        {
            DistanceJoint2D node = Instantiate(ropePart, transform).GetComponent<DistanceJoint2D>();
            node.transform.position = new Vector3(objectA.transform.position.x + distanceBetweenNodes * i * direction.x / direction.magnitude, objectA.transform.position.y + distanceBetweenNodes * i * direction.y / direction.magnitude, 0);
            node.connectedBody = nodes[i - 1].GetComponent<Rigidbody2D>();
            node.connectedBody = nodes[i - 1].GetComponent<Rigidbody2D>();
            node.autoConfigureDistance = false;
            node.distance = distanceBetweenNodes;
            node.maxDistanceOnly = true;
            nodes.Add(node);
        }
        wasGenerated = true;
    }

    public void Connect(Rigidbody2D bodyA, Rigidbody2D bodyB)
    {
        objectA = bodyA;
        objectB = bodyB;

        Generate();

        nodes[0].connectedBody = objectA.GetComponent<Rigidbody2D>();
        objectB.gameObject.AddComponent<DistanceJoint2D>();
        objectB.GetComponent<DistanceJoint2D>().connectedBody = nodes[nodes.Count - 1].GetComponent<Rigidbody2D>();
        objectB.GetComponent<DistanceJoint2D>().autoConfigureDistance = false;
        objectB.GetComponent<DistanceJoint2D>().distance = 0.1f;
        objectB.GetComponent<DistanceJoint2D>().maxDistanceOnly = true;
        if (Vector3.Distance(objectA.transform.position, objectB.transform.position) > nodeAmount * distanceBetweenNodes)
        {
            objectB.transform.position = nodes[nodes.Count - 1].transform.position;
        }
        else
        {
            nodes[nodes.Count - 1].transform.position = objectB.transform.position;
        }
    }

    private void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(0, objectA.transform.position);
        for (int i = 1; i < nodes.Count; i++)
        {
            lineRenderer.SetPosition(i, nodes[i].transform.position);
        }
    }

    private IEnumerator RollUp()
    {
        isRolling = true;
        while ((int)(length / distanceBetweenNodes) < nodes.Count)
        {
            if(nodes.Count == 2)
                break;
            Destroy(nodes[0].gameObject);
            nodes.Remove(nodes[0]);
            nodes[0].connectedBody = objectA;
            nodes[0].transform.position = objectA.transform.position;
            lineRenderer.positionCount = nodes.Count;
            UpdateLineRenderer();
            yield return new WaitForSeconds(1/rollSpeed);
        }
        isRolling = false;
        yield break;
    }

    private IEnumerator RollDown()
    {
        isRolling = true;
        while ((int)(length / distanceBetweenNodes) > nodes.Count)
        {
            DistanceJoint2D node = Instantiate(ropePart, transform).GetComponent<DistanceJoint2D>();
            node.autoConfigureDistance = false;
            node.distance = distanceBetweenNodes;
            node.maxDistanceOnly = true;
            node.transform.position = objectA.transform.position;
            node.connectedBody = objectA;
            nodes.Insert(0, node);
            nodes[1].connectedBody = nodes[0].GetComponent<Rigidbody2D>();
            lineRenderer.positionCount = nodes.Count;
            UpdateLineRenderer();
            yield return new WaitForSeconds(1/rollSpeed);
        }
        isRolling = false;
        yield break;
    }
}
