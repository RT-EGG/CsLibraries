using Reactive.Bindings;
using System;
using System.Reactive.Linq;

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

        public ModificationRecordValue(T inInitializeValue, bool inInitChanged = false)
            : base(initialValue: inInitializeValue)
        {
            Changed = inInitChanged;
            (this as IReactiveProperty<T>).Skip(1).Subscribe(_ => Changed = true);
            return;
        }

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
