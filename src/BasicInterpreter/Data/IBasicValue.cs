namespace JesseFreeman.BasicInterpreter.Data;

public interface IBasicVariable<T>
{
    T GetValue();
    void SetValue(T value);
}