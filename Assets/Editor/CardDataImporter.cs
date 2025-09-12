using UnityEngine;
using UnityEditor; // Required for Editor scripts
using System.IO;   // Required for file operations
using System.Collections.Generic; // Required for List

public class CardDataImporter : EditorWindow
{
    private TextAsset csvFile;
    private string cardDataPath = "Assets/Data/Cards/";

    [MenuItem("Tools/Card Data Importer")]
    public static void ShowWindow()
    {
        GetWindow<CardDataImporter>("Card Data Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Card Data from CSV", EditorStyles.boldLabel);

        csvFile = (TextAsset)EditorGUILayout.ObjectField("CSV File", csvFile, typeof(TextAsset), false);

        cardDataPath = EditorGUILayout.TextField("Card Data Save Path", cardDataPath);
        
        if (GUILayout.Button("Import Cards from CSV"))
        {
            if (csvFile == null)
            {
                EditorUtility.DisplayDialog("Error", "Please assign a CSV file.", "OK");
                return;
            }

            if (!Directory.Exists(cardDataPath))
            {
                Directory.CreateDirectory(cardDataPath);
                AssetDatabase.Refresh(); // Refresh to show the new folder in Unity
            }

            ImportCards();
        }
    }

    private void ImportCards()
    {
        string[] lines = csvFile.text.Split('\n');

        // Skip header row
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] fields = ParseCsvLine(line);

            // Now expecting 6 fields: cardID, cardValue, cardMultiplier, cardLocalWord, cardTranslation, questions
            if (fields.Length < 6)
            {
                Debug.LogWarning($"Skipping malformed line {i + 1}: {line}");
                continue;
            }

            CardData newCardData = ScriptableObject.CreateInstance<CardData>();

            try
            {
                newCardData.cardID = int.Parse(fields[0]);
                newCardData.cardValue = float.Parse(fields[1], System.Globalization.CultureInfo.InvariantCulture); // Use InvariantCulture for consistent float parsing
                newCardData.cardMultiplier = float.Parse(fields[2], System.Globalization.CultureInfo.InvariantCulture);
                newCardData.cardLocalWord = fields[3];
                newCardData.cardTranslation = fields[4];
                
                // Parse questions (assuming semicolon as delimiter)
                newCardData.questions = new List<string>();
                string questionsString = fields[5]; // Now fields[5] is questions
                if (!string.IsNullOrEmpty(questionsString))
                {
                    string[] rawQuestions = questionsString.Split(';');
                    foreach (string q in rawQuestions)
                    {
                        newCardData.questions.Add(q.Trim());
                    }
                }

                // Create the asset
                string assetPath = Path.Combine(cardDataPath, $"Card_{newCardData.cardLocalWord}.asset");
                AssetDatabase.CreateAsset(newCardData, assetPath);
                Debug.Log($"Created CardData: {newCardData.cardLocalWord} at {assetPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error importing card from line {i + 1}: {line}\nError: {e.Message}");
                DestroyImmediate(newCardData); // Clean up partially created asset
            }
        }

        AssetDatabase.SaveAssets(); // Save all created assets
        AssetDatabase.Refresh();    // Refresh the project window to show new assets
        EditorUtility.DisplayDialog("Import Complete", $"Finished importing cards from {csvFile.name}.", "OK");
    }

    // A simple CSV line parser that handles quoted fields (commas inside quotes)
    private string[] ParseCsvLine(string line)
    {
        List<string> fields = new List<string>();
        StringReader reader = new StringReader(line);
        string currentField = "";
        bool inQuote = false;

        while (reader.Peek() != -1)
        {
            char c = (char)reader.Read();

            if (c == '"')
            {
                inQuote = !inQuote;
            }
            else if (c == ',' && !inQuote)
            {
                fields.Add(currentField.Trim());
                currentField = "";
            }
            else
            {
                currentField += c;
            }
        }
        fields.Add(currentField.Trim()); // Add the last field

        return fields.ToArray();
    }
}