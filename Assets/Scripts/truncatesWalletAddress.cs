using UnityEngine;
using UnityEngine.UI;

public class truncatesWalletAddress : MonoBehaviour
{
    public string originalString = "0xf6C1eb5aAdF622d53e6cC9Dda09b83A942F2CD2f";
    public Text TruncteText;

    void Start()
    {
        string truncatedString = originalString.Substring(0, 4) + "..." + originalString.Substring(originalString.Length - 4);
        //Debug.Log(truncatedString);

        TruncteText.text = truncatedString; 
    }
}
