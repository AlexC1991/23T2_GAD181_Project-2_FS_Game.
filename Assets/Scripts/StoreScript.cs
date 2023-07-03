using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class StoreScript : MonoBehaviour
    {
        [SerializeField] private InvertoryManager invertoryM;
        [SerializeField] private GameObject turnOnOrOffUI;
        private bool insideOfStore;
        private bool canSellCarrots,canSellPotatos,canBuyCSeeds,canBuyPSeeds;
        [SerializeField] private Text costDisplay;
        private int costTotalCarrots, costTotalPotatos, costTotalPSeeds, costTotalCSeeds;

        private void Start()
        {
            turnOnOrOffUI.SetActive(false);
            
            costDisplay.text = ("Lets Trade!");
            costTotalCarrots = 2;
            costTotalPotatos = 1;
            costTotalPSeeds = 1;
            costTotalCSeeds = 3;
            
            canSellCarrots = false;
            canSellPotatos = false;
            canBuyCSeeds = false;
            canBuyPSeeds = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                turnOnOrOffUI.SetActive(true);
                insideOfStore = true;
            }
        }
        private void Update()
        {
            if (invertoryM._selected == 1 && insideOfStore && MoneyScript.moneyTotal >= costTotalCSeeds)
            {
                costDisplay.text = ("Costs: $" + costTotalCSeeds + " Press X To Buy");
                canBuyCSeeds = true;
            }
            else
            {
                canBuyCSeeds = false;
            }

            if (invertoryM._selected == 2 && insideOfStore && MoneyScript.moneyTotal >= costTotalPSeeds)
            {
                costDisplay.text = ("Costs: $" + costTotalPSeeds + " Press X To Buy");
                canBuyPSeeds = true;
            }
            else
            {
                canBuyPSeeds = false;
            }

            if (invertoryM._selected == 3 && insideOfStore && SeedStorage.carrots >= 1)
            {
                costDisplay.text = ("You Get: $" + costTotalCarrots + " Press X To Sell");
                canSellCarrots = true;
            }
            else
            {
                canSellCarrots = false;
            }

            if (invertoryM._selected == 4 && insideOfStore && SeedStorage.potatos >= 1)
            {
                costDisplay.text = ("You Get: $" + costTotalPotatos + " Press X To Sell");
                canSellPotatos = true;
            }
            else
            {
                canSellPotatos = false;
            }

            if (canSellCarrots && Input.GetKeyDown(KeyCode.X) && insideOfStore)
            {
                MoneyScript.moneyTotal += costTotalCarrots;
                SeedStorage.carrots -= 1;
                canSellCarrots = false;
            }

            if (canSellPotatos && Input.GetKeyDown(KeyCode.X) && insideOfStore)
            {
                MoneyScript.moneyTotal += costTotalPotatos;
                SeedStorage.potatos -= 1;
                canSellPotatos = false;
            }

            if (canBuyPSeeds && Input.GetKeyDown(KeyCode.X) && insideOfStore)
            {
                MoneyScript.moneyTotal -= costTotalPSeeds;
                SeedStorage.potatoSeed += 1;
                canBuyPSeeds = false;
            }

            if (canBuyCSeeds && Input.GetKeyDown(KeyCode.X) && insideOfStore)
            {
                MoneyScript.moneyTotal -= costTotalCSeeds;
                SeedStorage.carrotSeed += 1;
                canBuyCSeeds = false;
            }

            if (invertoryM._selected == 4 && insideOfStore && SeedStorage.potatos == 0)
            {
                costDisplay.text = ("You have nothing to trade");
            }

            if (invertoryM._selected == 3 && insideOfStore && SeedStorage.carrots == 0)
            {
                costDisplay.text = ("You have nothing to trade");
            }

            if (invertoryM._selected == 2 && insideOfStore  && MoneyScript.moneyTotal < costTotalPSeeds)
            {
                costDisplay.text = ("You can't buy that");
            }

            if (invertoryM._selected == 1 && insideOfStore  && MoneyScript.moneyTotal < costTotalCSeeds)
            {
                costDisplay.text = ("You can't buy that");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                turnOnOrOffUI.SetActive(false);
            }

        }
    }
}
