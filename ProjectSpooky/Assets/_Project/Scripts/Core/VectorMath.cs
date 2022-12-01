using UnityEngine;

public static class VectorMath
{
    public static Vector3 GetTargetHeading(Transform referenceTransform, Transform targetTransform)
    {
        return targetTransform.position - referenceTransform.position;
    }
    
    public static Vector3 GetTargetHeading(Transform referenceTransform, Vector3 targetPoint)
    {
        return targetPoint - referenceTransform.position;
    }
    
    public static Vector3 GetTargetHeading(Vector3 referencePoint, Vector3 targetPoint)
    {
        return targetPoint - referencePoint;
    }
    
    public static float IsTargetAhead(Vector3 targetHeading, Transform referenceTransform) {

        return AngleDir(referenceTransform.up, targetHeading, referenceTransform.right);
    }

    public static float IsTargetToTheLeftOrRight(Vector3 targetHeading, Transform referenceTransform) {

        return AngleDir(referenceTransform.forward, targetHeading, referenceTransform.up);
    }

    public static float IsTargetAboveOrBelow(Vector3 targetHeading, Transform referenceTransform) {

        return AngleDir(referenceTransform.forward, targetHeading, referenceTransform.right);
    }

    public static bool IsVectorFacingDown(Vector3 vector3)
    {
        return IsVectorFacingSameDirection(vector3, Vector3.down);
    }

    public static bool IsVectorFacingUp(Vector3 vector3)
    {
        return IsVectorFacingSameDirection(vector3, Vector3.up);
    }

    public static bool IsVectorFacingSameDirection(Vector3 vector3, Vector3 direction)
    {
        return Vector3.Dot(vector3, direction) > 0;
    }
    
    public static float AngleBetweenVector2(Vector2 vector1, Vector2 vector2)
    {
        Vector2 vector1Rotated90 = new Vector2(-vector1.y, vector1.x);
        float sign = (Vector2.Dot(vector1Rotated90, vector2) < 0) ? -1.0f : 1.0f;
        return Vector2.Angle(vector1, vector2) * sign;
    }

    private static float AngleDir(Vector3 forwardVector, Vector3 targetHeading, Vector3 upVector) {
        var perp = Vector3.Cross(forwardVector, targetHeading);
        float dir = Vector3.Dot(perp, upVector);

        if (dir > 0f) {
            return 1f;
        }
        if (dir < 0f) {
            return -1f;
        }
        return 0f;
    }
}