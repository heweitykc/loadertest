using UnityEngine;
using System.Collections;

//网址
//http://blog.jobbole.com/88152/

public enum TokenType
{
    INTEGER,
    PLUS,
    EOF
}

public class Token
{
    public TokenType type;
    public int value;

    public Token(TokenType type, int value)
    {
        this.type = type;
        this.value = value;
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
            return new Token(TokenType.EOF,' ');
        }
        char cchar = text[pos];
        if (cchar == '+')
        {
            var token = new Token(TokenType.PLUS, cchar);
            pos += 1;
            return token;
        } else {
            var token = new Token(TokenType.INTEGER, int.Parse(cchar.ToString()));
            pos += 1;
            return token;
        }
    }

    public void eat(TokenType type)
    {
        if (currentToken.type == type)
            currentToken = getNext();
    }

    public int expr()
    {
        currentToken = getNext();
        var left = currentToken;
        eat(TokenType.INTEGER);

        var op = currentToken;
        eat(TokenType.PLUS);

        var right = currentToken;
        eat(TokenType.INTEGER);

        return left.value + right.value;        
    }
}
