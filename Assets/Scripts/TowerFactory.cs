using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 4;
    [SerializeField] Tower towerPrefab;

    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(WayPoint baseWayPoint)
    {
        int towerCount = towerQueue.Count;
        if (towerCount < towerLimit)
        {
            InstantiateTower(baseWayPoint);
        }
        else
        {
            MoveTowerNewPosition(baseWayPoint);
        }
    }

    private void InstantiateTower(WayPoint baseWayPoint)
    {
        var newTower = Instantiate(towerPrefab, baseWayPoint.transform.position, Quaternion.identity);
        newTower.transform.parent = transform;
        baseWayPoint.isPlaceable = false; // Если башня стоит на блоке, то на этом блоке больше нельзя строить.
        newTower.baseWayPoint = baseWayPoint;
        towerQueue.Enqueue(newTower);
    }

    private void MoveTowerNewPosition(WayPoint newBaseWayPoint)
    {
        Tower oldTower = towerQueue.Dequeue();

        oldTower.transform.position = newBaseWayPoint.transform.position;
        oldTower.baseWayPoint.isPlaceable = true;
        newBaseWayPoint.isPlaceable = false;
        oldTower.baseWayPoint = newBaseWayPoint;

        towerQueue.Enqueue(oldTower);
    }
}
