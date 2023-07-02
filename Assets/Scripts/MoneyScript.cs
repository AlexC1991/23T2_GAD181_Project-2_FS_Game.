using System;
using UnityEngine;
using UnityEngine.UI;


namespace AlexzanderCowell
{
    public class MoneyScript : MonoBehaviour
    {
        [SerializeField] private Text moneyDisplay;
        [HideInInspector] public static int moneyTotal;
        private void Start()
        {
            moneyTotal = 5;
        }

        private void Update()
        {
            moneyDisplay.text = (moneyTotal).ToString();
            moneyTotal = Mathf.Clamp(moneyTotal, 0, 100);
        }
    }
}
