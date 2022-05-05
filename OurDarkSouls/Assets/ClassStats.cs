using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    [System.Serializable]
    public class ClassStats
    {
        public string playerClass;
        public int classLevel;

        [TextArea]
        public string classDescription;

        [Header("Class Stats")]

        public int strenghtLevel;
        public int dexterityLevel;

    }
}
