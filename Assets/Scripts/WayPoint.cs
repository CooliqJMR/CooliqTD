using UnityEngine;

[SelectionBase]
public class WayPoint : MonoBehaviour
{ 
    public bool isPlaceable = true; // �������� �� ���������� ���������� ����� �� �����.
    public bool isExplored = false;
    public WayPoint exploredFrom; // ��������� ���� ��� ���������� ����.
    
    Vector2Int gridPos; //������� ������� ����;
    const int gridSize = 10; //��������� �������� �� ������ �����.
    

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGritPos()
    {
        return new Vector2Int
            (
            Mathf.RoundToInt(transform.position.x / gridSize), //��������� �� ������� �� X.
            Mathf.RoundToInt(transform.position.z / gridSize) //��������� �� ������� �� Y.
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
                Debug.Log("������ �������");
            }
        }
    }
}
