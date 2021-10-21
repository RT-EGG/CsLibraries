using Reactive.Bindings;

namespace RtCs
{
    public interface IModificationRecordValue : IReactiveProperty
    {
        bool CheckAndTurnOff();
    }

    public class ModificationRecordValue<T> : ReactiveProperty<T>, IModificationRecordValue
    {
        public ModificationRecordValue()
            : this(default)
        { }

        public ModificationRecordValue(T inInitializeValue)
            : base(initialValue: inInitializeValue)
        { }

        public void MarkUnchanged()
            => Changed = false;
        public bool CheckAndTurnOff()
        {
            bool result = Changed;
            MarkUnchanged();
            return result;
        }

        public bool Changed
        { get; private set; } = true;
    }
}
