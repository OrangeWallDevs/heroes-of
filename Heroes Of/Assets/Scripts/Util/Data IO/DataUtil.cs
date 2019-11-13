using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

        var assets = new List<T>();

        searchInFolders = searchInFolders ?? new[] { "" };
        Debug.Log($"searchInFolders: {searchInFolders}");
        foreach (string folder in searchInFolders) {
            foreach (T foundAsset in LoadAssetsHelper<T>(filter, folder)) {
                assets.Add(foundAsset);
            }                
        }

        return assets;
    }

    IEnumerable<T> LoadAssetsHelper<T>(string filter, string folder) where T : UnityEngine.Object {
        string path = $"{folder}{(folder == "" ? folder : "/")}{filter ?? ""}";
        Object[] foundAssets = Resources.LoadAll(path);

        foreach (Object foundAsset in foundAssets) {
            if (!(foundAsset is null)) {
                if (typeof(T) == typeof(GameObject)) {
                    yield return foundAsset as T;
                } else if (foundAsset is GameObject && typeof(Component).IsAssignableFrom(typeof(T))) {
                    yield return (foundAsset as GameObject).GetComponent<T>();
                } else {
                    yield return null;
                }
            }
        }
    }

}
