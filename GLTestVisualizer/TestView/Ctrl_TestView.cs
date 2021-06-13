using System.Windows.Forms;

namespace GLTestVisualizer.TestView
{
    public partial class Ctrl_TestView : UserControl
    {
        public Ctrl_TestView()
        {
            InitializeComponent();
        }

        public virtual string SceneName => "TestScene";

        public virtual void Start()
        { }
        public virtual void Exit()
        { }
    }
}
