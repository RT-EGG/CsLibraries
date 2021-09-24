using Reactive.Bindings;

namespace RtCs
{
    public class ModificationRecordValue<T> : ReactiveProperty<T>
    {
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
