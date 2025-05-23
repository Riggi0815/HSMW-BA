using System;
using System.IO;
using UnityEngine;

public class CSVWriter : MonoBehaviour
{

    public static CSVWriter Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    string fileName = "";
    string logDirectory = "";
    
    public void CreateCSVFile()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        
        // Create a logs directory inside persistentDataPath (this is better than dataPath)
        logDirectory = Path.Combine(Application.persistentDataPath, "Logs");
        
        // Create directory if it doesn't exist
        if (!Directory.Exists(logDirectory))
        {
            try
            {
                Directory.CreateDirectory(logDirectory);
                Debug.Log($"Created log directory at: {logDirectory}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to create log directory: {e.Message}");
                // Fallback to application data path if we can't create the directory
                logDirectory = Application.dataPath;
            }
        }
        
        fileName = Path.Combine(logDirectory, $"LogFile_{timestamp}.csv");
        
        try
        {
            using (TextWriter tw = new StreamWriter(fileName, false))
            {
                tw.WriteLine("WaveCount; EnemyType; EnemyCount; EnemyHealth; EnemySpeed; EnemyAttackDamage; EnemyAttackSpeed; EnemyProjectileSpeed; PlayerLevel;");
            }
            Debug.Log($"CSV log file created at: {fileName}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to create CSV file: {e.Message}");
        }
    }

    public void WriteCSVLine(int waveCount, string enemyType, int enemyCount, float enemyHealth, float enemySpeed, float enemyAttackDamage, float enemyAttackSpeed, float enemyProjectileSpeed, int playerLevel)
    {
        TextWriter tw = new StreamWriter(fileName, true);
        tw.WriteLine($"{waveCount}; {enemyType}; {enemyCount}; {enemyHealth}; {enemySpeed}; {enemyAttackDamage}; {enemyAttackSpeed}; {enemyProjectileSpeed}; {playerLevel};");
        tw.Close();
    }

}
