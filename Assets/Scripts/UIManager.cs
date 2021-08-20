using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _collectableText;

    public void UpdateCollectableText(int collectableCount)
    {
        _collectableText.text = "Balls: " + collectableCount;
    }
}
