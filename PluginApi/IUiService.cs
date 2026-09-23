using System;
using System.Windows.Forms;

namespace PluginApi
{
    public enum ToolStripLocationKind
    {
        Menu,
        Toolbar
    }

    public interface IUIService
    {
        // --------------------------------------------------------
        // EXTENSION POINTS
        // --------------------------------------------------------

        /// <summary>
        /// Exposes a menu, context menu or toolbar as an extension point.
        /// </summary>
        IDisposable RegisterToolStripLocation(
            string locationId,
            ToolStripLocationKind kind,
            ToolStripItemCollection items);

        /// <summary>
        /// Exposes a WinForms container as an extension point.
        ///
        /// Plugins may contribute Controls to it.
        /// </summary>
        IDisposable RegisterControlLocation(
            string locationId,
            Control container);


        // --------------------------------------------------------
        // TOOLSTRIP CONTRIBUTIONS
        // --------------------------------------------------------

        void AddCommand(
            string locationId,
            string commandId,
            int order = 0);

        void AddSeparator(
            string locationId,
            int order = 0);

        /// <summary>
        /// Adds a dropdown/submenu and creates a new menu extension point
        /// for its contents.
        /// </summary>
        void AddDropDown(
            string parentLocationId,
            string childLocationId,
            string text,
            int order = 0);


        // --------------------------------------------------------
        // CONTROL CONTRIBUTIONS
        // --------------------------------------------------------

        /// <summary>
        /// Adds a Control to a registered control location.
        ///
        /// The factory is called once for every live instance of that
        /// location. It must return a new Control each time.
        /// </summary>
        void AddControl(
            string locationId,
            Func<Control> factory,
            int order = 0);
    }
}