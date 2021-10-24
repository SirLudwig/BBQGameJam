using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    public List<FishController> fishInNet = new List<FishController>();

    public Rigidbody2D rbA;

    [SerializeField]
    private Rigidbody2D rbB;

    [SerializeField]
    private Rope rope;

    [SerializeField]
    private PolygonCollider2D collider;

    public float carriedWeight;

    public float PullingSpeed { get; set; }

    private void Update()
    {
        rbA.velocity = new Vector2(0, PullingSpeed);
        rbB.velocity = new Vector2(0, PullingSpeed);

        Vector2[] points = new Vector2[rope.nodes.Count+1];

        for(int i = 0; i < rope.nodes.Count; i++)
        {
            points[i] = (rope.nodes[i].transform.position);
        }
        points[points.Length-1] = (rope.nodes[0].transform.position);

        collider.points = points;
    }

    public float GetTotalMass()
    {
        float totalMass = 0f;
        foreach(FishController fish in fishInNet)
        {
            totalMass += fish._stats.Weight;
        }

        return totalMass;
    }

    public Vector2 GetPosition()
    {
        return rbA.transform.position;
    }

    public void DisableCollisions()
    {
        foreach(var node in rope.nodes)
        {
            node.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    public void EnableCollisions()
    {
        foreach (var node in rope.nodes)
        {
            node.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fish")
        {
            collision.GetComponent<FishController>().isInNet = true;
            fishInNet.Add(collision.GetComponent<FishController>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Fish")
        {
            collision.GetComponent<FishController>().isInNet = false;
            fishInNet.Remove(collision.GetComponent<FishController>());
        }
    }
}
