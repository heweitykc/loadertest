using UnityEngine;
using System.Collections;

//网址
//http://blog.jobbole.com/88152/

public enum TokenType
{
    INTEGER,
    PLUS,
    MINUS,
    EOF
}

sealed public class Token
{
    public TokenType type;
    public int value;

    public Token(TokenType type, int value)
    {
        this.type = type;
        this.value = value;
        Debug.Log(this.ToString());
    }

    public override string ToString()
    {
        return type + "=" + value;
    }
}

public class interpreter
{
    public string text;
    public int pos;
    public Token currentToken;

    public interpreter(string text)
    {
        this.text = text;
        this.pos = 0;
        this.currentToken = null;
    }

    public Token getNext()
    {
        if (pos > (text.Length - 1)) {
            return new Token(TokenType.EOF, char.MinValue);
        }
        char cchar = text[pos];        
        if (cchar == '+'){
            pos += 1;
            return new Token(TokenType.PLUS, cchar);
        } else if (cchar == '-') {
            pos += 1;
            return new Token(TokenType.MINUS, cchar);
        } else {
            return new Token(TokenType.INTEGER, getInteger());
        }
    }

    int getInteger()
    {
        string result="";
        char cchar;        
        do {
            cchar = text[pos];
            if (cchar < 48 || cchar > 57) break;            
            result += cchar.ToString();
            pos++;
            if (pos >= text.Length) break; //超出了
        } while (true);
        Debug.Log("ret=" + result);
        return int.Parse(result);
    }

    public void eat(TokenType type)
    {
        if (currentToken.type == type) {
            currentToken = getNext();
        }            
    }

    public int expr()
    {
        currentToken = getNext();
        var left = currentToken;
        eat(left.type);

        var op = currentToken;
        eat(op.type);

        var right = currentToken;
        eat(right.type);

        if(op.type == TokenType.PLUS)
            return left.value + right.value;        
        else if(op.type == TokenType.MINUS)
            return left.value - right.value;

        return 0;
    }
}
