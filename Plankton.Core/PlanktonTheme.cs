using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI.ThemeVS2012;

namespace Plankton.UI;

public sealed class PlanktonTheme : VS2015LightTheme
{
    public PlanktonTheme()
    {
        ColorPalette = new DockPanelColorPalette(new PlanktonPaletteFactory());

        ImageService =
            new WeifenLuo.WinFormsUI.ThemeVS2012.ImageService(this);

        ToolStripRenderer = new VisualStudioToolStripRenderer(ColorPalette)
        {
            UseGlassOnMenuStrip = false
        };

        Measures.DockPadding = 0;
        
    }

    private sealed class PlanktonPaletteFactory : IPaletteFactory
    {
        private static readonly Color Window = Color.FromArgb(240, 240, 240);
        private static readonly Color Surface = Color.FromArgb(232, 232, 232);
        private static readonly Color SurfaceLight = Color.FromArgb(248, 248, 248);
        private static readonly Color Hover = Color.FromArgb(218, 218, 218);
        private static readonly Color Pressed = Color.FromArgb(202, 202, 202);
        private static readonly Color Selected = Color.FromArgb(210, 210, 210);

        private static readonly Color Border = Color.FromArgb(175, 175, 175);
        private static readonly Color BorderDark = Color.FromArgb(145, 145, 145);
        private static readonly Color Separator = Color.FromArgb(200, 200, 200);

        private static readonly Color Text = Color.FromArgb(32, 32, 32);
        private static readonly Color TextInactive = Color.FromArgb(96, 96, 96);
        private static readonly Color TextDisabled = Color.FromArgb(150, 150, 150); 
        private static readonly Color TabStrip = Color.FromArgb(220, 220, 220);

        public void Initialize(DockPanelColorPalette palette)
        {
            // Auto-hide
            palette.AutoHideStripDefault.Background = Surface;
            palette.AutoHideStripDefault.Border = Border;
            palette.AutoHideStripDefault.Text = TextInactive;

            palette.AutoHideStripHovered.Background = Hover;
            palette.AutoHideStripHovered.Border = BorderDark;
            palette.AutoHideStripHovered.Text = Text;

            // Menus
            palette.CommandBarMenuDefault.Background = Window;
            palette.CommandBarMenuDefault.Text = Text;

            palette.CommandBarMenuPopupDefault.Arrow = Text;
            palette.CommandBarMenuPopupDefault.BackgroundBottom = SurfaceLight;
            palette.CommandBarMenuPopupDefault.BackgroundTop = SurfaceLight;
            palette.CommandBarMenuPopupDefault.Border = Border;
            palette.CommandBarMenuPopupDefault.Checkmark = Text;
            palette.CommandBarMenuPopupDefault.CheckmarkBackground = Selected;
            palette.CommandBarMenuPopupDefault.IconBackground = SurfaceLight;
            palette.CommandBarMenuPopupDefault.Separator = Separator;

            palette.CommandBarMenuPopupDisabled.Checkmark = TextDisabled;
            palette.CommandBarMenuPopupDisabled.CheckmarkBackground = Surface;
            palette.CommandBarMenuPopupDisabled.Text = TextDisabled;

            palette.CommandBarMenuPopupHovered.Arrow = Text;
            palette.CommandBarMenuPopupHovered.Checkmark = Text;
            palette.CommandBarMenuPopupHovered.CheckmarkBackground = Selected;
            palette.CommandBarMenuPopupHovered.ItemBackground = Hover;
            palette.CommandBarMenuPopupHovered.Text = Text;

            palette.CommandBarMenuTopLevelHeaderHovered.Background = Hover;
            palette.CommandBarMenuTopLevelHeaderHovered.Border = Border;
            palette.CommandBarMenuTopLevelHeaderHovered.Text = Text;

            // Toolbars
            palette.CommandBarToolbarDefault.Background = Window;
            palette.CommandBarToolbarDefault.Border = Separator;
            palette.CommandBarToolbarDefault.Grip = BorderDark;
            palette.CommandBarToolbarDefault.OverflowButtonBackground = Window;
            palette.CommandBarToolbarDefault.OverflowButtonGlyph = TextInactive;
            palette.CommandBarToolbarDefault.Separator = Border;
            palette.CommandBarToolbarDefault.SeparatorAccent = SurfaceLight;
            palette.CommandBarToolbarDefault.Tray = Window;

            palette.CommandBarToolbarButtonChecked.Background = Selected;
            palette.CommandBarToolbarButtonChecked.Border = BorderDark;
            palette.CommandBarToolbarButtonChecked.Text = Text;

            palette.CommandBarToolbarButtonCheckedHovered.Border = BorderDark;
            palette.CommandBarToolbarButtonCheckedHovered.Text = Text;

            palette.CommandBarToolbarButtonDefault.Arrow = Text;

            palette.CommandBarToolbarButtonHovered.Arrow = Text;
            palette.CommandBarToolbarButtonHovered.Separator = Border;

            palette.CommandBarToolbarButtonPressed.Arrow = Text;
            palette.CommandBarToolbarButtonPressed.Background = Pressed;
            palette.CommandBarToolbarButtonPressed.Text = Text;

            palette.CommandBarToolbarOverflowHovered.Background = Hover;
            palette.CommandBarToolbarOverflowHovered.Glyph = Text;

            palette.CommandBarToolbarOverflowPressed.Background = Pressed;
            palette.CommandBarToolbarOverflowPressed.Glyph = Text;

            // Document overflow button
            palette.OverflowButtonDefault.Glyph = TextInactive;

            palette.OverflowButtonHovered.Background = Hover;
            palette.OverflowButtonHovered.Border = Border;
            palette.OverflowButtonHovered.Glyph = Text;

            palette.OverflowButtonPressed.Background = Pressed;
            palette.OverflowButtonPressed.Border = BorderDark;
            palette.OverflowButtonPressed.Glyph = Text;

            // Document tabs
            palette.TabSelectedActive.Background = Surface;
            palette.TabSelectedActive.Button = TextInactive;
            palette.TabSelectedActive.Text = Text;

            palette.TabSelectedInactive.Background = Surface;
            palette.TabSelectedInactive.Button = TextInactive;
            palette.TabSelectedInactive.Text = TextInactive;

            palette.TabUnselected.Background = Color.FromArgb(224, 224, 224);

            palette.TabUnselected.Background = Surface;
            palette.TabUnselected.Text = TextInactive;

            palette.TabUnselectedHovered.Background = Hover;
            palette.TabUnselectedHovered.Button = Text;
            palette.TabUnselectedHovered.Text = Text;

            SetButton(
                palette.TabButtonSelectedActiveHovered,
                Hover, Border, Text);

            SetButton(
                palette.TabButtonSelectedActivePressed,
                Pressed, BorderDark, Text);

            SetButton(
                palette.TabButtonSelectedInactiveHovered,
                Hover, Border, Text);

            SetButton(
                palette.TabButtonSelectedInactivePressed,
                Pressed, BorderDark, Text);

            SetButton(
                palette.TabButtonUnselectedTabHoveredButtonHovered,
                Hover, Border, Text);

            SetButton(
                palette.TabButtonUnselectedTabHoveredButtonPressed,
                Pressed, BorderDark, Text);

            // Main workspace
            //palette.MainWindowActive.Background = Surface;
            palette.MainWindowActive.Background = TabStrip;

            // Status bar
            palette.MainWindowStatusBarDefault.Background = Surface;
            palette.MainWindowStatusBarDefault.Highlight = Hover;
            palette.MainWindowStatusBarDefault.HighlightText = Text;
            palette.MainWindowStatusBarDefault.ResizeGrip = BorderDark;
            palette.MainWindowStatusBarDefault.ResizeGripAccent = SurfaceLight;
            palette.MainWindowStatusBarDefault.Text = Text;

            // Tool-window captions
            palette.ToolWindowCaptionActive.Background = Selected;
            palette.ToolWindowCaptionActive.Button = Text;
            palette.ToolWindowCaptionActive.Grip = BorderDark;
            palette.ToolWindowCaptionActive.Text = Text;

            palette.ToolWindowCaptionInactive.Background = Surface;
            palette.ToolWindowCaptionInactive.Button = TextInactive;
            palette.ToolWindowCaptionInactive.Grip = Border;
            palette.ToolWindowCaptionInactive.Text = TextInactive;

            SetButton(
                palette.ToolWindowCaptionButtonActiveHovered,
                Hover, BorderDark, Text);

            SetButton(
                palette.ToolWindowCaptionButtonPressed,
                Pressed, BorderDark, Text);

            SetButton(
                palette.ToolWindowCaptionButtonInactiveHovered,
                Hover, Border, Text);

            // Tool-window tabs
            palette.ToolWindowTabSelectedActive.Background = SurfaceLight;
            palette.ToolWindowTabSelectedActive.Text = Text;

            palette.ToolWindowTabSelectedInactive.Background = Surface;
            palette.ToolWindowTabSelectedInactive.Text = TextInactive;

            palette.ToolWindowTabUnselected.Background = Surface;
            palette.ToolWindowTabUnselected.Text = TextInactive;

            palette.ToolWindowTabUnselectedHovered.Background = Hover;
            palette.ToolWindowTabUnselectedHovered.Text = Text;

            // In PlanktonTheme
            palette.ToolWindowBorder = Surface;
            palette.ToolWindowSeparator = Surface;

            // Docking guide graphics
            palette.DockTarget.Background = SurfaceLight;
            palette.DockTarget.Border = BorderDark;
            palette.DockTarget.ButtonBackground = Surface;
            palette.DockTarget.ButtonBorder = Border;
            palette.DockTarget.GlyphBackground = SurfaceLight;
            palette.DockTarget.GlyphArrow = Text;
            palette.DockTarget.GlyphBorder = BorderDark;
        }

        private static void SetButton(
            HoveredButtonPalette palette,
            Color background,
            Color border,
            Color glyph)
        {
            palette.Background = background;
            palette.Border = border;
            palette.Glyph = glyph;
        }
    }
}
