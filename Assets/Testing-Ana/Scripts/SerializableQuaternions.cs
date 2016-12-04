using UnityEngine;


//--- SERIALIZING QUATERNIONS
// for more information on structs, go here: https://msdn.microsoft.com/en-us/library/0taef578.aspx

[System.Serializable]
public struct SerializableQuaternions
{
    //quaternions need x, y, z, and w elements.
    public float x;
    public float y;
    public float z;
    public float w;

    //now we have a public function.
    public SerializableQuaternions(float rX, float rY, float rZ, float rW)
    {
        x = rX;
        y = rY;
        z = rZ;
        w = rW;
    }

    //now let's return a string representation of the Quaternion 
    public override string ToString()
    {
        return string.Format("[{0},{1},{2},{3}]", x, y, z, w);
    }

    //and this is to automatically (implicitly) convert from SerializeQuaternion to Quaternion.
    public static implicit operator Quaternion(SerializableQuaternions rValue)
    {
        return new Quaternion(rValue.x, rValue.y, rValue.z, rValue.w);
    }

    //automatically convert from Quaternion to SerializeQuaternion.
    public static implicit operator SerializableQuaternions(Quaternion rValue)
    {
        return new SerializableQuaternions(rValue.x, rValue.y, rValue.z, rValue.w);
    }

    
}

