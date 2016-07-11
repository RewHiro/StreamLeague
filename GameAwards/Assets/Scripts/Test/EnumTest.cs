using UnityEngine;
using System.Reflection;
using System.Reflection.Emit;

public class EnumTest : MonoBehaviour
{
    static System.Type BuildEnum(string[] strings)
    {
        AssemblyName asmName = new AssemblyName { Name = "MyAssembly" };
        System.AppDomain domain = System.AppDomain.CurrentDomain;
        AssemblyBuilder asmBuilder = domain.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
        ModuleBuilder moduleBuilder = asmBuilder.DefineDynamicModule("MyModule");
        EnumBuilder enumBuilder = moduleBuilder.DefineEnum("MyNamespace.MyEnum", TypeAttributes.Public, typeof(int));

        for (int i = 0; i < strings.Length; ++i)
            enumBuilder.DefineLiteral(strings[i], i + 1);
        
        return enumBuilder.CreateType();
    }

    void Start()
    {
        string[] animalTable = new string[] { "Dog", "Cat", "Horse", "Elephant" };
        System.Type enumType = BuildEnum(animalTable);
        Debug.Log(enumType);
    }
}
