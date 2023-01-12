using UnityEngine;

[SelectionBase]
public class WayPoint : MonoBehaviour
{ 
    public bool isPlaceable = true; // Отвечает за разрешение установить башню на блоке.
    public bool isExplored = false;
    public WayPoint exploredFrom; // Оставляет след для постраения пути.
    
    Vector2Int gridPos; //Позиция каждого куба;
    const int gridSize = 10; //Константа отвечает за размер сетки.
    

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGritPos()
    {
        return new Vector2Int
            (
            Mathf.RoundToInt(transform.position.x / gridSize), //Округляем до десятых по X.
            Mathf.RoundToInt(transform.position.z / gridSize) //Округляем до десятых по Y.
            );
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                Debug.Log("Нельзя строить");
            }
        }
    }
}
