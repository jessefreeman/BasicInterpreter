namespace JesseFreeman.BasicInterpreter.Data;

public class BasicNumberArray : IBasicVariable<double[]>
{
    public double[] Value { get; private set; }

    public double[] GetValue()
    {
        return Value;
    }

    public void SetValue(double[] value)
    {
        Value = value;
    }
}