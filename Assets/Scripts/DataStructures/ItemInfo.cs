using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Key,
    Note,
}

[System.Serializable]
public class ItemInfo
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Data { get; private set; }
    public string MetaInfo { get; private set; }
    public ItemType Type { get; private set; }
}
