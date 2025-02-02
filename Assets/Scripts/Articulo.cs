using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum PlaneType
{
    horizontal = 0,
    vertical = 1
}

public enum category
{
    Electrodomesticos = 0,
    muebles = 1,
    decorativos = 2
}

[CreateAssetMenu]
public class Articulo : ScriptableObject
{
    public int id;
    public string ArtName;
    public Sprite ArtImage;
    public category category;
    public PlaneType Type;
    public string ArtDescription;
    public GameObject Art3DModel;
}
