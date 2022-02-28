using System.Collections.Generic;

public class Data
{
    static int numberHereString = 0;
    static List<Character> characters = new List<Character>();
    static bool isAuto = false;
    static bool isSkip = false;
    static bool isEndString = false;
    static float timeAuto = 1;
    static float Volume = 1;
    static bool isClick = true;

    public void setNumberString(int input)
    {
        numberHereString = input;
    }

    public void addCharacter(Character input)
    {
        characters.Add(input);
    }

    public int getNumberString()
    {
        return numberHereString;
    }

    public Character getCharacter(string input)
    {
        foreach (Character character in characters)
            if (character.name.ToLower().Equals(input.ToLower().Trim()) || character.getName().ToLower().Equals(input.ToLower().Trim()))
                return character;
        return new Character();
    }

    public void setTimeAuto(float newTime)
    {
        timeAuto = newTime;
    }

    public float getTimeAuto()
    {
        return timeAuto;
    }

    public void setAuto(bool booling)
    {
        isAuto = booling;
    }

    public bool getAuto()
    {
        return isAuto;
    }

    public void setSkip(bool booling)
    {
        isSkip = booling;
    }

    public bool getSkip()
    {
        return isSkip;
    }

    public void setEndString(bool booling)
    {
        isEndString = booling;
    }

    public bool getEndString()
    {
        return isEndString;
    }

    public void setClick(bool click)
    {
        isClick = click;
    }

    public bool getClick()
    {
        return isClick;
    }

    public float getVolume()
    {
        return Volume;
    }

    public void setVolume(float v)
    {
        Volume = v;
    }
}
