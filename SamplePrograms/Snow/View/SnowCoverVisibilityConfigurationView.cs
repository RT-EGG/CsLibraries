using Reactive.Bindings;
using RtCs;
using RtCs.OpenGL;
using RtCs.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Snow.View
{
    interface ISnowCoverVisibilityConfigurationModel
    {
        IReactiveProperty<bool> ChannelVisibilityR { get; }
        IReactiveProperty<bool> ChannelVisibilityG { get; }
        IReactiveProperty<bool> ChannelVisibilityB { get; }
        IReactiveProperty<EGLRenderPolygonMode> RenderPolygonMode { get; }
        IReactiveProperty<float> InnerLevel { get; }
        IReactiveProperty<float> OuterLevel { get; }
        IReactiveProperty<float> HeightScale { get; }
        IReactiveProperty<SceneObject.SnowCover.RenderMode> RenderMode { get; }
    }

    partial class SnowCoverVisibilityConfigurationView : UserControl
    {
        public SnowCoverVisibilityConfigurationView()
        {
            InitializeComponent();
        }

        public ISnowCoverVisibilityConfigurationModel Model
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
                    m_ModelSubscription.Add(m_Model.ChannelVisibilityR.Subscribe(v => CheckChannelVisibilityR.Checked = v));
                    m_ModelSubscription.Add(m_Model.ChannelVisibilityG.Subscribe(v => CheckChannelVisibilityG.Checked = v));
                    m_ModelSubscription.Add(m_Model.ChannelVisibilityB.Subscribe(v => CheckChannelVisibilityB.Checked = v));
                    m_ModelSubscription.Add(m_Model.RenderPolygonMode.Subscribe(v => {
                        switch (v) {
                            case EGLRenderPolygonMode.Face:
                                RadioRenderFaceMode.Checked = true;
                                break;
                            case EGLRenderPolygonMode.Line:
                                RadioRenderLineMode.Checked = true;
                                break;
                            default:
                                m_Model.RenderPolygonMode.Value = EGLRenderPolygonMode.Face;
                                break;
                        };
                    }));
                    m_ModelSubscription.Add(m_Model.InnerLevel.Subscribe(v => UdInnerLevel.Value = (decimal)v));
                    m_ModelSubscription.Add(m_Model.OuterLevel.Subscribe(v => UdOuterLevel.Value = (decimal)v));
                    m_ModelSubscription.Add(m_Model.HeightScale.Subscribe(v => UdHeightScale.Value = (decimal)v));
                }

                this.SetEnabledRecursive(m_Model != null);
            }
        }

        private void CheckChannelVisibilityR_CheckedChanged(object sender, EventArgs e)
            => m_Model.ChannelVisibilityR.Value = CheckChannelVisibilityR.Checked;
        private void CheckChannelVisibilityG_CheckedChanged(object sender, EventArgs e)
            => m_Model.ChannelVisibilityG.Value = CheckChannelVisibilityG.Checked;
        private void CheckChannelVisibilityB_CheckedChanged(object sender, EventArgs e)
            => m_Model.ChannelVisibilityB.Value = CheckChannelVisibilityB.Checked;
        private void RadioRenderMode_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioRenderSurfaceMode.Checked) {
                m_Model.RenderMode.Value = SceneObject.SnowCover.RenderMode.Surface;
            } else if (RadioRenderNormalMode.Checked) {
                m_Model.RenderMode.Value = SceneObject.SnowCover.RenderMode.Normal;
            } else if (RadioRenderOffsetMode.Checked) {
                m_Model.RenderMode.Value = SceneObject.SnowCover.RenderMode.Offset;
            }
        }
        private void RadioRenderPolygonMode_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioRenderFaceMode.Checked) {
                m_Model.RenderPolygonMode.Value = EGLRenderPolygonMode.Face;
            } else if (RadioRenderLineMode.Checked) {
                m_Model.RenderPolygonMode.Value = EGLRenderPolygonMode.Line;
            }
        }

        private void UdInnerLevel_ValueChanged(object sender, EventArgs e)
            => m_Model.InnerLevel.Value = (float)UdInnerLevel.Value;
        private void UdOuterLevel_ValueChanged(object sender, EventArgs e)
            => m_Model.OuterLevel.Value = (float)UdOuterLevel.Value;

        private void UdHeightScale_ValueChanged(object sender, EventArgs e)
            => m_Model.HeightScale.Value = (float)UdHeightScale.Value;

        private ISnowCoverVisibilityConfigurationModel m_Model = null;
        private List<IDisposable> m_ModelSubscription = new List<IDisposable>();
    }
}
