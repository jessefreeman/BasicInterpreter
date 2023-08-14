namespace JesseFreeman.BasicInterpreter.Data;

public class BasicStringArray : IBasicVariable<string[]>
{
    public string[] Value { get; private set; }

    public string[] GetValue()
    {
        return Value;
    }

    public void SetValue(string[] value)
    {
        Value = value;
    }
}