using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GLTestVisualizer.Model
{
    public class CameraMouseController : CameraController, IDisposable
    {
        public CameraMouseController(Control inOwner)
        {
            Owner = inOwner;
            return;
        }

        public override Matrix4x4 CameraMatrix => throw new NotImplementedException();

        private void RegisterTo(Control inControl)
        {
            //inControl.MouseClick
        }

        protected virtual void Dispose(bool inDisposing)
        {
            if (!m_Disposed) {
                if (inDisposing) {

                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                m_Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            return;
        }

        private readonly Control Owner = null;
        private bool m_Disposed;
    }
}
