
using UnityEngine;

public class AssetFilter : Entity {
    
    string namTable;
    string txtAssetFilter;
    string txtAssetPath;

    public string NamTable { get => namTable; set => namTable = value; }
    public string TxtAssetFilter { get => txtAssetFilter; set => txtAssetFilter = value; }
    public string TxtAssetPath { get => txtAssetPath; set => txtAssetPath = value; }
    
    public AssetFilter(GameObject gameObject) : base(gameObject) {

    }
    
}
