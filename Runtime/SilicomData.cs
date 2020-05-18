using UnityEngine;

namespace Silicom.Name
{
    public class SilicomData
    {

        public string EquipmentName;
        public string VarName;
        public string VarType;
        public string VarValue;

        public bool TryReadAsBool(out bool boolValue)
        {
            if (bool.TryParse(VarValue, out boolValue)) return true;
            Debug.Log($"Parsing failed for {EquipmentName} / {VarName} / {VarType} / {VarValue}");
            return false;
        }

        public bool TryReadAsInt(out int intValue)
        {
            if (int.TryParse(VarValue, out intValue)) return true;
            Debug.Log($"Parsing failed for {EquipmentName} / {VarName} / {VarType} / {VarValue}");
            return false;
        }

        public bool TryReadAsFloat(out float floatValue)
        {
            if (float.TryParse(VarValue, out floatValue)) return true;
            Debug.Log($"Parsing failed for {EquipmentName} / {VarName} / {VarType} / {VarValue}");
            return false;
        }

        public bool TryReadAsString(out string stringValue)
        {
            stringValue = VarValue;
            return true;
        }

        public const string TYPE_BOOL = "bool";
        public const string TYPE_FLOAT = "float";

    }
}