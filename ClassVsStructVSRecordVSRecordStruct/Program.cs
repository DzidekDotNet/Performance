using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmark>();

[MemoryDiagnoser]
[RankColumn]
public class Benchmark
{
    private readonly string _sampleString = new('a', 500);
    private readonly int _a = 1;

    private readonly int rows = 1000;

    [Benchmark]
    public int RefStructWithInt()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            RefStructWithInt test = new(_a);
            sum += test.Value;
        }

        return sum;
    }

    [Benchmark]
    public int StructWithInt()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            StructWithInt test = new(_a);
            sum += test.Value;
        }

        return sum;
    }

    [Benchmark]
    public int ReadOnlyRefStructWithInt()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            ReadOnlyRefStructWithInt test = new(_a);
            sum += test.Value;
        }

        return sum;
    }

    [Benchmark]
    public int ReadOnlyStructWithInt()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            ReadOnlyStructWithInt test = new(_a);
            sum += test.Value;
        }

        return sum;
    }

    [Benchmark]
    public int ReadOnlyRecordStructWithInt()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            ReadOnlyRecordStructWithInt test = new(_a);
            sum += test.Value;
        }

        return sum;
    }

    [Benchmark]
    public int RecordStructWithInt()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            RecordStructWithInt test = new(_a);
            sum += test.Value;
        }

        return sum;
    }

    [Benchmark]
    public int ClassWithInt()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            ClassWithInt test = new(_a);
            sum += test.Value;
        }

        return sum;
    }

    [Benchmark]
    public int RecordWithInt()
    {
        int sum = 0;
        for (int i = 0; i < rows; i++)
        {
            RecordWithInt test = new(_a);
            sum += test.Value;
        }

        return sum;
    }

    [Benchmark]
    public int StructWithString()
    {
        int sum = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rows; i++)
        {
            StructWithString test = new(_a, _sampleString);
            sum += test.Value;
            sb.Append(test.Text);
        }

        return sum + sb.Length;
    }

    [Benchmark]
    public int RefStructWithString()
    {
        int sum = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rows; i++)
        {
            RefStructWithString test = new(_a, _sampleString);
            sum += test.Value;
            sb.Append(test.Text);
        }

        return sum + sb.Length;
    }

    [Benchmark]
    public int ReadOnlyRefStructWithString()
    {
        int sum = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rows; i++)
        {
            ReadOnlyRefStructWithString test = new(_a, _sampleString);
            sum += test.Value;
            sb.Append(test.Text);
        }

        return sum + sb.Length;
    }

    [Benchmark]
    public int ReadOnlyStructWithString()
    {
        int sum = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rows; i++)
        {
            ReadOnlyStructWithString test = new(_a, _sampleString);
            sum += test.Value;
            sb.Append(test.Text);
        }

        return sum + sb.Length;
    }

    [Benchmark]
    public int ReadOnlyRecordStructWithString()
    {
        int sum = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rows; i++)
        {
            ReadOnlyRecordStructWithString test = new(_a, _sampleString);
            sum += test.Value;
            sb.Append(test.Text);
        }

        return sum + sb.Length;
    }

    [Benchmark]
    public int RecordStructWithString()
    {
        int sum = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rows; i++)
        {
            RecordStructWithString test = new(_a, _sampleString);
            sum += test.Value;
            sb.Append(test.Text);
        }

        return sum + sb.Length;
    }

    [Benchmark]
    public int RecordWithString()
    {
        int sum = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rows; i++)
        {
            RecordWithString test = new(_a, _sampleString);
            sum += test.Value;
            sb.Append(test.Text);
        }

        return sum + sb.Length;
    }

    [Benchmark]
    public int ClassWithString()
    {
        int sum = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rows; i++)
        {
            ClassWithString test = new(_a, _sampleString);
            sum += test.Value;
            sb.Append(test.Text);
        }

        return sum + sb.Length;
    }

    [Benchmark]
    public int TupleWithString()
    {
        int sum = 0;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < rows; i++)
        {
            var test = (Value: _a, Text: _sampleString);
            sum += test.Value;
            sb.Append(test.Text);
        }

        return sum + sb.Length;
    }
}

public ref struct RefStructWithInt
{
    public RefStructWithInt(int value)
    {
        Value = value;
    }

    public int Value { get; }
};

public readonly ref struct ReadOnlyRefStructWithInt
{
    public ReadOnlyRefStructWithInt(int value)
    {
        Value = value;
    }

    public int Value { get; }
};

public struct StructWithInt
{
    public StructWithInt(int value)
    {
        Value = value;
    }

    public int Value { get; }
};

public readonly struct ReadOnlyStructWithInt
{
    public ReadOnlyStructWithInt(int value)
    {
        Value = value;
    }

    public int Value { get; }
};

public record struct RecordStructWithInt(int Value);

public readonly record struct ReadOnlyRecordStructWithInt(int Value);

public struct StructWithString
{
    public StructWithString(int value, string text)
    {
        Value = value;
        Text = text;
    }

    public int Value { get; }
    public string Text { get; }
};

public ref struct RefStructWithString
{
    public RefStructWithString(int value, string text)
    {
        Value = value;
        Text = text;
    }

    public int Value { get; }
    public string Text { get; }
};

public readonly ref struct ReadOnlyRefStructWithString
{
    public ReadOnlyRefStructWithString(int value, string text)
    {
        Value = value;
        Text = text;
    }

    public int Value { get; }
    public string Text { get; }
};

public readonly struct ReadOnlyStructWithString
{
    public ReadOnlyStructWithString(int value, string text)
    {
        Value = value;
        Text = text;
    }

    public int Value { get; }
    public string Text { get; }
};

public record struct RecordStructWithString(int Value, string Text);

public readonly record struct ReadOnlyRecordStructWithString(int Value, string Text);

public class ClassWithInt
{
    public ClassWithInt(int value)
    {
        Value = value;
    }

    public int Value { get; }
};

public record RecordWithInt(int Value);

public record RecordWithString(int Value, string Text);

public class ClassWithString
{
    public ClassWithString(int value, string text)
    {
        Value = value;
        Text = text;
    }

    public int Value { get; }
    public string Text { get; }
};