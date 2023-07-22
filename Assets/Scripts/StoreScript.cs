using UnityEngine;
using UnityEngine.UI;

namespace AlexzanderCowell
{
    public class StoreScript : MonoBehaviour
    {
        [SerializeField] private InventoryManager invertoryM; // Inventory Manager Script.
        [SerializeField] private GameObject turnOnOrOffUI; // UI Text On Booth to be turned on or off for displaying for Selling & Buying.
        private bool insideOfStore; // Checks if player is in the store collision or not.
        private bool canSellCarrots,canSellPotatos,canBuyCSeeds,canBuyPSeeds; // Checks if can sell the certain vegetables/seeds related to what should be displayed.
        [SerializeField] private Text costDisplay; // Displays the text on the booth as to what it should be saying when activated. 
        private int costTotalPSeeds, costTotalCSeeds; // How much each vegetable/seeds cost.

        // Declaration of the variables needed to play the buys sound effects
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip buySFX;

        private void Start()
        {
            turnOnOrOffUI.SetActive(false); // Start of the game the UI will be turned off for the booth.
            
            costDisplay.text = ("Lets Trade!"); // Default message for the cost display text to say.
            costTotalPSeeds = 1; // How much Potato Seeds cost.
            costTotalCSeeds = 3; // How much Carrot Seeds cost.
            
            canSellCarrots = false; // Can sell carrots set to false.
            canSellPotatos = false; // Can sell Potato's set to false.
            canBuyCSeeds = false; // Can Buy Carrot Seeds set to false.
            canBuyPSeeds = false; // Can buy Potato Seeds set to false.
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) // If player is inside of the collider it will allow the UI to display and make inside of store bool true.
            {
                turnOnOrOffUI.SetActive(true);
                insideOfStore = true;
            }
        }
        private void Update()
        {
            if (InventoryManager._selected == 1 && insideOfStore && MoneyScript.moneyTotal >= costTotalCSeeds) // If the Invertory is selected is equal to 1 & you are inside of the store & you have enough money that is more or equal to the cost of the seeds.
            {
                costDisplay.text = ("Costs: $" + costTotalCSeeds + " Press E To Buy"); // You will see the cost of the Carrot Seeds and Prompted to press X to buy.
                canBuyCSeeds = true; // Allowed to buy Carrot seeds.
            }
            else
            {
                canBuyCSeeds = false; // If any is null you can not buy Carrot Seeds.
            }

            if (InventoryManager._selected == 2 && insideOfStore && MoneyScript.moneyTotal >= costTotalPSeeds) // If the Invertory is selected is equal to 2 & you are inside of the store & you have enough money that is more or equal to the cost of the seeds.
            {
                costDisplay.text = ("Costs: $" + costTotalPSeeds + " Press E To Buy"); // You will see the cost of the Potato Seeds and Prompted to press X to buy.
                canBuyPSeeds = true; // Allowed to buy Potato seeds.
            }
            else
            {
                canBuyPSeeds = false; // If any is null you can not buy Potato Seeds.
            }

            if (InventoryManager._selected == 3 && insideOfStore && SeedStorage.carrots >= 1) // If the Invertory is selected is equal to 3 & you are inside of the store & you have more or equal to 1 of the carrots in your invertory you can sell them.
            {
                costDisplay.text = ("We Do Not Sell Anything Here Sir!"); // You will see the cost of the Carrots and Prompted to press X to sell.
                canSellCarrots = true; // Allowed to sell the Carrots.
            }
            else
            {
                canSellCarrots = false; // If any is null you can not sell Carrots.
            }

            if (InventoryManager._selected == 4 && insideOfStore && SeedStorage.potatos >= 1) // If the Invertory is selected is equal to 4 & you are inside of the store & you have more or equal to 1 of the potato's in your invertory you can sell them.
            {
                costDisplay.text = ("We Do Not Sell Anything Here Sir!"); // You will see the cost of the Potato's and Prompted to press X to sell.
                canSellPotatos = true; // Allowed to sell the Potato's.
            }
            else
            {
                canSellPotatos = false; // If any is null you can not sell Potato's.
            }

            if (canBuyPSeeds && Input.GetKeyDown(KeyCode.E) && insideOfStore) // If can buy Potato Seeds are true & you are still inside of the store and you press X button.
            {
                MoneyScript.moneyTotal -= costTotalPSeeds; // Will minus the cost of the Potato Seeds from the money total.
                SeedStorage.potatoSeed += 1; // Will add 1 potato seed from the seed storage script.
                canBuyPSeeds = false; // Turns off Can buy Potato Seeds so it does not continuously loop.

                // Plays the buySFX sound effect
                sfxSource.PlayOneShot(buySFX);
            }

            if (canBuyCSeeds && Input.GetKeyDown(KeyCode.E) && insideOfStore) // If can buy Carrot Seeds are true & you are still inside of the store and you press X button.
            {
                MoneyScript.moneyTotal -= costTotalCSeeds; // Will minus the cost of the Carrot Seeds from the money total.
                SeedStorage.carrotSeed += 1; // Will add 1 carrot seed from the seed storage script.
                canBuyCSeeds = false; // Turns off Can buy Carrot Seeds so it does not continuously loop.

                // Plays the buySFX sound effect
                sfxSource.PlayOneShot(buySFX);
            }

            if (InventoryManager._selected == 4 && insideOfStore && SeedStorage.potatos == 0) // If the Invertory is selected is equal to 4 & you are inside of the store & you have 0 of the potato's in your invertory you have nothing to sell.
            {
                costDisplay.text = ("We Do Not Buy Things Here Sir!");
            }

            if (InventoryManager._selected == 3 && insideOfStore && SeedStorage.carrots == 0) // If the Invertory is selected is equal to 3 & you are inside of the store & you have 0 of the carrots in your invertory you have nothing to sell.
            {
                costDisplay.text = ("We Do Not Buy Things Here Sir!");
            }

            if (InventoryManager._selected == 2 && insideOfStore  && MoneyScript.moneyTotal < costTotalPSeeds) // If the Invertory is selected is equal to 2 & you are inside of the store & you have less than the cost of the potato seeds in your money total then you can not buy anymore.
            {
                costDisplay.text = ("You can't buy that");
            }

            if (InventoryManager._selected == 1 && insideOfStore  && MoneyScript.moneyTotal < costTotalCSeeds) // If the Invertory is selected is equal to 1 & you are inside of the store & you have less than the cost of the carrot seeds in your money total then you can not buy anymore.
            {
                costDisplay.text = ("You can't buy that");
            }
        }
        private void OnTriggerExit(Collider other) 
        {
            if (other.CompareTag("Player")) // If the player is not inside of the collider then the UI will be turned off for the text.
            {
                turnOnOrOffUI.SetActive(false);
                insideOfStore = false;
            }

        }
    }
}
