using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text countHealth;
    float valueProgress = 0;
    Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        slider.transform.LookAt(transform.position + camera.transform.forward);
    }
    public void SetMaxValus(float maxValues)
    {
        slider.maxValue = maxValues;
        valueProgress = maxValues;
        slider.value = maxValues;
        countHealth.text = maxValues.ToString();
    }

    public void SetValues(float price)
    {
        float newCount = valueProgress + price;
        countHealth.DOCounter((int)valueProgress, (int)newCount, 0.7f);
        valueProgress += price;
        slider.DOValue(valueProgress, 1);
    }

    public void SetBadValues(float price)
    {
        float newCount = valueProgress - price;
        countHealth.DOCounter((int)valueProgress, (int)newCount, 0.7f);
        valueProgress -= price;
        slider.DOValue(valueProgress, 0.7f);
        if (valueProgress <= 0)
        {
            slider.gameObject.SetActive(false);
        }

    }
}
