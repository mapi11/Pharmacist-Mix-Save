using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DropZone : MonoBehaviour, IDropHandler
{
    public List<RectTransform> items = new List<RectTransform>();
    public int maxColumns = 3;
    public float spacing = 10f; // Расстояние между объектами

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform droppedItem = eventData.pointerDrag.GetComponent<RectTransform>();
        if (droppedItem != null)
        {
            items.Add(droppedItem);
            droppedItem.SetParent(transform);
            PlaceItems();
        }
    }

    public void RemoveItem(RectTransform item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
        PlaceItems();
    }

    public void PlaceItems()
    {
        float itemWidth = items.Count > 0 ? items[0].rect.width : 100f; // Ширина элемента
        float itemHeight = items.Count > 0 ? items[0].rect.height : 100f; // Высота элемента
        int rows = Mathf.CeilToInt(items.Count / (float)maxColumns);

        for (int i = 0; i < items.Count; i++)
        {
            int row = i / maxColumns;
            int column = i % maxColumns;
            items[i].anchoredPosition = new Vector2(column * (itemWidth + spacing), -row * (itemHeight + spacing));
        }

        // Центрирование по горизонтали
        float totalWidth = Mathf.Min(items.Count, maxColumns) * (itemWidth + spacing) - spacing;
        float startX = -totalWidth / 2 + itemWidth / 2;

        for (int i = 0; i < items.Count; i++)
        {
            items[i].anchoredPosition += new Vector2(startX, 0);
        }
    }
}
