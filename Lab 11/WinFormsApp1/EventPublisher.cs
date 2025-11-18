using System;

public class EventPublisher
{
    // Custom delegate for color event
    public delegate void ColorChangedHandler(object sender, ColorEventArgs e);
    public event ColorChangedHandler ColorChanged;

    // Custom delegate for text change event
    public delegate void TextChangedHandler(object sender, EventArgs e);
    public event TextChangedHandler TextChanged;

    // Method to raise color event
    public void RaiseColorChanged(string colorName)
    {
        ColorChanged?.Invoke(this, new ColorEventArgs(colorName));
    }

    // Method to raise text event
    public void RaiseTextChanged()
    {
        TextChanged?.Invoke(this, EventArgs.Empty);
    }
}
