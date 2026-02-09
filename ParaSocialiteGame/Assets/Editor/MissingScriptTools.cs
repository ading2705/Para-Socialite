using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

//This Script is Completely AI - Generated I had console log problems and told GPT to see if there was any missing scripts. 
public static class MissingScriptTools
{
    [MenuItem("Tools/Missing Scripts/Report In Open Scene")]
    private static void ReportInOpenScene()
    {
        List<string> objectsWithMissing = new List<string>();
        int missingCount = CountMissingInActiveScene(objectsWithMissing, remove: false);

        if (missingCount == 0)
        {
            Debug.Log("MissingScriptTools: no missing scripts found in the active scene.");
            return;
        }

        Debug.Log($"MissingScriptTools: found {missingCount} missing script component(s) on {objectsWithMissing.Count} object(s).");
        foreach (string path in objectsWithMissing)
            Debug.Log($"MissingScriptTools: {path}");
    }

    [MenuItem("Tools/Missing Scripts/Remove In Open Scene")]
    private static void RemoveInOpenScene()
    {
        List<string> objectsWithMissing = new List<string>();
        int removedCount = CountMissingInActiveScene(objectsWithMissing, remove: true);

        if (removedCount == 0)
        {
            Debug.Log("MissingScriptTools: no missing scripts found to remove.");
            return;
        }

        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        Debug.Log($"MissingScriptTools: removed {removedCount} missing script component(s) from {objectsWithMissing.Count} object(s).");
    }

    private static int CountMissingInActiveScene(List<string> objectsWithMissing, bool remove)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (!scene.IsValid() || !scene.isLoaded)
            return 0;

        int count = 0;
        foreach (GameObject root in scene.GetRootGameObjects())
            count += Traverse(root.transform, objectsWithMissing, remove);

        return count;
    }

    private static int Traverse(Transform current, List<string> objectsWithMissing, bool remove)
    {
        int count = 0;
        GameObject go = current.gameObject;
        int missingOnObject = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);

        if (missingOnObject > 0)
        {
            count += missingOnObject;
            objectsWithMissing.Add(GetHierarchyPath(current));

            if (remove)
            {
                Undo.RegisterCompleteObjectUndo(go, "Remove Missing Scripts");
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
            }
        }

        for (int i = 0; i < current.childCount; i++)
            count += Traverse(current.GetChild(i), objectsWithMissing, remove);

        return count;
    }

    private static string GetHierarchyPath(Transform transform)
    {
        string path = transform.name;
        Transform parent = transform.parent;

        while (parent != null)
        {
            path = parent.name + "/" + path;
            parent = parent.parent;
        }

        return path;
    }
}
