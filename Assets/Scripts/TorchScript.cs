using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class TorchScript : MonoBehaviour
    {
        [SerializeField] private Sprite batteryStatus0,
            batteryStatus1,
            batteryStatus5,
            batteryStatus15,
            batteryStatus25,
            batteryStatus40,
            batteryStatus55,
            batteryStatus62,
            batteryStatus75,
            batteryStatus82,
            batteryStatus88,
            batteryStatus94,
            batteryStatus100;

        private bool torchTurnedOn;
        public static float batteryBank;
        private int torchButtonState;
        [SerializeField] private GameObject batteryIcon;
        [SerializeField] private GameObject torchGameObject;
        [SerializeField] private GameObject batteryUIArea;

        private void Start()
        {
            batteryUIArea.SetActive(false);
            torchGameObject.SetActive(false);
            torchButtonState = 0;
        }

        private void Update()
        {
            batteryBank = Mathf.Clamp(batteryBank, 0, 120);

            if (InventoryManager._selected == 6 && Input.GetKeyDown(KeyCode.Mouse1))
            {
                torchButtonState += 1;
            }

            if (NetScript.addPowerToTorch && NetScript._torchChargeTotal > 1)
            {
                batteryBank += NetScript._torchChargeTotal;
            }

            if (torchButtonState >= 2)
            {
                torchButtonState = 0;
            }

            if (torchButtonState == 1)
            {
                batteryUIArea.SetActive(true);
                torchGameObject.SetActive(true);
                
                if (batteryBank > 0.2f)
                {
                    batteryBank -= 0.8f;
                }
                
            }

            if (torchButtonState == 0)
            {
                batteryUIArea.SetActive(false);
                torchGameObject.SetActive(false);
            }
            
            if (batteryIcon.activeInHierarchy && batteryBank > 0)
            {
                torchGameObject.GetComponent<Light>().intensity = 50;
            }
            else
            {
                torchGameObject.GetComponent<Light>().intensity = 0;
            }

            if (batteryBank < 1)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus0;
            }
            else if (batteryBank < 5 && batteryBank > 0)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus1;
            }
            else if (batteryBank < 15 && batteryBank > 4)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus5;
            }
            else if (batteryBank < 25 && batteryBank > 14)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus15;
            }
            else if (batteryBank < 40 && batteryBank > 24)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus25;
            }
            else if (batteryBank < 55 && batteryBank > 39)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus40;
            }
            else if (batteryBank < 62 && batteryBank > 54)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus55;
            }
            else if (batteryBank < 75 && batteryBank > 61)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus62;
            }
            else if (batteryBank < 82 && batteryBank > 74)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus75;
            }
            else if (batteryBank < 88 && batteryBank > 81)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus82;
            }
            else if (batteryBank < 94 && batteryBank > 87)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus88;
            }
            else if (batteryBank < 100 && batteryBank > 93)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus94;
            }
            else if (batteryBank < 120 && batteryBank > 99)
            {
                batteryIcon.GetComponent<Image>().sprite = batteryStatus100;
            }
        }
    }
}
