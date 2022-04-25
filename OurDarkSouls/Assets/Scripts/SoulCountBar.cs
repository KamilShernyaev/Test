using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class SoulCountBar : MonoBehaviour
    {
        public Text soulCountText;
    
        public void SetSoulCountText(int currentSoulCount)
        {
            soulCountText.text = currentSoulCount.ToString();
        }
    }
}