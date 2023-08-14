namespace JesseFreeman.BasicInterpreter.Data;

public class BasicString : IBasicVariable<string>
{
    public string Value { get; private set; }

    public string GetValue()
    {
        return Value;
    }

    public void SetValue(string value)
    {
        Value = value;
    }
}