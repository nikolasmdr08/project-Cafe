[System.Serializable]

public class PlayerData
{
    public int[] scores = new int[10];
    public string[] users = new  string[10];

    public PlayerData(int[] scores, string[] users) {
        for (int i = 0; i < 10; i++) {
            this.scores[i] = scores[i];
            this.users[i] = users[i];
        }
    }
}
