public class LexerException : System.Exception
{
    public LexerException(string msg)
        : base(msg)
    {
    }

}

public class Lexer
{
    protected int position;
    protected char currentCh;       // ��������� ��������� ������
    protected int currentCharValue; // ����� �������� ���������� ���������� �������
    protected System.IO.StringReader inputReader;
    protected string inputString;

    public Lexer(string input)
    {
        inputReader = new System.IO.StringReader(input);
        inputString = input;
    }

    public void Error()
    {
        System.Text.StringBuilder o = new System.Text.StringBuilder();
        o.Append(inputString + '\n');
        o.Append(new System.String(' ', position - 1) + "^\n");
        o.AppendFormat("Error in symbol {0}", currentCh);
        throw new LexerException(o.ToString());
    }

    protected void NextCh()
    {
        this.currentCharValue = this.inputReader.Read();
        this.currentCh = (char)currentCharValue;
        this.position += 1;
    }

    public virtual void Parse()
    {

    }
}

public class IntLexer : Lexer
{
    protected System.Text.StringBuilder intString;
    public IntLexer(string input)
        : base(input)
    {
        intString = new System.Text.StringBuilder();
    }

    public override void Parse()
    {
        NextCh();
        if (char.IsLetter(currentCh))
            NextCh();
        else
            if (currentCharValue == -1)
                ;
            else
                Error();

        while (currentCharValue != -1)
        {
            if (!char.IsDigit(currentCh) && !char.IsLetter(currentCh))
                Error();
            if (char.IsDigit(currentCh))
            {
                NextCh();
                if (currentCharValue == -1)
                    break;
                if (!char.IsLetter(currentCh))
                    Error();
                NextCh();
                continue;
            }
            if (char.IsLetter(currentCh))
            {
                NextCh();
                if (currentCharValue == -1)
                    break;
                if (!char.IsDigit(currentCh))
                    Error();
                NextCh();
                continue;
            }
        } 
            
        if (currentCharValue != -1) // StringReader ������ -1 � ����� ������
            Error();

        System.Console.WriteLine("Integer is recognized");
    }
}


public class Program
{
    public static void Main()
    {
        string input = System.Console.ReadLine();
        Lexer L = new IntLexer(input);
        try
        {
            L.Parse();
        }
        catch (LexerException e)
        {
            System.Console.WriteLine(e.Message);
        }

    }
}