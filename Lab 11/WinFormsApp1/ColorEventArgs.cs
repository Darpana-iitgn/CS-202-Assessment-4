using System;

public class ColorEventArgs : EventArgs
{
    public string ColorName { get; }

    public ColorEventArgs(string colorName)
    {
        ColorName = colorName;
    }
}
