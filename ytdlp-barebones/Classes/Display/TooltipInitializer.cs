public static class TooltipInitializer
{
    // Sets the tooltips for the provided controls.
    /// <param name="toolTipData">A dictionary mapping controls to their tooltip text.</param>
    public static void SetTooltips(Dictionary<Control, string> toolTipData)
    {
        ToolTip toolTip = new ToolTip
        {
            // Keep the default fade animation.
            UseAnimation = true,
            UseFading = true,

            // Chat bubble
            IsBalloon = true,
        };

        foreach (var pair in toolTipData)
        {
            Control control = pair.Key;
            string tipText = pair.Value;

            // Set the tooltip text for the control.
            toolTip.SetToolTip(control, tipText);

            // When the mouse leaves the control, hide the tooltip.
            control.MouseLeave += (s, e) => toolTip.Hide(control);
        }
    }
}
