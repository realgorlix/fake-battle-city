using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image Game_over_label;
    [SerializeField] private Text Lives_left, Tanks_left, Final_score;

    private void Awake()
    {
        Game_over_label.enabled = false;
        Final_score.enabled = false;
    }

    public void Set_health_counter(int counter)
    {
        Lives_left.text = "Lives: " + counter;
    }

    public void Set_enemies_counter(int left)
    {
        Tanks_left.text = "Enemies Left: " + left;
    }

    public void Game_over()
    {
        Game_over_label.enabled = true;
    }

    public void Game_win(int final_score)
    {
        Lives_left.enabled = false;
        Tanks_left.enabled = false;
        Final_score.enabled = true;
        Final_score.text = "Final Score: " + final_score;
    }
}
