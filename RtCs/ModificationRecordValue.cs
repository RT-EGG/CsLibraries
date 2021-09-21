using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs
{
    public class ModificationRecordValue<T> : IObservable<T>
    {
        public ModificationRecordValue()
            : this(default)
        { }

        public ModificationRecordValue(T inInitialValue)
        {
            m_Value = inInitialValue;
            return;
        }

        public T Value 
        {
            get => m_Value;
            set {
                if (!m_Value.Equals(value)) {
                    Changed = true;
                }
                m_Value = value;
                foreach (var observer in m_SubscriptionList.Select(s => s.Observer)) {
                    observer.OnNext(m_Value);
                }
                return;
            }
        }

        public void MarkUnchanged()
            => Changed = false;
        public bool CheckAndTurnOff()
        {
            bool result = Changed;
            MarkUnchanged();
            return result;
        }

        public IDisposable Subscribe(IObserver<T> inObserver)
        {
            var subscription = new Subscription(this, inObserver);
            m_SubscriptionList.Add(subscription);
            inObserver.OnNext(Value);
            return subscription;
        }

        public IDisposable Subscribe(Action<T> inAction)
            => Subscribe(new DefaultObserver(inAction));

        public bool Changed
        { get; private set; } = true;

        private T m_Value = default;
        private List<Subscription> m_SubscriptionList = new List<Subscription>();

        private class Subscription : IDisposable
        {
            public Subscription(ModificationRecordValue<T> inOwner, IObserver<T> inObserver)
            {
                m_Owner = inOwner;
                Observer = inObserver;
                return;
            }

            public void Dispose()
            {
                m_Owner.m_SubscriptionList.Remove(this);
                return;
            }

            private readonly ModificationRecordValue<T> m_Owner = null;
            public readonly IObserver<T> Observer;
        }

        public class DefaultObserver : IObserver<T>
        {
            public DefaultObserver(Action<T> inOnNotify)
            {
                OnNotify = inOnNotify;
                return;
            }

            public readonly Action<T> OnNotify = default;

            public void OnCompleted()
            { }

            public void OnError(Exception inError)
            { }

            public void OnNext(T inValue)
                => OnNotify.Invoke(inValue);
        }
    }
}
