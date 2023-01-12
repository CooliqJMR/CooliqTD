using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] WayPoint startPoint, endPoint;

    Dictionary<Vector2Int, WayPoint> grid = new Dictionary<Vector2Int, WayPoint>(); // Создаем словарь из блоков Ключ(Vector2Int) и Значение(WayPoint).

    Queue<WayPoint> queue = new Queue<WayPoint>();

    bool isRunning = true;

    WayPoint searchPoint;

    List<WayPoint> path = new List<WayPoint>();

    Vector2Int[] directions =      // Создаем массив из векторов для обнаружения соседних клеток в алгоритме поиска пути.
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };           

    /// <summary>
    /// Метод отвечает за запрос пути.
    /// </summary>
    /// <returns>WayPoint</returns>
    public List<WayPoint> GetPath()
    {
        if (path.Count == 0) //Проверка создан ли путь движения или нет. Требуется для того чтобы создать путь 1 раз.
        {
            LoadBlocks();

            PathfindAlgorithm();

            CreatePath();
        }

        return path;
    }

    /// <summary>
    /// Метод для создания пути по точкам exploredFrom.
    /// </summary>
    private void CreatePath()
    {
        path.Add(endPoint);

        endPoint.isPlaceable = false; // Выключаем конечный блок, чтобы нельзя было строить башню на нем.

        WayPoint prevPoint = endPoint.exploredFrom; // Путь с координатами которые хранятся в переменной exploredFrom начиная с endPoint.

        while (prevPoint != startPoint)
        {
            path.Add(prevPoint); // Добавить в лист следущую точку пути.

            prevPoint.isPlaceable = false; // Выключаем блоки которые строят путь, чтобы на них нельзя было строить башни.

            prevPoint = prevPoint.exploredFrom; // Переназначаем следущую точку для добавления в лист.
        }

        path.Add(startPoint); // Добавляем начало пути.
        startPoint.isPlaceable = false; 
        path.Reverse(); // Разворачиваем готовый путь от startPoint до endPoint.
    }

    /// <summary>
    /// Реализация алгоритма поиска пути в ширину.
    /// </summary>
    private void PathfindAlgorithm() 
    {
        queue.Enqueue(startPoint);
        
        while (queue.Count > 0 && isRunning == true)
        {
            searchPoint = queue.Dequeue(); // Удаляем клетку из очереди после проверки.
            searchPoint.isExplored = true;    // Метка что клетка проверена.
            CheckForEndPoint();
            ExploreNearPoints();
        }
    }
    /// <summary>
    /// Метод проверяет является ли клетка финишом
    /// </summary>
    private void CheckForEndPoint() // Метод проверяет является ли текущая клетка финишной.
    {
        if (searchPoint == endPoint)
        {
            print("финиш");
            isRunning = false;
        }
    }
    /// <summary>
    /// Метод находит позицию соседних клеток.
    /// </summary>
    private void ExploreNearPoints()
    {
        if (!isRunning) 
        {
            return; 
        }

        foreach (var direction in directions)
        {
            Vector2Int nearPointCoordinates = searchPoint.GetGritPos() + direction;  /* Находим позицию соседних клеток добавляя вектора.
                                                                                        Например startPoint(2, 1) + Vector2Int.up(0, 1) */

            if (grid.ContainsKey(nearPointCoordinates))   // Проверяем есть ли такая клетка по ключу и если есть добавляем.
            {
                WayPoint nearPoint = grid[nearPointCoordinates];
                AddPointToQueue(nearPoint);
            }
        }
    }
    /// <summary>
    /// Метод добавляет соседнюю клетку в очередь для проверки.
    /// </summary>
    /// <param name="nearPoint"></param>
    private void AddPointToQueue(WayPoint nearPoint)
    {
        if (nearPoint.isExplored || queue.Contains(nearPoint))  // Проверяем клетку была ли она уже в очереди. Если клетки не было, то добавляем.
        {
            return;
        }
        else
        {
            queue.Enqueue(nearPoint); // Добавляем соседние клетки в очередь.
            nearPoint.exploredFrom = searchPoint;
        }
    }
    /// <summary>
    /// Метод добавляет все блоки в словарь.
    /// </summary>
    private void LoadBlocks()
    {
        var wayPoints = FindObjectsOfType<WayPoint>(); // Находим все объекты с типом WayPoint и заносим в массив

        foreach(var wayPoint in wayPoints)
        {
            Vector2Int gridPos = wayPoint.GetGritPos();

            if (grid.ContainsKey(gridPos)) // Проверка на дубликат объекта в словаре по ключу.
            {
                Debug.LogWarning($"Блок повторяется {wayPoint}"); //Если объект повторяется выводим предупреждение в консоль.
            }
            else
            {
                grid.Add(gridPos, wayPoint); // Добавляем каждый объект в словарь(Dictionary).
            }
        }
    }

    //void SetColorStartAndEnd()
    //{
    //    startPoint.SetTopColor(Color.green);
    //    endPoint.SetTopColor(Color.red);
    //}
}
