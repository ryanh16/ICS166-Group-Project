using UnityEngine;
using UnityEngine.UI;

public class DateUI : MonoBehaviour
{
    [SerializeField] private Text dateText;

    public static DateUI Instance;



    private void Awake()
    {
        Instance = this;
    }


    private void OnDestroy()
    {
        Instance = null;
    }


    public void UpdateDate(string newDate)
    {
        dateText.text = newDate;
    }
}