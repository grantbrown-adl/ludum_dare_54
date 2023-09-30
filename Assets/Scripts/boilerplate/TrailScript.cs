using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrailScript : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f; //lifetime of a point on the trail
    [SerializeField] private float minimumVertexDistance = 0.1f; //minimum distance moved before a new point is solidified.
    [SerializeField] private Vector3 velocity; //direction the points are moving
    [SerializeField] private LineRenderer line;
    //position data
    private List<Vector3> points;
    private Queue<float> spawnTimes = new Queue<float>(); //list of spawn times, to simulate lifetime. Back of the queue is vertex 1, front of the queue is the end of the trail.
                                                          //Length of this queue is one less than the number of positions in the linerenderer, since the linerenderer always has a non-solidified vertex at the object's position.

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
        points = new List<Vector3>() { transform.position }; //indices 1 - end are solidified points, index 0 is alwaays transform.position
        line.SetPositions(points.ToArray());
    }

    void AddPoint(Vector3 position)
    {
        points.Insert(1, position);
        spawnTimes.Enqueue(Time.time);
    }

    void RemovePoint()
    {
        spawnTimes.Dequeue();
        points.RemoveAt(points.Count - 1); //remove corresponding oldest point at the end
    }

    void Update()
    {

        //cull based on lifetime
        while(spawnTimes.Count > 0 && spawnTimes.Peek() + lifetime < Time.time)
        {
            RemovePoint();
        }

        //move positions
        Vector3 diff = -velocity * Time.deltaTime;
        for(int i = 1; i < points.Count; i++)
        {
            points[i] += diff;
        }

        //add new point
        if(points.Count < 2 || Vector3.Distance(transform.position, points[1]) > minimumVertexDistance)
        {
            //if we have no solidified points, or we've moved enough for a new point
            AddPoint(transform.position);
        }

        //update index 0;
        points[0] = transform.position;

        //save result
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }
}
