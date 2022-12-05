using System.Collections.Generic;
using DestroyKingdom.Data;
using JetBrains.Annotations;
using TaleWorlds.SaveSystem;

namespace DestroyKingdom;

[UsedImplicitly]
public class CustomSaveDefiner : SaveableTypeDefiner
{
    public CustomSaveDefiner() : base(212_894_101)
    {
    }

    protected override void DefineClassTypes()
    {
        AddClassDefinition(typeof(AnnexationRebelClansStorage), 1);
    }

    protected override void DefineContainerDefinitions()
    {
        ConstructContainerDefinition(typeof(Dictionary<string, List<string>>));
        ConstructContainerDefinition(typeof(List<string>));
    }
}