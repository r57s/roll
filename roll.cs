using UnityEngine;

public class Roll
{
    public static Roll d6 = new Roll("d6");
    public static Roll d10 = new Roll("d10");
    public static Roll d100 = new Roll("d100");
    
    public int Count { get; private set; }
    public int Face { get; private set; }
    public int Add { get; private set; }
    
    public Roll(string m)
    {
        int it = 0;
        SkipWhiteSpace(m, ref it);
        
        // Check for Count
        if (char.IsNumber(m[it]))
        {
            Count = ReadNumber(m, ref it);
        }
        else
        {
            Count = 1;
        }
        
        if (SkipConstant(m, 'd', ref it))
            return;
        
        SkipWhiteSpace(m, ref it);
        
        if (char.IsNumber(m[it]))
        {
            Face = ReadNumber(m, ref it);
        }
        else
        {
            return;
        }
        
        if (SkipConstant(m, '+', ref it))
        {
            SkipWhiteSpace(m, ref it);
            if (char.IsNumber(m[it]))
            {
                Add = ReadNumber(m, ref it);
            }
        }
        
    }
    
    public Roll(int count, int face, int add)
    {
        Count = count;
        Face = face;
        Add = add;   
    }
    
    public int Dice()
    {
        int roll = Add;
       
        for (var i = 0; i < Count; i++)
            roll += Random(1, Face);
        
        return roll;
    }
    
    protected virtual int Random(int min, int max)
    {
        return Mathf.RoundToInt(min + UnityEngine.Random.value * (max - min));
    }
     
     
    public override string ToString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder(10);
        if (Count != 1)
            sb.Append(Count);
        sb.Append('d');
        sb.Append(Face);
        if (Add != 0)
            sb.AppendFormat("{+0}", Add);
        return sb.ToString();
    }
    
    public static void Test()
    {
        bool x = Test("d6");
        x |= Test("d60");
        x |= Test("2d6");
        x |= Test("20d60");
        x |= Test("d6+1");
        x |= Test("d60+10");
        x |= Test("2d6+1");
        x |= Test("20d60+10");
        if (!x)
        Debug.Log("Failed");
        else
        Debug.Log("Passed");
    }
    
    private static bool Test(string m)
    {
        if (new Roll(m).ToString() != m.Replace(" ", "")) {
            Debug.LogError("Dice:" + m);
            return false;}
            return true;
    }
    
    private static void SkipWhiteSpace(string str, ref int it)
    {
        for(;it < str.Length;it++)
        {
            if (char.IsWhiteSpace(str[it]) == false)
                break;
        }
    }
    
    public static bool SkipConstant(string str, char c, ref int it)
    {
        SkipWhiteSpace(str, ref it);
        if (str[it] == c)
        {
            it++;
            return true;
        }
        return false;
    }
    
    private static int ReadNumber(string str, ref int it)
    {
        
    }
    
}