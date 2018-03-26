using Unity.Entities;

namespace ECSExample
{
    public class SelectableComponentWrapper : ComponentDataWrapper<Selectable>
    {
    }

    public class UnitComponentWrapper : ComponentDataWrapper<Unit>
    {
    }

    public class ControllableComponentWrapper : ComponentDataWrapper<Controllable>
    {
    }
}