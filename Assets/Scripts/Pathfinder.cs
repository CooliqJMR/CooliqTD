using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] WayPoint startPoint, endPoint;

    Dictionary<Vector2Int, WayPoint> grid = new Dictionary<Vector2Int, WayPoint>(); // ������� ������� �� ������ ����(Vector2Int) � ��������(WayPoint).

    Queue<WayPoint> queue = new Queue<WayPoint>();

    bool isRunning = true;

    WayPoint searchPoint;

    List<WayPoint> path = new List<WayPoint>();

    Vector2Int[] directions =      // ������� ������ �� �������� ��� ����������� �������� ������ � ��������� ������ ����.
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };           

    /// <summary>
    /// ����� �������� �� ������ ����.
    /// </summary>
    /// <returns>WayPoint</returns>
    public List<WayPoint> GetPath()
    {
        if (path.Count == 0) //�������� ������ �� ���� �������� ��� ���. ��������� ��� ���� ����� ������� ���� 1 ���.
        {
            LoadBlocks();

            PathfindAlgorithm();

            CreatePath();
        }

        return path;
    }

    /// <summary>
    /// ����� ��� �������� ���� �� ������ exploredFrom.
    /// </summary>
    private void CreatePath()
    {
        path.Add(endPoint);

        endPoint.isPlaceable = false; // ��������� �������� ����, ����� ������ ���� ������� ����� �� ���.

        WayPoint prevPoint = endPoint.exploredFrom; // ���� � ������������ ������� �������� � ���������� exploredFrom ������� � endPoint.

        while (prevPoint != startPoint)
        {
            path.Add(prevPoint); // �������� � ���� �������� ����� ����.

            prevPoint.isPlaceable = false; // ��������� ����� ������� ������ ����, ����� �� ��� ������ ���� ������� �����.

            prevPoint = prevPoint.exploredFrom; // ������������� �������� ����� ��� ���������� � ����.
        }

        path.Add(startPoint); // ��������� ������ ����.
        startPoint.isPlaceable = false; 
        path.Reverse(); // ������������� ������� ���� �� startPoint �� endPoint.
    }

    /// <summary>
    /// ���������� ��������� ������ ���� � ������.
    /// </summary>
    private void PathfindAlgorithm() 
    {
        queue.Enqueue(startPoint);
        
        while (queue.Count > 0 && isRunning == true)
        {
            searchPoint = queue.Dequeue(); // ������� ������ �� ������� ����� ��������.
            searchPoint.isExplored = true;    // ����� ��� ������ ���������.
            CheckForEndPoint();
            ExploreNearPoints();
        }
    }
    /// <summary>
    /// ����� ��������� �������� �� ������ �������
    /// </summary>
    private void CheckForEndPoint() // ����� ��������� �������� �� ������� ������ ��������.
    {
        if (searchPoint == endPoint)
        {
            print("�����");
            isRunning = false;
        }
    }
    /// <summary>
    /// ����� ������� ������� �������� ������.
    /// </summary>
    private void ExploreNearPoints()
    {
        if (!isRunning) 
        {
            return; 
        }

        foreach (var direction in directions)
        {
            Vector2Int nearPointCoordinates = searchPoint.GetGritPos() + direction;  /* ������� ������� �������� ������ �������� �������.
                                                                                        �������� startPoint(2, 1) + Vector2Int.up(0, 1) */

            if (grid.ContainsKey(nearPointCoordinates))   // ��������� ���� �� ����� ������ �� ����� � ���� ���� ���������.
            {
                WayPoint nearPoint = grid[nearPointCoordinates];
                AddPointToQueue(nearPoint);
            }
        }
    }
    /// <summary>
    /// ����� ��������� �������� ������ � ������� ��� ��������.
    /// </summary>
    /// <param name="nearPoint"></param>
    private void AddPointToQueue(WayPoint nearPoint)
    {
        if (nearPoint.isExplored || queue.Contains(nearPoint))  // ��������� ������ ���� �� ��� ��� � �������. ���� ������ �� ����, �� ���������.
        {
            return;
        }
        else
        {
            queue.Enqueue(nearPoint); // ��������� �������� ������ � �������.
            nearPoint.exploredFrom = searchPoint;
        }
    }
    /// <summary>
    /// ����� ��������� ��� ����� � �������.
    /// </summary>
    private void LoadBlocks()
    {
        var wayPoints = FindObjectsOfType<WayPoint>(); // ������� ��� ������� � ����� WayPoint � ������� � ������

        foreach(var wayPoint in wayPoints)
        {
            Vector2Int gridPos = wayPoint.GetGritPos();

            if (grid.ContainsKey(gridPos)) // �������� �� �������� ������� � ������� �� �����.
            {
                Debug.LogWarning($"���� ����������� {wayPoint}"); //���� ������ ����������� ������� �������������� � �������.
            }
            else
            {
                grid.Add(gridPos, wayPoint); // ��������� ������ ������ � �������(Dictionary).
            }
        }
    }

    //void SetColorStartAndEnd()
    //{
    //    startPoint.SetTopColor(Color.green);
    //    endPoint.SetTopColor(Color.red);
    //}
}
