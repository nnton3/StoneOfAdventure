using UnityEngine;
using UnityEngine.UI;

public class PlayerParamBar : MonoBehaviour {

    public Player player;

    [Header("OBJECTS")]
    public Transform loadingBar;

    float current_percent = 100;
    [Range(0, 100)] public int speed;

    void Update() {
        if (current_percent > ((int)((player.health / player.max_health) * 100)) + 1)
        {
            current_percent -= speed * Time.deltaTime;
        }
        else if (current_percent < ((int)((player.health / player.max_health) * 100)) - 1)
        {
            current_percent += speed * Time.deltaTime;
        }
        Debug.Log(current_percent);
        Debug.Log((player.health / player.max_health) * 100);
        loadingBar.GetComponent<Image>().fillAmount = current_percent / 100;
    }
}
