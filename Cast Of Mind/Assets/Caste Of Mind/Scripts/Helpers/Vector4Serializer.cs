using UnityEngine;

[System.Serializable]
public class Vector4Serializer
{
    public float x;
    public float y;
    public float z;
    public float w;

    public Vector4Serializer(Vector3 vec)
    {
        x = vec.x;
        y = vec.y;
        z = vec.z;
        w = 0;
    }

    public Vector4Serializer(Quaternion quaternion)
    {
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }

    public Vector3 GetVector3()
    {
        return new Vector3(x, y, z);
    }

    public Quaternion GetQuaternion()
    {
        return new Quaternion(x, y, z, w);
    }

    public static implicit operator Vector3 (Vector4Serializer v)
    {
        return v.GetVector3();
    }

    public static implicit operator Quaternion(Vector4Serializer v)
    {
        return v.GetQuaternion();
    }
}
