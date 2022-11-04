using System.Collections.Concurrent;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        
        //While generic classes are type-safe and efficient due to lack of boxing and unboxing,
        //Concurrent classes are thread-safe

        //Concurrent classes make it so that, when multiple threads try to access a singular data
        //they can do so without any deadlocks or exceptions 

        ConcurrentDictionary<string, string> dictionaryCountries = new ConcurrentDictionary<string, string>();
        dictionaryCountries.TryAdd("UK", "London, Manchester, Birmingham");
        dictionaryCountries.TryAdd("USA", "Chicago, New York, Washington");
        dictionaryCountries.TryAdd("IND", "Mumbai, Delhi, Bhubaneswar");
        
        //The following key will not be added as IND already exits
        //It will not throw exception, simply TryAdd method returns false
        bool IsDupliacteKeyAdded = dictionaryCountries.TryAdd("IND", "Mumbai, Bhubaneswar");

        //Accessing Dictionary Elements using For Each Loop
        Console.WriteLine("Accessing ConcurrentDictionary Elements using For Each Loop");
        foreach (KeyValuePair<string, string> KVP in dictionaryCountries)
        {
            Console.WriteLine($"Key:{KVP.Key}, Value: {KVP.Value}");
        }
        //Or
        //foreach (var item in dictionaryCountries)
        //{
        //    Console.WriteLine($"Key:{ item.Key}, Value: { item.Value}");
        //}

        Console.WriteLine("\nIs USA Key Exists : " + dictionaryCountries.ContainsKey("IND")); //check if a key is contained in the dictionary 

        Console.WriteLine("\nRemoving Element using TryRemove Method");
        string removedCountry = string.Empty;

        bool result = dictionaryCountries.TryRemove("IND", out removedCountry); //out catches tje removed keyword and stores the reference in removedCountry

        Console.WriteLine($"Is IND Key Removed: {result}");
        Console.WriteLine($"Removed Value: {removedCountry}");

        bool result1 = dictionaryCountries.TryUpdate("UK", "United Kingdom Updated", "United");    //updates if the prevously value matches, else return false
        Console.WriteLine($"\nIs the key UK update with TryUpdate Method: {result1}");
        Console.WriteLine($"key UK, Value: {dictionaryCountries["UK"]}");

        result1 = dictionaryCountries.TryUpdate("UK", "United Kingdom Updated", "London, Manchester, Birmingham"); 
        Console.WriteLine($"\nIs the key UK update with TryUpdate Method: {result1}");
        Console.WriteLine($"key UK, Value: {dictionaryCountries["UK"]}");

        dictionaryCountries.AddOrUpdate("SL", "Srilanka", (k, v) => "Srilanka Updated"); //Adds if key not present else, updates

        string Result1 = dictionaryCountries.GetOrAdd("UK", "United Kingdom");          //Returns the value if present or adds to the dict and returns 
        Console.WriteLine($"Key:UK, Value: {Result1}");

        Console.Read();
        Console.Clear();

        //out, ref, params

        int x= 0;
        int y = Add(10, 20, 30, 40, 50);
        int z;

        Console.WriteLine("param result: " + y);

        Calc(y, out z);
        Half(y, ref x, ref z);

        Console.WriteLine("\nOut can take un initialised arguments, but will definitely assign / initiate a value inside the function: " + z);
        Console.WriteLine("\nRef takes initialised arguments and may or may not change their value inside the function: " + x);

    }
    public static int Add(params int[] MyColl)          //params allows a list of specificed type to be passed as arguments
    {                                                   //It will also accept no arguments and considers the list of size 0
        int total = 0;                                  //Only one param allowed with no arguments after it 

        foreach (int i in MyColl)
        {
            total += i;
        }
        return total;
    }
    public static void Calc(int num, out int temp)
    {
        temp = num * 2; //error if you comment it out
    }
    public static void Half(int num , ref int x, ref int z) //error if z was not initialised before
    {
        x = num * z / 2;
    }

}
