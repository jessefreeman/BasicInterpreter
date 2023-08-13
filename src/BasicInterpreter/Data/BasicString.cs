namespace JesseFreeman.BasicInterpreter.Data;

public class BasicString : IBasicVariable<string>
{
    public string Value { get; private set; }

    public string GetValue() => Value;

    public void SetValue(string value)
    {
        Value = value;
    }
}
