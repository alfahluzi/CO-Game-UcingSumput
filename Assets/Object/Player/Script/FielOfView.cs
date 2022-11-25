using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FielOfView : MonoBehaviour
{
    public float viewRadius;
    [Tooltip("Derajat luas area yang akan di deteksi")]
    [Range(0, 360)] public float viewAngle;
    [Range(0, 360)] public float maxTargetAngle;
    [Range(0, 360)] public float minTargetAngle;

    [Tooltip("Kalibrasi Sudut pandang Objek")]
    [Range(0, 360)] public float bodyAngle = 0;

    [Range(0, 360)] public float targetAngle;
    public float meshResolution;
    public int edgeResolveIterations;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    [Tooltip("Layer Mask yang akan di deteksi dan masuk kedalam list  visibleTargets")]
    public LayerMask targetMask;

    [Tooltip("Layer Mask yang akan di deteksi sebagai benda yang dapat menghalagi pandangan")]
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    private Transform target;


    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        //StartCoroutine("FindTargetWithDelay", 0.001f);
    }
    void LateUpdate()
    {
        //DrawFieldOfView();
        FindVisibleTargets();
    }

    //IEnumerator FindTargetWithDelay(float delay)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(delay);
    //        FindVisibleTargets();
    //    }
    //}

    //Finds targets inside field of view not blocked by walls
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        target = null;
        //Adds targets in view radius to an array
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        //For every targetsInViewRadius it checks if they are inside the field of view
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            target = targetsInViewRadius[i].transform;
            Vector2 dirToTarget = (target.position - transform.position).normalized;
            targetAngle = -Vector2.Angle(transform.up, dirToTarget);
            maxTargetAngle = bodyAngle + (viewAngle / 2);
            minTargetAngle = bodyAngle - (viewAngle / 2);
            if (maxTargetAngle > 180)
            {
                var sisa = maxTargetAngle -= 180;
                maxTargetAngle = -1 * (180 - sisa);
            }if (minTargetAngle < -180)
            {
                var sisa = minTargetAngle += 180;
                minTargetAngle = (180 + sisa);
            }
            if (targetAngle < maxTargetAngle && Vector2.Angle(transform.up, dirToTarget) > minTargetAngle)
            {
                float dstToTarget = Vector2.Distance(transform.position, target.position);
                //If line draw from object to target is not interrupted by wall, add target to list of visible targets
                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    //void DrawFieldOfView()
    //{
    //    int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
    //    float stepAngleSize = viewAngle / stepCount;
    //    List<Vector2> viewPoints = new List<Vector2>();

    //    ViewCastInfo oldViewCast = new ViewCastInfo();
    //    for (int i = 0; i <= stepCount; i++)
    //    {
    //        float angle = bodyAngle + (transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i);//////
    //        ViewCastInfo newViewCast = ViewCast(angle);
    //        if (i > 0)
    //        {
    //            if (oldViewCast.hit != newViewCast.hit)
    //            {
    //                EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
    //                if (edge.pointA != Vector2.zero)
    //                {
    //                    viewPoints.Add(edge.pointA);
    //                }
    //                if (edge.pointB != Vector2.zero)
    //                {
    //                    viewPoints.Add(edge.pointB);
    //                }
    //            }
    //        }
    //        viewPoints.Add(newViewCast.point);
    //        oldViewCast = newViewCast;
    //    }

    //    int vertexCount = viewPoints.Count + 1;
    //    Vector2[] vertices = new Vector2[vertexCount];
    //    Vector3[] verticesInV3 = new Vector3[vertexCount];
    //    int[] triangles = new int[(vertexCount - 2) * 3];
    //    vertices[0] = Vector2.zero;
    //    verticesInV3[0] = new Vector3(vertices[0].x, vertices[0].y, transform.position.z);
    //    for (int i = 0; i < vertexCount - 1; i++)
    //    {
    //        vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
    //        verticesInV3[i + 1] = new Vector3(vertices[i + 1].x, vertices[i + 1].y, transform.position.z);

    //        if (i < vertexCount - 2)
    //        {
    //            triangles[i * 3] = 0;
    //            triangles[i * 3 + 1] = i + 1;
    //            triangles[i * 3 + 2] = i + 2;
    //        }
    //    }

    //    viewMesh.Clear();
    //    viewMesh.vertices = verticesInV3;
    //    viewMesh.triangles = triangles;
    //    viewMesh.RecalculateNormals();
    //}

    //EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    //{
    //    float minAngle = minViewCast.angle;
    //    float maxAngle = maxViewCast.angle;
    //    Vector2 minPoint = Vector2.zero;
    //    Vector2 maxPoint = Vector2.zero;

    //    for (int i = 0; i < edgeResolveIterations; i++)
    //    {
    //        float angle = (minAngle + maxAngle) / 2;
    //        ViewCastInfo newViewCast = ViewCast(angle);
    //        if (newViewCast.hit == minViewCast.hit)
    //        {
    //            minAngle = angle;
    //            minPoint = newViewCast.point;
    //        }
    //        else
    //        {
    //            maxAngle = angle;
    //            maxPoint = newViewCast.point;
    //        }
    //    }
    //    return new EdgeInfo(minPoint, maxPoint);
    //}
    //ViewCastInfo ViewCast(float globalAngle)
    //{
    //    Vector2 dir = DirFromAngle(globalAngle, true);
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);
    //    if (hit.collider != null)
    //    {
    //        return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
    //    }
    //    else
    //        return new ViewCastInfo(false, new Vector2(transform.position.x, transform.position.y) + dir * viewRadius, viewRadius, globalAngle);
    //}
    //public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    //{
    //    if (!angleIsGlobal)
    //    {
    //        angleInDegrees -= transform.eulerAngles.z;
    //    }
    //    return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    //}

    //public struct ViewCastInfo
    //{
    //    public bool hit;
    //    public Vector2 point;
    //    public float distance, angle;

    //    public ViewCastInfo(bool _hit, Vector2 _point, float _distance, float _angle)
    //    {
    //        hit = _hit;
    //        point = _point;
    //        distance = _distance;
    //        angle = _angle;
    //    }
    //}

    //public struct EdgeInfo
    //{
    //    public Vector2 pointA;
    //    public Vector2 pointB;
    //    public EdgeInfo(Vector2 _pointA, Vector2 _pointB)
    //    {
    //        pointA = _pointA;
    //        pointB = _pointB;
    //    }
    //}

    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.position);
        }
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z));
    }
}
