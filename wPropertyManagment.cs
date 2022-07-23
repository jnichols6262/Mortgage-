using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wPropertyManagment : MonoBehaviour
{
    public Text manageText;
    public Text buttonText;
    public Image iSpot;
    public int propertyNumber;
    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject bHouse;
    GameManager gm;
    Player player;
    soSpot spot;
    public ePos ownedProp;
    public int mortgageCost;
    public void InitPropertyManagment()
    {
        gm = GameManager.gm;
        player = gm.players[gm.curPlayer];
        
        ownedProp = player.listPropertiesOwned[propertyNumber].position;
        spot = gm.rRep.spots[(int)ownedProp];
        if (spot.typeSpot != eTypeSpot.property)
        {
            bHouse.SetActive(false);

        }
        else
            bHouse.SetActive(true);
        if(spot.isMortgage == false)
        {
            iSpot.sprite = spot.spotArt;
        }
        else
        {
            iSpot.sprite = spot.spotBackArt;
        }
        leftArrow.SetActive(false);
        if (propertyNumber > player.listPropertiesOwned.Count)
        {
            rightArrow.SetActive(false);
        }
        

            
    }

    public void OnRightArrow()
    {
        propertyNumber += 1;
        ownedProp = player.listPropertiesOwned[propertyNumber].position;
        spot = gm.rRep.spots[(int)ownedProp];
        iSpot.sprite = spot.spotArt;
        leftArrow.SetActive(true);
        if (spot.typeSpot != eTypeSpot.property)
        {
            bHouse.SetActive(false);

        }
        else
            bHouse.SetActive(true);
        if (propertyNumber <= player.listPropertiesOwned.Count)
        {
            rightArrow.SetActive(false);
        }
    }

    public void OnLeftArrow()
    {
        propertyNumber -= 1;
        ownedProp = player.listPropertiesOwned[propertyNumber].position;
        spot = gm.rRep.spots[(int)ownedProp];
        iSpot.sprite = spot.spotArt;
        if (spot.typeSpot != eTypeSpot.property)
        {
            bHouse.SetActive(false);

        }
        else
            bHouse.SetActive(true);
        if (propertyNumber > player.listPropertiesOwned.Count)
        {
            rightArrow.SetActive(true);
        }
        if (propertyNumber == 0)
        {
            leftArrow.SetActive(false);
        }
    }

    public void OnCancel()
    {
        Destroy(this.gameObject);
    }
    public void MortgageProperty()
    {
        player.SetCashOnHand(spot.mortgageAmount);
        spot.isMortgage = true;
        Mortgaged();
        Destroy(this.gameObject);
    }

    public void Mortgaged()
    {
        string message = "You just used mortgaged " + spot.nameSpot + " for $" + spot.mortgageAmount + "?";
        wMessage scr = Instantiate(GameManager.gm.rRep.wMessage, GameManager.gm.inGameUI.transform).GetComponent<wMessage>();
        scr.InitUi(message, "", "OK", null, "");
    }
    public void UnMortgaged()
    {
        string message = "You just used unmortgage " + spot.nameSpot + " for $" + mortgageCost + "?";
        wMessage scr = Instantiate(GameManager.gm.rRep.wMessage, GameManager.gm.inGameUI.transform).GetComponent<wMessage>();
        scr.InitUi(message, "", "OK", null, "");
    }
    public void UnMortgageProperty()
    {
        mortgageCost = (int)((float)spot.mortgageAmount * 1.1);
        player.SetCashOnHand(-mortgageCost);
        spot.isMortgage = false;
        UnMortgaged();
        Destroy(this.gameObject);
    }

    public void OnMortgage()
    {
        if(spot.isMortgage == false)
        {
            string message = "Are you sure you want to mortgage " + spot.nameSpot + " for $" + spot.mortgageAmount + "?";
            wMessage scr = Instantiate(GameManager.gm.rRep.wMessage, GameManager.gm.inGameUI.transform).GetComponent<wMessage>();
            scr.InitUi(message, "No", "Yes", this.gameObject, "MortgageProperty");
            
        }
        else
        {
            string message = "Are you sure you want to unmortgage " + spot.nameSpot + " for $" + mortgageCost + "?";
            wMessage scr = Instantiate(GameManager.gm.rRep.wMessage, GameManager.gm.inGameUI.transform).GetComponent<wMessage>();
            scr.InitUi(message, "No", "Yes", this.gameObject, "UnMortgageProperty");
        }
    }

    public void BuyHouse()
    {
        if (spot.amountHouses != 5)
        {
            player.SetCashOnHand(-spot.costOfImprovement);
            spot.amountHouses += 1;
            HouseBought();
            Destroy(this.gameObject);
        }
        else
        {
            string message = "You already own a hotel!";
            wMessage scr = Instantiate(GameManager.gm.rRep.wMessage, GameManager.gm.inGameUI.transform).GetComponent<wMessage>();
            scr.InitUi(message, "", "OK", null, "");
            Destroy(this.gameObject);
        }
        

        

    }

    public void HouseBought()
    {
        string message = "You just used bought a house for " + spot.nameSpot + " for $" + spot.costOfImprovement + "?";
        wMessage scr = Instantiate(GameManager.gm.rRep.wMessage, GameManager.gm.inGameUI.transform).GetComponent<wMessage>();
        scr.InitUi(message, "", "OK", null, "");
    }

    public void OnHouse()
    {
        if(player.cashOnHand > spot.costOfImprovement)
        {
            string message = "Are you sure you want to buy a house for " + spot.nameSpot + " for $" + spot.costOfImprovement + "?";
            wMessage scr = Instantiate(GameManager.gm.rRep.wMessage, GameManager.gm.inGameUI.transform).GetComponent<wMessage>();
            scr.InitUi(message, "No", "Yes", this.gameObject, "BuyHouse");
            
            
        }
    }
}
