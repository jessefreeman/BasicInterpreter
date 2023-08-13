namespace JesseFreeman.BasicInterpreter.Data;

public class BasicStringArray : IBasicVariable<string[]>
{
    public string[] Value { get; private set; }

    public string[] GetValue() => Value;

    public void SetValue(string[] value)
    {
        Value = value;
    }
}