//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Fun2048.ChipPrimaryIDComponent chipPrimaryID { get { return (Fun2048.ChipPrimaryIDComponent)GetComponent(GameComponentsLookup.ChipPrimaryID); } }
    public bool hasChipPrimaryID { get { return HasComponent(GameComponentsLookup.ChipPrimaryID); } }

    public void AddChipPrimaryID(int newValue) {
        var index = GameComponentsLookup.ChipPrimaryID;
        var component = (Fun2048.ChipPrimaryIDComponent)CreateComponent(index, typeof(Fun2048.ChipPrimaryIDComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceChipPrimaryID(int newValue) {
        var index = GameComponentsLookup.ChipPrimaryID;
        var component = (Fun2048.ChipPrimaryIDComponent)CreateComponent(index, typeof(Fun2048.ChipPrimaryIDComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveChipPrimaryID() {
        RemoveComponent(GameComponentsLookup.ChipPrimaryID);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherChipPrimaryID;

    public static Entitas.IMatcher<GameEntity> ChipPrimaryID {
        get {
            if (_matcherChipPrimaryID == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ChipPrimaryID);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherChipPrimaryID = matcher;
            }

            return _matcherChipPrimaryID;
        }
    }
}