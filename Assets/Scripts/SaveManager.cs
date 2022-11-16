using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public static class SaveManager
{
    private static void saveScoreData(int[] scores, string[] users) {
        PlayerData playerdata = new PlayerData(scores, users);
        string dataPach = Application.persistentDataPath + "/data.save";

        FileStream fileStream = new FileStream(dataPach, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, playerdata);
        fileStream.Close();
    }

    private static PlayerData loadData() {
        string dataPach = Application.persistentDataPath + "/data.save";
        FileStream fileStream;
        BinaryFormatter binaryFormatter;

        if (!File.Exists(dataPach)) {
            int[] scores = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] users = { "AAAAA", "AAAAA", "AAAAA", "AAAAA", "AAAAA", "AAAAA", "AAAAA", "AAAAA", "AAAAA", "AAAAA" };
            PlayerData playerdata = new PlayerData(scores, users);
            fileStream = new FileStream(dataPach, FileMode.Create);
            binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, playerdata);
            fileStream.Close();
        }

        fileStream = new FileStream(dataPach, FileMode.Open);
        binaryFormatter = new BinaryFormatter();
        PlayerData playerData = (PlayerData) binaryFormatter.Deserialize(fileStream);
        fileStream.Close();
        return playerData;
    }

    public static void obtenerScores(TMP_Text textbox) {
        PlayerData playerData = loadData();
        string retorno="";
        for (int i = 0; i < 10; i++) {
            retorno += (i+1) + "# " + playerData.users[i] + " -> " + playerData.scores[i] + "\n";
        }

        textbox.text = retorno;
    }

    public static void RegistrarScore(int score, string namePlayer) {
        PlayerData playerData = loadData();
        // arrays temporales
        int[] tempScores = new int[11];
        string[] tempUsers = new string[11];
        for (int i = 0; i < 10; i++) {
            tempScores[i] = playerData.scores[i];
            tempUsers[i] = playerData.users[i];
        }
        tempScores[10] = score;
        tempUsers[10] = namePlayer;

        //ordeno el array de mayor a menor
        int auxScore; string auxName;
        for (int i = 0; i < tempScores.Length-1; i++) {
            for (int j = i+1 ; j < tempScores.Length; j++) {
                if(tempScores[i]> tempScores[j]) {
                    auxScore = tempScores[i];
                    tempScores[i] = tempScores[j];
                    tempScores[j] = auxScore;

                    auxName = tempUsers[i];
                    tempUsers[i] = tempUsers[j];
                    tempUsers[j] = auxName;
                }
            }
        }

        //sobreescribo los datos de playerData
        for (int i = playerData.scores.Length-1; i >= 0; i--) {
            playerData.scores[(playerData.scores.Length - 1)-i]=tempScores[i+1];
            playerData.users[(playerData.scores.Length - 1) - i]=tempUsers[i+1];
        }

        for (int i = 0; i < 10; i++) {
            Debug.Log((i + 1) + "# " + playerData.users[i] + " -> " + playerData.scores[i] );
        }

        //guardo en el archivo 
        saveScoreData(playerData.scores, playerData.users);
    }
}
