using Reactive.Bindings;
using RtCs;
using RtCs.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Snow.View
{
    interface ISimulationModel
    {
        void Initialize();
        void Initialize(float inSeed);

        IReactiveProperty<float> TimeStepScale { get; }
    }

    partial class SimulationView : UserControl
    {
        public SimulationView()
        {
            InitializeComponent();
        }

        public ISimulationModel Model
        {
            get => m_Model;
            set {
                if (m_Model == value) {
                    return;
                }

                m_ModelSubscription.DisposeItems();
                m_ModelSubscription.Clear();

                m_Model = value;
                if (m_Model != null) {
                    m_ModelSubscription.Add(m_Model.TimeStepScale.Subscribe(v => {
                        TrackBarSimulationScaleValue = v;
                        UdSimulationScale.Value = (decimal)v;
                    }));
                }

                this.SetEnabledRecursive(m_Model != null);
            }
        }

        private void ButtonInitializeSimulation_Click(object sender, EventArgs e)
            => m_Model.Initialize();

        private void TrackBarSimulationScale_ValueChanged(object sender, EventArgs e)
        {
            UdSimulationScale.ValueChanged -= UdSimulationScale_ValueChanged;
            try {
                UdSimulationScale.Value = (decimal)TrackBarSimulationScaleValue;
                m_Model.TimeStepScale.Value = TrackBarSimulationScaleValue;

            } finally {
                UdSimulationScale.ValueChanged += UdSimulationScale_ValueChanged;
            }
        }

        private void UdSimulationScale_ValueChanged(object sender, EventArgs e)
        {
            TrackBarSimulationScale.ValueChanged -= TrackBarSimulationScale_ValueChanged;
            try {
                TrackBarSimulationScaleValue = (float)UdSimulationScale.Value;
                m_Model.TimeStepScale.Value = (float)UdSimulationScale.Value;

            } finally {
                TrackBarSimulationScale.ValueChanged += TrackBarSimulationScale_ValueChanged;
            }
        }

        private float TrackBarSimulationScaleValue
        {
            get => TrackBarSimulationScale.Value / 10.0f;
            set => TrackBarSimulationScale.Value = (int)(value * 10.0f);
        }

        private ISimulationModel m_Model;
        private List<IDisposable> m_ModelSubscription = new List<IDisposable>();
    }
}
