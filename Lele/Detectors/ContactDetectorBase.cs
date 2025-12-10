using UnityEngine;

public abstract class ContactDetectorBase
{
    protected PlayerController pc;
    public ContactDetectorBase(PlayerController pc)
    {
        this.pc = pc;
    }
   
    public abstract void PerformDetection();
    protected void DrawBoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, Color color)
    {
        float halfWidth = size.x / 2f;
        float halfHeight = size.y / 2f;

        Vector2 topLeft = origin + new Vector2(-halfWidth, halfHeight);
        Vector2 topRight = origin + new Vector2(halfWidth, halfHeight);
        Vector2 bottomLeft = origin + new Vector2(-halfWidth, -halfHeight);
        Vector2 bottomRight = origin + new Vector2(halfWidth, -halfHeight);

        Vector2 endOrigin = origin + direction.normalized * distance;
        Vector2 endTopLeft = endOrigin + new Vector2(-halfWidth, halfHeight);
        Vector2 endTopRight = endOrigin + new Vector2(halfWidth, halfHeight);
        Vector2 endBottomLeft = endOrigin + new Vector2(-halfWidth, -halfHeight);
        Vector2 endBottomRight = endOrigin + new Vector2(halfWidth, -halfHeight);

        Debug.DrawLine(topLeft, topRight, color);
        Debug.DrawLine(topRight, bottomRight, color);
        Debug.DrawLine(bottomRight, bottomLeft, color);
        Debug.DrawLine(bottomLeft, topLeft, color);

        Debug.DrawLine(endTopLeft, endTopRight, color);
        Debug.DrawLine(endTopRight, endBottomRight, color);
        Debug.DrawLine(endBottomRight, endBottomLeft, color);
        Debug.DrawLine(endBottomLeft, endTopLeft, color);

        Debug.DrawLine(topLeft, endTopLeft, color);
        Debug.DrawLine(topRight, endTopRight, color);
        Debug.DrawLine(bottomLeft, endBottomLeft, color);
        Debug.DrawLine(bottomRight, endBottomRight, color);
    }
}
