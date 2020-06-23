using UnityEngine;
using System;

[Serializable]
public class SerializedVector3
{
    // Class for serializing Vector3's.
    // We store 3 floats, and just Convert back to a Vector3.
    public float x,y,z;

    public SerializedVector3(float X, float Y, float Z)
	{
		x = X;
		y = Y;
		z = Z;
	}
    
    public Vector3 ToVector3()
	{
		return new Vector3(x, y, z);
	}
}