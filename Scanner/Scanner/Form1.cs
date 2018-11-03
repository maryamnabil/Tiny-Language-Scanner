using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scanner
{
    public partial class Form1 : Form
    {
        string code;
        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
            label1.Hide();

        }
        // is digit 
        bool isDigit(char d) { return (d >= '0' && d <= '9'); }

        //is letter
        bool isLetter(char l) { return (l >= 'a' && l <= 'z' || l >= 'A' && l <= 'Z'); }

        //is symbol 
        bool isSymbol(char c)
        {
            return (c == '+' || c == '-' || c == '*' || c == '/' || c == '=' || c == '<' || c == '(' || c == ')' || c == ';');
        }
        //is whiteSpace
        bool isSpace(char s) { return (s == ' ' || s == '\t' || s == '\n'); }
        //STATES 
        enum states { START, COMMENT, NUM, IDINTIFIER, ASSIGN, DONE };
        states state = states.START;
        //reserved words
        string[] RES_WORDS = { "if", "then", "else", "end", "repeat", "until", "read", "write" };


        private void button1_Click(object sender, EventArgs e)
        {
            code = richTextBox1.Text;
            getToken(code+=" ");
            label1.Show();

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }



        public void getToken(string c)
        {
            string mytoken="";
            bool res_flag = false;
            int i = 0;
            while (state != states.DONE)
            {
                switch (state)
                {
                    case states.START:
                        if (isSpace(c[i]))
                        {
                            i++;
                            if (i == c.Length) state = states.DONE;
                            else state = states.START;
                        }
                        else if (isDigit(c[i]))
                        {
                            state = states.NUM;
                        }
                        else if (isLetter(c[i]))
                        {
                            state = states.IDINTIFIER;
                        }
                        else if (c[i] == ':')
                        {
                            state = states.ASSIGN;
                        }
                        else if (c[i] == '{')
                        {
                            i++;
                            state = states.COMMENT;
                        }
                        else if (isSymbol(c[i]))
                        {
                            switch (c[i])
                            {
                                case ';': label1.Text += c[i]+"\n"; break;
                                default: label1.Text += c[i]+" , symbol \n"; break;
                            }
                            i++;
                            if (i == c.Length) state = states.DONE;
                            else state = states.START;
                        }
                        else state = states.DONE;
                        break;
                    case states.COMMENT:
                        if (state == states.COMMENT)
                        {
                            while (c[i] != '}')
                            {
                                i++;
                            }
                            i++;
                            if (i == c.Length) state = states.DONE;
                            else state = states.START;
                        }
                        break;

                    case states.NUM:
                        while (isDigit(c[i]))
                        {
                            mytoken += c[i];
                            i++;
                        }
                        label1.Text+=mytoken +=" , number \n";
                        mytoken = "";
                        if (i == c.Length) state = states.DONE;
                        else state = states.START;
                        break;

                    case states.IDINTIFIER:
                        while (isLetter(c[i]))
                        {
                                mytoken += c[i];
                                i++;
                        }
                        for (int count = 0; count < 8; count++)
                        {
                            if (RES_WORDS[count] == mytoken) res_flag = true;
                        }
                        if (res_flag) label1.Text += mytoken + " , reserved word \n";
                        else label1.Text += mytoken += " , identifier \n";
                        mytoken = "";
                        res_flag = false;
                        if (i == c.Length) state = states.DONE;
                        else state = states.START;
                        break;

                    case states.ASSIGN:
                        if (c[i] == ':')
                        {
                            i += 2;
                            label1.Text += " := , assign \n";
                            state = states.START;
                        }
                        else
                        {
                            if (i == c.Length) state = states.DONE;
                            else state = states.START;
                        }
                        break;
                    case states.DONE:
                        break;

                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label1.Hide();
            richTextBox1.Text = "";
            state = states.START;
        }

    }

}


