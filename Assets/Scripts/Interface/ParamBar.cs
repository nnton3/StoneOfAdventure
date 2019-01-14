using UnityEngine;
using UnityEngine.UI;

public class ParamBar : MonoBehaviour {

    public Unit unit;

    [Header("OBJECTS")]
    public Transform loadingBar;

    float current_percent = 100;
    [Range(0, 100)] public int speed;

    void Update() {
        if (current_percent > ((int)((unit.health / unit.max_health) * 100)) + 1)
        {
            current_percent -= speed * Time.deltaTime;
        }
        else if (current_percent < ((int)((unit.health / unit.max_health) * 100)) - 1)
        {
            current_percent += speed * Time.deltaTime;
        }
        loadingBar.GetComponent<Image>().fillAmount = current_percent / 100;
    }
}
