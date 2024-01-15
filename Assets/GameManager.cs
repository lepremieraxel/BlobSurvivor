using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    private string folderPath = Application.dataPath + Path.AltDirectorySeparatorChar + "Metrics";
    private string gameToken;
    private string playerToken;
    private string playerFile = Application.dataPath + Path.AltDirectorySeparatorChar + "player.txt";
    private int tokenLength = 10;
    public long startTimestamp;
    private float timeBetweenSaving = 5f;
    private Transform playerTransform;
    private string serverUrl = "https://bridge.axelmarcial.com";
    private string localUrl = "http://localhost/blob-survivor/csvToSql/";
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        startTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        playerToken = GetPlayerToken();
        gameToken = GetGameToken();
        StartCoroutine(CallFunctionEveryXSeconds());
    }

    private string GetPlayerToken()
    {
        string token;
        if (!File.Exists(playerFile))
        {
            File.Create(playerFile).Dispose();
            token = GenerateToken();
            File.WriteAllText(playerFile, token);
        } else
        {
            token = File.ReadAllText(playerFile);
        }
        return token;
    }

    private string GetGameToken()
    {
        string token;
        if(gameToken == null)
        {
            token = GenerateToken();
        } else
        {
            token = gameToken;
        }
        return token;
    }

    private string GenerateToken()
    {
        string token = "";
        for(int i = 1; i < tokenLength; i++)
        {
            token += UnityEngine.Random.Range(0, tokenLength);
        }
        return token;
    }

    public void SaveEndGame(long start, long end)
    {
        string filePath = folderPath + Path.AltDirectorySeparatorChar + gameToken + "_games.csv";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        string line = 0+";"+playerToken + ";" + gameToken + ";" + start + ";" + end + '\n';
        File.AppendAllText(filePath, line);
    }
    public void SaveEnemyKill(long timestamp, string type, string weapon)
    {
        string filePath = folderPath + Path.AltDirectorySeparatorChar + gameToken + "_enemies-killed.csv";
        if(!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        if(!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        string line = 0+";"+playerToken + ";"+gameToken+";" +timestamp+";" + type + ";" + weapon + '\n';
        File.AppendAllText(filePath, line);
    }

    public void SaveEnemySpawn(long timestamp, string type)
    {
        string filePath = folderPath + Path.AltDirectorySeparatorChar + gameToken + "_enemies-spawned.csv";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        string line = 0+";"+playerToken + ";" + gameToken + ";" + timestamp + ";" + type + '\n';
        File.AppendAllText(filePath, line);
    }

    public void SavePlayerChoice(long timestamp, string choice)
    {
        string filePath = folderPath + Path.AltDirectorySeparatorChar + gameToken + "_player-choices.csv";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        string line = 0+";"+playerToken + ";" + gameToken + ";" + timestamp + ";" + choice + '\n';
        File.AppendAllText(filePath, line);
    }

    public void SavePlayerHealth(long timestamp, int currentHealth, int maxHealth)
    {
        string filePath = folderPath + Path.AltDirectorySeparatorChar + gameToken + "_player-health.csv";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        string line = 0+";"+playerToken + ";" + gameToken + ";" + timestamp + ";" + currentHealth.ToString() + ";" + maxHealth.ToString() + '\n';
        File.AppendAllText(filePath, line);
    }

    public void SavePlayerPowerup(long  timestamp, string type)
    {
        string filePath = folderPath + Path.AltDirectorySeparatorChar + gameToken + "_player-powerups.csv";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        string line = 0+";"+playerToken + ";" + gameToken + ";" + timestamp + ";" + type + '\n';
        File.AppendAllText(filePath, line);
    }

    private void SavePlayerPos(long timestamp, float x, float y)
    {
        string filePath = folderPath + Path.AltDirectorySeparatorChar + gameToken + "_player-pos.csv";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        string line = 0+";"+playerToken + ";" + gameToken + ";" + timestamp + ";" + x + ";" + y + '\n';
        File.AppendAllText(filePath, line);
    }

    public IEnumerator SendMetricsToWebService()
    {
        string preparedPath = folderPath + Path.AltDirectorySeparatorChar + gameToken;
        string[] path = new string[7];
        path[0] = preparedPath + "_games.csv";
        path[1] = preparedPath + "_enemies-killed.csv";
        path[2] = preparedPath + "_enemies-spawned.csv";
        path[3] = preparedPath + "_player-choices.csv";
        path[4] = preparedPath + "_player-health.csv";
        path[5] = preparedPath + "_player-powerups.csv";
        path[6] = preparedPath + "_player-pos.csv";

        UnityWebRequest[] files = new UnityWebRequest[path.Length];
        WWWForm form = new WWWForm();
        for (int i = 0; i < files.Length; i++)
        {
            files[i] = UnityWebRequest.Get(path[i]);
            yield return files[i].SendWebRequest();
            form.AddBinaryData(i.ToString(), files[i].downloadHandler.data, Path.GetFileName(path[i]), "text/csv");
        }

        UnityWebRequest server = UnityWebRequest.Post(serverUrl, form);
        yield return server.SendWebRequest();

        if (server.result == UnityWebRequest.Result.ProtocolError || server.result == UnityWebRequest.Result.ConnectionError) {
            Debug.Log(server.error);
        }
        else
        {
            Debug.Log("Uploaded " + files.Length + " files successfully to remote server.");
        }

        UnityWebRequest local = UnityWebRequest.Post(localUrl, form);
        yield return local.SendWebRequest();

        if (local.result == UnityWebRequest.Result.ProtocolError || local.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(local.error);
        }
        else
        {
            Debug.Log("Uploaded " + files.Length + " files successfully to local server.");
        }
        FileUtil.DeleteFileOrDirectory(folderPath);
    }

    IEnumerator CallFunctionEveryXSeconds()
    {
        while(true)
        {
            SavePlayerPos(DateTimeOffset.Now.ToUnixTimeSeconds(), playerTransform.position.x, playerTransform.position.y);
            yield return new WaitForSeconds(timeBetweenSaving);
        }
    }
}
