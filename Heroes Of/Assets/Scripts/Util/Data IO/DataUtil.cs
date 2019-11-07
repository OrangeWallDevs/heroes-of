using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUtil : Singleton<DataUtil> {

    private DataUtil() {}

    public T LoadAsset<T>() where T : UnityEngine.Object {
        return LoadAsset<T>(string.Empty, null);
    }

    public T LoadAsset<T>(string filter) where T : UnityEngine.Object {
        return LoadAsset<T>(filter, null);
    }

    public T LoadAsset<T>(string[] searchInFolders) where T : UnityEngine.Object {
        return LoadAsset<T>(string.Empty, searchInFolders);
    }

    public T LoadAsset<T>(string filter, string[] searchInFolders) where T : UnityEngine.Object {
        List<T> foundAssets = LoadAssets<T>(filter, searchInFolders);
        
        return foundAssets.Count != 0 ? foundAssets[0] : null;
    }

    public List<T> LoadAssets<T>() where T : UnityEngine.Object {
        return LoadAssets<T>(string.Empty, null);
    }

    public List<T> LoadAssets<T>(string filter) where T : UnityEngine.Object {
        return LoadAssets<T>(filter, null);
    }

    public List<T> LoadAssets<T>(string[] searchInFolders) where T : UnityEngine.Object {
        return LoadAssets<T>(string.Empty, searchInFolders);
    }        

    public List<T> LoadAssets<T>(string filter, string[] searchInFolders)
            where T : UnityEngine.Object {
        // string[] results = searchInFolders != null ? AssetDatabase.FindAssets(filter, searchInFolders)
        //                                            : AssetDatabase.FindAssets(filter);
        var assets = new List<T>();

        // foreach (string result in results) {
        //     string path = AssetDatabase.GUIDToAssetPath(result);
        //     T asset = AssetDatabase.LoadAssetAtPath<T>(path);

        //     if (asset != null) {
        //         assets.Add(asset);
        //     }
        // }

        return assets;
    }

}
