using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debuffManager : MonoBehaviour
{
    public static List<deBuff> all = new List<deBuff>();


    public static void Init()
    {
        all.Clear();
    }

    public static void RunDebuffs()
    {

        
        for(int i = all.Count -1;i>= 0 && i< all.Count; ++i )
        {

            var item = all[i];
            float value = AttributeData.values[item.debuf.attribute];
           
            item.debuf.DoOnBase();
                
        }
    }

    public static bool hasDebuff(Attributes attribute )
    {

        print(all.Count);
        foreach(var item in all)
        {
            if (item.debuf.attribute == attribute) return true;
        }


        return false;
    }
}
