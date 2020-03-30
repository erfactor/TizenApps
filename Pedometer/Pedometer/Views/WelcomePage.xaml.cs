using Tizen.Wearable.CircularUI.Forms;
using Pedometer.Privilege;

namespace Pedometer.Views
{
    /// <summary>
    /// Welcome page of the application
    /// </summary>
    public partial class WelcomePage : CirclePage
    {
        /// <summary>
        /// Initalizes WelcomePage class instance
        /// </summary>
        public WelcomePage()
        {
            InitializeComponent();
            var privilegeManager = PrivilegeManager.Instance;
            if (!privilegeManager.AllPermissionsGranted())
            {
                privilegeManager.CheckAllPrivileges();
            }
        }
    }
}