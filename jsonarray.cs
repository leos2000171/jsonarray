using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using UnityEngine;

class JSONarray {
    static public List<T> multiJson<T>(string filenameORjsontext, bool jsontext = false){
        List<T> list = new List<T>();
        string text = "";
        if (!jsontext) {
            string filePath = "";
            filePath = Path.Combine(Application.streamingAssetsPath, filenameORjsontext);
            if (File.Exists(filePath)) {
                text = Regex.Replace(File.ReadAllText(filePath), @"\t|\n|\r", "").Replace("\t", " ");
            } else {
                Debug.LogError("No File Exists.");
                return list;
            }
        } else {
            text = filenameORjsontext;
        }
        if (text.IndexOf(',') == -1 || text.IndexOf('{') == -1) {
            Debug.LogError("No Data Loaded");
        } else {
            while (text.IndexOf('{') != -1) {
                string json = text.Substring(text.IndexOf('{'), text.IndexOf('}'));
                list.Add(JsonUtility.FromJson<T>(json));
                text = text.Substring(text.IndexOf('}') + 1);
            }
            Debug.Log("Loaded " + list.Count + " Objects");
        }
        return list;
    }
}
