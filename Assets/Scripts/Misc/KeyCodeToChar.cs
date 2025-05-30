using UnityEngine;

public class KeyCodeToChar : MonoBehaviour
{
    public static char KeyCodeToCharCalc(KeyCode keyCode)
{
    if (keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9)
    {
        return (char)('0' + (keyCode - KeyCode.Alpha0));
    }

    if (keyCode >= KeyCode.A && keyCode <= KeyCode.Z)
    {
        return (char)('a' + (keyCode - KeyCode.A));
    }

    switch (keyCode)
    {
        case KeyCode.Space: return ' ';
        case KeyCode.Minus: return '-';
        case KeyCode.Equals: return '=';
        case KeyCode.LeftBracket: return '[';
        case KeyCode.RightBracket: return ']';
        case KeyCode.Semicolon: return ';';
        case KeyCode.Quote: return '\'';
        case KeyCode.Comma: return ',';
        case KeyCode.Period: return '.';
        case KeyCode.Slash: return '/';
        case KeyCode.Backslash: return '\\';
        case KeyCode.BackQuote: return '`';
    }

    return '\0';
}
}
